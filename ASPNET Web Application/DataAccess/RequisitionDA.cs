using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.Globalization;

namespace DataAccess
{
    public class RequisitionDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Requistion Page
        public List<RequisitionBO> getRequistionList(int depID)
        {
            var qry = (from i in context.Employees
                       join si in context.Requisitions on i.EmployeeID equals si.EmployeeID
                       where i.DepartmentID == depID //loggined employee deparment id
                       select new { si.RequisitionID, si.DepartmentID, si.EmployeeID, si.OrderDate, si.Status, i.FirstName, i.LastName }).ToList();
            List<RequisitionBO> lst = new List<RequisitionBO>();
            foreach (var q in qry)
            {
                RequisitionBO b = new RequisitionBO();
                b.RequisitionID = q.RequisitionID;
                b.ReqID = "R" + q.RequisitionID;
                b.DepartmentID = q.DepartmentID;
                b.EmployeeID = q.EmployeeID;
                b.OrderDate = q.OrderDate;
                b.Status = q.Status;
                b.FullName = q.FirstName + " " + q.LastName;
                lst.Add(b);
            }
            return lst;
        }

        //Get the requisition object by Number
        public RequisitionBO getRequisitionIDByNumber(int rid)
        {
            var qry = context.Requisitions.Where(x => x.RequisitionID == rid).FirstOrDefault();
            RequisitionBO rbo = new RequisitionBO();
            rbo.ReqID = "R" + qry.RequisitionID;
            rbo.OrderDate = qry.OrderDate;
            rbo.Status = qry.Status;
            return rbo;
        }
        
        //For detailed information Requisition Detailed Page
        public List<Requisition_ItemBO> getRequisitionDetail(int reqID)
        {
            var qry = (from i in context.Items
                       join si in context.Requisition_Item on i.ItemID equals si.ItemID
                       where si.RequisitionID == reqID
                       select new { si.ItemID, i.ItemNumber, i.Description, si.RequestedQty, si.ReceivedQty, i.UnitOfMeasure,si.Status }).ToList();
            List<Requisition_ItemBO> lst = new List<Requisition_ItemBO>();
            foreach (var q in qry)
            {
                Requisition_ItemBO b = new Requisition_ItemBO();
                b.ItemID = q.ItemID;
                b.Description = q.Description;
                b.RequestedQty = q.RequestedQty;
                b.ReceivedQty = q.ReceivedQty;
                b.Uom = q.UnitOfMeasure;
                b.ItemNumber = q.ItemNumber;
                b.Status = q.Status;
                lst.Add(b);
            }
            return lst;
        }

        //Search By Date
        public List<RequisitionBO> searchRequisitionByDate(string fromDate, string toDate,int deptID)
        {
            DateTime dt1 = DateTime.Parse(fromDate);
            DateTime dt2 = DateTime.Parse(toDate);

            var qry = (from i in context.Employees
                       join si in context.Requisitions on i.EmployeeID equals si.EmployeeID
                       where i.DepartmentID == deptID && (si.OrderDate >= dt1 && si.OrderDate <= dt2) //added dept id by parameter pass
                       select new { si.RequisitionID, si.DepartmentID, si.EmployeeID, si.OrderDate, si.Status,i.FirstName,i.LastName }).ToList();
            List<RequisitionBO> lst = new List<RequisitionBO>();
            foreach (var q in qry)
            {
                RequisitionBO b = new RequisitionBO();
                b.ReqID = "R" + q.RequisitionID;
                b.DepartmentID = q.DepartmentID;
                b.EmployeeID = q.EmployeeID;
                b.OrderDate = q.OrderDate;
                b.Status = q.Status;
                b.FullName = q.FirstName + " " + q.LastName;
                lst.Add(b);
            }
            return lst;
        }

        //Employee -- Approve Requisition Page --
        //1.. Get Department Requistions which status is "Pending"
        public List<RequisitionBO> getDeptPendingRequisition(int departmentID)
        {
            List<RequisitionBO> lst = new List<RequisitionBO>();

            var qry = (from r in context.Requisitions
                       join ri in context.Requisition_Item on r.RequisitionID equals ri.RequisitionID
                       join emp in context.Employees on r.EmployeeID equals emp.EmployeeID
                       where (r.DepartmentID == departmentID && r.Status.Equals("Pending"))
                       group new { r, ri, emp } by r.RequisitionID into g
                       select new
                       {
                           g.FirstOrDefault().r.RequisitionID,
                           totalCount = g.Count(),
                           g.FirstOrDefault().emp.FirstName,
                           g.FirstOrDefault().emp.LastName,
                           g.FirstOrDefault().r.OrderDate
                       }).ToList();

            foreach (var q in qry)
            {
                RequisitionBO reqBO = new RequisitionBO();
                reqBO.RequisitionID = q.RequisitionID;
                reqBO.TotalItem = q.totalCount;
                reqBO.FirstName = q.FirstName;
                reqBO.LastName = q.LastName;
                reqBO.OrderDate = q.OrderDate;
                reqBO.FullName = q.FirstName + " " + q.LastName; //For Full Name
                lst.Add(reqBO);
            }
            return lst;
            //return context.Requisitions.Where(r => r.DepartmentID == 2).ToList<Requisition>();
        }

        //Employee --  Approve Requisition Detail Page --
        //1.. Get Requistion Item List by Requisition ID for detail page
        public List<Requisition_ItemBO> getRequisitionItem(int requisitionID)
        {
            List<Requisition_ItemBO> lst = new List<Requisition_ItemBO>();
            var qry = (from r in context.Requisitions
                       join ri in context.Requisition_Item on r.RequisitionID equals ri.RequisitionID
                       join i in context.Items on ri.ItemID equals i.ItemID
                       where r.RequisitionID == requisitionID
                       select new { r.RequisitionID, ri.Requisition_ItemID, ri.ItemID, ri.RequestedQty, i.Description, i.UnitOfMeasure })
                       .ToList();

            foreach (var q in qry)
            {
                Requisition_ItemBO reqItemBO = new Requisition_ItemBO();
                reqItemBO.RequisitionID = q.RequisitionID;
                reqItemBO.Requisition_ItemID = q.Requisition_ItemID;
                reqItemBO.ItemID = q.ItemID;
                reqItemBO.RequestedQty = q.RequestedQty;
                reqItemBO.Description = q.Description;
                reqItemBO.Uom = q.UnitOfMeasure;
                lst.Add(reqItemBO);
            }
            return lst;
        }

        //2.. Approve Requisition
        public void approveRequisition(int reqID)
        {
            //Get the requisition object by req ID
            var qry = context.Requisitions.Where(r => r.RequisitionID == reqID).ToList().FirstOrDefault();
            if (qry != null)
            {
                qry.Status = "Approved";
            }
            context.SaveChanges();
        }

        //3.. Reject Requisition
        public void rejectRequisition(int reqID,string reason)
        {
            var qry = context.Requisitions.Where(r => r.RequisitionID == reqID).ToList().FirstOrDefault();
            if (qry != null)
            {
                qry.Status = "Rejected";
                qry.Reason = reason;
            }
            context.SaveChanges();
        }

        //Get employee Name
        public string getEmpName(int reqID)
        {
            int? empID = context.Requisitions.Where(x => x.RequisitionID == reqID).Select(x => x.EmployeeID).FirstOrDefault();
            var q = (from x in context.Employees
                     where x.EmployeeID == empID
                     select new { x.FirstName, x.LastName }).FirstOrDefault();
            string name = q.FirstName + " " + q.LastName;
            return name;
        }

        public string getEMail(int reqID)
        {
            int? empID = context.Requisitions.Where(x => x.RequisitionID == reqID).Select(x => x.EmployeeID).FirstOrDefault();
            var q = (from x in context.Employees
                     where x.EmployeeID == empID
                     select new { x.Email }).FirstOrDefault();
            string email = q.Email;
            return email;
        }
    }
}
