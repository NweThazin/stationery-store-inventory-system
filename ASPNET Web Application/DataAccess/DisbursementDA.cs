using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace DataAccess
{

    public class DisbursementDA
    {
        ADTeam1Entities context;

        public DisbursementDA()
        {
            context = new ADTeam1Entities();

        }

        public string getEmployeeName(int departmentID)
        {
            List<Employee> employees = context.Employees.Where(x => x.DepartmentID == departmentID).ToList();
            string name = null;
            foreach (Employee e in employees)
            {
                User user = new User();
                if (e.Users.Where(x => x.DelegatedRole == "Employee Rep" || x.DelegatedRole == "Employee RepHead").FirstOrDefault() != null)
                {
                    user = e.Users.Where(x => x.DelegatedRole == "Employee Rep" || x.DelegatedRole == "Employee RepHead").FirstOrDefault();
                    int? employeeID = user.EmployeeID;
                    name = e.FirstName + "  " + e.LastName;
                }
            }
            return name;

        }




        public string getCollectionPointName(int collectionPointID)
        {
            return context.CollectionPoints.Where(x => x.CollectionPointID == collectionPointID).Select(x => x.Name).FirstOrDefault();
        }

        public string getDepartmentName(int departmentID)
        {
            return context.Departments.Where(x => x.DepartmentID == departmentID).Select(x => x.DepartmentName).FirstOrDefault();
        }

        public string getcategoryName(string itemNumber)
        {
            int? categoryID = context.Items.Where(x => x.ItemNumber == itemNumber).Select(x => x.CategoryID).FirstOrDefault();
            return context.Categories.Where(x => x.CategoryID == categoryID).Select(x => x.CategoryName).FirstOrDefault();
        }

        public List<CollectionPointBO> getCollectionPointDeptList(int collectionPointID)
        {
            List<CollectionPointBO> lst = new List<CollectionPointBO>();
            var qry = (from c in context.CollectionPoints
                       join d in context.Departments on c.CollectionPointID equals d.CollectionPointID
                       join emp in context.Employees on d.DepartmentID equals emp.DepartmentID
                       join u in context.Users on emp.EmployeeID equals u.EmployeeID
                       join dis in context.Disbursements on d.DepartmentID equals dis.DepartmentID
                       join disItem in context.Disbursement_Item on dis.DisbursementID equals disItem.DisbursementID
                       where (c.CollectionPointID == collectionPointID) && (u.DelegatedRole == "Employee Rep" || u.DelegatedRole == "Employee RepHead") && disItem.Status == "Pending"
                       group new { c, d, emp, u } by disItem.DisbursementID into g
                       select new
                       {
                           g.FirstOrDefault().c.CollectionPointID,
                           g.FirstOrDefault().c.Name,
                           g.FirstOrDefault().c.Time,
                           g.FirstOrDefault().d.DepartmentID,
                           g.FirstOrDefault().d.DepartmentName,
                           g.FirstOrDefault().emp.FirstName,
                           g.FirstOrDefault().emp.LastName,
                           g.FirstOrDefault().emp.Phone,
                           total = g.Count()
                       }).ToList();

            foreach (var q in qry)
            {
                CollectionPointBO cpBO = new CollectionPointBO();
                cpBO.CollectionPointID = q.CollectionPointID;
                cpBO.Name = q.Name;
                cpBO.Time = q.Time;
                cpBO.DepartmentID = q.DepartmentID;
                cpBO.DepartmentName = q.DepartmentName;
                cpBO.EmpFirstName = q.FirstName;
                cpBO.EmpLastName = q.LastName;
                cpBO.Phone = q.Phone;
                cpBO.TotalItem = q.total;
                cpBO.EmployeeName = q.FirstName + "" + q.LastName;
                lst.Add(cpBO);
            }

            return lst;
        }

        public List<DisbursementBO> getDisbursementItems(int departmentID)
        {
            List<DisbursementBO> lst = new List<DisbursementBO>();

            var qry = (from dis in context.Disbursements
                       join disItem in context.Disbursement_Item on dis.DisbursementID equals disItem.DisbursementID
                       join i in context.Items on disItem.ItemID equals i.ItemID
                       where dis.DepartmentID == departmentID && disItem.Status == "Pending"
                       select new
                       {
                           disItem.Disbursement_ItemID,
                           disItem.DisbursementID,
                           i.Description,
                           disItem.Qty,
                           disItem.Status,
                           dis.CollectionDate,
                           i.ItemNumber,
                           i.UnitOfMeasure,
                           disItem.ItemID
                       }
                           ).ToList();

            foreach (var q in qry)
            {
                DisbursementBO di = new DisbursementBO();
                di.DisbursementItemID = q.Disbursement_ItemID;
                di.DisbursementID = (int)q.DisbursementID;
                di.ItemDescription = q.Description;
                di.Qty = (int)q.Qty;
                di.CollectionDate = (DateTime)q.CollectionDate;
                di.Status = q.Status;
                di.ItemNumber = q.ItemNumber;
                di.Uom = q.UnitOfMeasure;
                di.ItemID = q.ItemID;
                lst.Add(di);
            }
            return lst;
        }

        public void updateDisbursement(int departmentID, int disbursementID, int itemID, int updateQty)
        {

            var qry1 = context.Disbursement_Item.Where(x => x.DisbursementID == disbursementID && x.ItemID == itemID && x.Status.Equals("Pending")).ToList().FirstOrDefault();
            qry1.Qty = updateQty;

            var req = context.Requisitions.Where(x => x.DepartmentID == departmentID && x.Status.Equals("Approved")).ToList();
            foreach (var r in req)
            {
                var qry2 = context.Requisition_Item.Where(x => x.RequisitionID == r.RequisitionID && x.Status.Equals("Pending") && x.ItemID == itemID).ToList().FirstOrDefault();

                if (qry2 != null)
                {
                    if (updateQty == qry2.UnfulfilledQty)
                    {
                        qry2.UnfulfilledQty = 0;
                        qry2.ReceivedQty = qry2.ReceivedQty + updateQty;
                        qry2.Status = "Completed";
                    }
                    else if (updateQty < qry2.UnfulfilledQty)
                    {
                        qry2.UnfulfilledQty = qry2.RequestedQty - updateQty;
                        qry2.ReceivedQty = qry2.ReceivedQty + updateQty;
                        qry2.Status = "Unfulfilled";
                    }
                }
            }
            context.SaveChanges();

        }


        public void confirmDisbursement(int departmentID)
        {
            int totalReceive = 0;

            var qry1 = context.Disbursements.Where(x => x.DepartmentID == departmentID).FirstOrDefault();
            var qry2 = context.Disbursement_Item.Where(y => y.DisbursementID == qry1.DisbursementID && y.Status == "Pending").ToList();
            foreach (var q in qry2)
            {
                q.Status = "Completed";
                context.SaveChanges();
            }

            var req = context.Requisitions.Where(x => x.DepartmentID == departmentID && x.Status.Equals("Approved")).ToList();
            foreach (var r in req)
            {

                var reqItem = context.Requisition_Item
                    .Where(x => x.RequisitionID == r.RequisitionID
                    && x.Status.Equals("Pending")).ToList();

                if (reqItem != null)
                {
                    foreach (var ri in reqItem)
                    {
                        totalReceive = (int)ri.ReceivedQty + (int)ri.RetrievedQty;

                        ri.ReceivedQty = totalReceive;
                        ri.UnfulfilledQty = ri.RequestedQty - totalReceive;
                        if (totalReceive == ri.RequestedQty)
                        {
                            ri.Status = "Completed";
                        }
                        else if (totalReceive < ri.RequestedQty)
                        {
                            ri.Status = "Unfulfilled";
                        }
                        context.SaveChanges();
                    }
                }
            }
            context.SaveChanges();
            totalReceive = 0;

        }
    


        public bool findDisbursementByDepID(int depID)
        {
            int disbursementID = context.Disbursements.Where(x => x.DepartmentID == depID).Select(x => x.DisbursementID).FirstOrDefault();
            List<Disbursement_Item> lbo = context.Disbursement_Item.Where(x => x.DisbursementID == disbursementID).ToList();
            List<Disbursement_Item> l = context.Disbursement_Item.Where(x => x.Disbursement_ItemID == disbursementID && x.Status == "Completed").ToList();
            if (lbo.Count == l.Count)
            {
                return false;

            }
            else
            {
                return true;
            }
        }


        public int addDisbursementItem(List<RetrievalBO> retrievalBOs, string statusPar)
        {
            string status = null;
            if (statusPar == "Unfullfill Orders")
            {
                status = "Unfulfilled";
            }
            else if (statusPar == "New Orders")
            {
                status = "New";
            }
            int changes;
            context = new ADTeam1Entities();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.Where(x => x.Status == status).ToList();
            List<Disbursement_Item> disbursement_Items = context.Disbursement_Item.ToList();
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                foreach (RetrievalBO retrievalBO in retrievalBOs)
                {
                    if (r_Item.Item.ItemNumber == retrievalBO.ItemNumber)
                    {
                        Disbursement_Item d_Item = new Disbursement_Item();
                        d_Item.Disbursement = r_Item.Requisition.Department.Disbursements.FirstOrDefault();
                        d_Item.Item = r_Item.Item;
                        d_Item.Qty = r_Item.ToRetrieveQty;
                        d_Item.Status = "Pending";
                        context.Disbursement_Item.Add(d_Item);
                    }
                }
            }
            changes = context.SaveChanges();
            return changes;
        }
    }
}
