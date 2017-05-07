using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace DataAccess
{
    public class AdjustmentVouchersDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Adjsutment Vounchers ListforManager
        public List<AdjustmentBO> getAdjustmentByManagerandSupervisor(string status,int empID)
        {
            List<AdjustmentBO> lst = new List<AdjustmentBO>();
            if (status.Equals("manager"))
            {
                var qry = context.Adjustments.Where(x => x.Price >= 250 && x.Status.Equals("Pending") && x.EmployeeID != empID).ToList();
                foreach (var q in qry)
                {
                    AdjustmentBO a = new AdjustmentBO();
                    a.AdjustmentID = q.AdjustmentID;
                    a.AdjID = "A" + q.AdjustmentID;
                    a.Date = q.Date;
                    a.Status = q.Status;
                    lst.Add(a);
                }
            }
            else if (status.Equals("supervisor"))
            {
                var qry = context.Adjustments.Where(x => x.Price < 250 && x.Status.Equals("Pending") && x.EmployeeID != empID).ToList();
                foreach (var q in qry)
                {
                    AdjustmentBO a = new AdjustmentBO();
                    a.AdjustmentID = q.AdjustmentID;
                    a.AdjID = "A" + q.AdjustmentID;
                    a.Date = q.Date;
                    a.Status = q.Status;
                    lst.Add(a);
                }
            }
            return lst;
        }

        public List<AdjustmentBO> getAllAdjustmentList()
        {
            List<AdjustmentBO> lst = new List<AdjustmentBO>();
            var qry = context.Adjustments.ToList();//need to add emp id to check
            foreach (var q in qry)
            {
                AdjustmentBO a = new AdjustmentBO();
                a.AdjustmentID = q.AdjustmentID;
                a.AdjID = "A" + q.AdjustmentID;
                a.Date = q.Date;
                a.Status = q.Status;
                lst.Add(a);
            }
            return lst;
        }
        //Get the adjustment lists
        public List<AdjustmentVoucherDetailBO> getAdjustmentDetailed(int adjustmentID)
        {
            var qry = (from ai in context.Adjustment_Item
                       join i in context.Items on ai.ItemID equals i.ItemID
                       join a in context.Adjustments on ai.AdjustmentID equals a.AdjustmentID
                       where a.AdjustmentID == adjustmentID
                       select new { i.ItemNumber,i.Description,ai.AdjustedQty,ai.Reason,ai.Condition,i.ItemID }).ToList();

            List<AdjustmentVoucherDetailBO> lst = new List<AdjustmentVoucherDetailBO>();
            foreach (var q in qry)
            {
                AdjustmentVoucherDetailBO b = new AdjustmentVoucherDetailBO();
                b.ItemNumber = q.ItemNumber;
                b.ItemDescription = q.Description;
                b.QuantityAdjusted = Convert.ToString(q.AdjustedQty);
                b.Reason = q.Reason;
                lst.Add(b);
            }
            return lst;
        }
        //Get adjustment by id
        public AdjustmentBO getAdjustmentById(int number)
        {
            var q = context.Adjustments.Where(x => x.AdjustmentID == number).FirstOrDefault();
            AdjustmentBO a = new AdjustmentBO();
            a.AdjustmentID = q.AdjustmentID;
            a.Date = q.Date;
            a.EmpID = q.EmployeeID;
            a.Status = q.Status;
            return a;
        }
        //Change Approved or Rejected of adjustment table
        public int ApprovedOrRejected(int num, int adjNum)
        {
            
            //num==1 mean approved 
            var q = context.Adjustments.Where(x => x.AdjustmentID == adjNum).FirstOrDefault();
            
            int flag = 0;
            Adjustment a = (Adjustment)q;
            if (num == 1)
            {
                a.Status = "Approved";
                flag = context.SaveChanges();
            }
            else if (num == 0) //mean Rejected
            {
                a.Status = "Rejected";
                flag = context.SaveChanges();
            }
            return flag;
        }
        //Update in in stock quantity
        public int changeInStockByApproveAndReject(List<AdjustmentVoucherDetailBO> lst)
        {
            foreach (AdjustmentVoucherDetailBO l in lst)
            {
                int adjQty = int.Parse(l.QuantityAdjusted);
                var itemQry = context.Items.Where(x => x.ItemNumber.Equals(l.ItemNumber)).FirstOrDefault();
                if (adjQty < 0)
                {
                    itemQry.InStockQty = itemQry.InStockQty - adjQty;
                    context.SaveChanges();
                }
                else
                {
                    itemQry.InStockQty = itemQry.InStockQty + adjQty;
                    context.SaveChanges();
                }
               
            }
            return 1;
        }
        public int CheckPending(int adjNum)
        {
            var q = context.Adjustments.Where(x => x.AdjustmentID == adjNum).Select(x => x.Status).FirstOrDefault().ToString();
            if (q.Equals("Approved"))
            {
                return 1;
            }
            else if (q.Equals("Rejected"))
            {
                return 0;
            }
            else if (q.Equals("Pending"))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        //Get Name by Adjustment Number
        public string get(int adjNum)
        {
            int? empID = context.Adjustments.Where(x => x.AdjustmentID == adjNum).Select(x => x.EmployeeID).FirstOrDefault();
            var empName = (from x in context.Employees
                           where x.EmployeeID == empID
                           select new { x.FirstName, x.LastName }).FirstOrDefault();
            if (empName != null)
            {
                return empName.FirstName + " " + empName.LastName;
            }
            else
            {
                return "";
            }
        }
        //get the employee by ID
        public EmployeeBO getEmpInfoByAdjsutmentID(int adjNum)
        {
            int? empID = context.Adjustments.Where(x => x.AdjustmentID == adjNum).Select(x => x.EmployeeID).FirstOrDefault();
            var emp = (from i in context.Employees
                       where i.EmployeeID == empID
                       select new { i.FirstName, i.LastName, i.Email }).FirstOrDefault();
            EmployeeBO e = new EmployeeBO();
            e.FullName = emp.FirstName + " " + emp.LastName;
            e.Email = emp.Email;
            return e;
        }
    }
}
