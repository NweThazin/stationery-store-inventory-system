using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class UserDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Change Representative Page
        //3.. Assign Department Rep
        public void assignDeptRep(int employeeID)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeID).ToList().FirstOrDefault();
            if (qry != null)
            {
                //qry.DelegatedRole = "Employee Rep";
                if (qry.DelegatedRole.Equals("Employee Head"))
                {
                    qry.DelegatedRole = "Employee RepHead";
                }
                else
                {
                    qry.DelegatedRole = "Employee Rep";
                }
            }
            context.SaveChanges();
        }
        //4.. Remove DepartmentRep
        public void removeDeptRep(int employeeid)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeid).ToList().FirstOrDefault();
            if (qry != null)
            {
                //qry.DelegatedRole = "";
                if (qry.DelegatedRole.Equals("Employee RepHead"))
                {
                    qry.DelegatedRole = "Employee Head";
                }
                else
                {
                    qry.DelegatedRole = "";
                }
            }
            context.SaveChanges();
        }

        //For Delegate Head Page
        public void assignDeptDelegateHead(int employeeid,DateTime startDate,DateTime endDate)
        {
            //delegated role -- employee rep 
            //yes - employee rephead
            //no - employee head

            var qry = context.Users.Where(u => u.EmployeeID == employeeid).ToList().FirstOrDefault();

            if (qry.DelegatedRole.Equals("Employee Rep"))
            {
                qry.DelegatedRole = "Employee RepHead";
                qry.StartDate = startDate;
                qry.EndDate = endDate;
            }
            else
            {
                qry.DelegatedRole = "Employee Head";
                qry.StartDate = startDate;
                qry.EndDate = endDate;
            }
     
            context.SaveChanges();
        }

        public void removeDeptDelegateHead(int employeeid)
        {
            //delegated role -- employee repHead? yes - employee rep
            //delegated role -- employee head? yes - remove 
            var qry = context.Users.Where(u => u.EmployeeID == employeeid).ToList().FirstOrDefault();
            if (qry.DelegatedRole.Equals("Employee RepHead"))
            {
                qry.DelegatedRole = "Employee Rep";
            }
            else if (qry.DelegatedRole.Equals("Employee Head"))
            {
                qry.DelegatedRole = "";
            }
            context.SaveChanges();
        }

        public EmployeeBO getEmpInfoByID(int empID)
        {
            Employee emp = context.Employees.Where(x => x.EmployeeID == empID).FirstOrDefault();
            EmployeeBO e = new EmployeeBO();
            e.EmployeeID = emp.EmployeeID;
            e.FirstName = emp.FirstName;
            e.LastName = emp.LastName;
            e.Email = emp.Email;
            e.Phone = emp.Phone;
            e.Address = emp.Address;
            e.DepartmentID = emp.DepartmentID;
            return e;
        }
    }
}
