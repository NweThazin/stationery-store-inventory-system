using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace DataAccess
{
    public class LoginDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Login Page
        //1.. Check the user is correct or not
        public User LoggedInUser(string userName, string password)
        {
            var qry = context.Users.Where(x => x.UserName.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
            if (qry != null)
            {
                if (qry.UserName.Equals(userName) && qry.Password.Equals(password))
                {
                    User u = new User();
                    u.UserID = qry.UserID;
                    u.UserName = qry.UserName;
                    u.Password = qry.Password;
                    u.PrimaryRole = qry.PrimaryRole;
                    u.DelegatedRole = qry.DelegatedRole;
                    u.EmployeeID = qry.EmployeeID;
                    u.StartDate = qry.StartDate;
                    u.EndDate = qry.EndDate;
                    return u;
                }
            }
            return null;
        }
        //2.. Get Login User Information
        public LoginBO getLoginInfo(int userID)
        {
            var qry = (from u in context.Users
                       join e in context.Employees on u.EmployeeID equals e.EmployeeID
                       join d in context.Departments on e.DepartmentID equals d.DepartmentID
                       where u.UserID == userID
                       select new { u.UserID, e.EmployeeID, d.DepartmentID, d.DepartmentName, d.DepartmentCode,e.FirstName,e.LastName }).FirstOrDefault();

            LoginBO lbo = new LoginBO();
            lbo.UserID = qry.UserID;
            lbo.EmpID = qry.EmployeeID;
            lbo.DepID = qry.DepartmentID;
            lbo.DeptName = qry.DepartmentName;
            lbo.DepCode = qry.DepartmentCode;
            lbo.EmpName = qry.FirstName + " " + qry.LastName;
            return lbo;
        }

        //For Change Representative page
        //1.. Get the emplyee by department id (Show Department Rep Name which delegate role is employee rep or employee rep head)
        public EmployeeBO getDelegateRoleNameByDeptID(int departmentID)
        {
            var qry = (from u in context.Users
                       join e in context.Employees on u.EmployeeID equals e.EmployeeID
                       join d in context.Departments on e.DepartmentID equals d.DepartmentID
                       where d.DepartmentID == departmentID && (u.DelegatedRole.Equals("Employee Rep") || u.DelegatedRole.Equals("Employee RepHead"))
                       select new { e.FirstName,e.LastName,e.EmployeeID}).FirstOrDefault();

            EmployeeBO emp = new EmployeeBO();
            emp.FullName = qry.FirstName + " " + qry.LastName;
            emp.EmployeeID = qry.EmployeeID;
            return emp;
        }
    }
}
