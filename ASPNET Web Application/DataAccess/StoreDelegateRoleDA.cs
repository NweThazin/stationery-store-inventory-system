using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObject;
using DataAccess;
using BusinessObject.StoreClerk;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StoreDelegateRoleDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
       
        //Get Store Employee List except Store Manager and Store Supervisor

        //For Manager
        public List<Employee> getDepartmentStoreEmployeeForManager(int departmentID)
        {
            List<Employee> elst = new List<Employee>();
            var qry = (from e in context.Employees
                       join u in context.Users on e.EmployeeID equals u.EmployeeID
                       where e.DepartmentID == departmentID && 
                       (!u.PrimaryRole.Equals("Store Manager") && !u.PrimaryRole.Equals("Store Supervisor")) && 
                       !u.DelegatedRole.Equals("Store Supervisor") && !u.DelegatedRole.Equals("Store Manager")
                       select new { e.EmployeeID, e.FirstName, e.LastName, e.Email, e.Phone, e.Address, e.DepartmentID }).ToList();
            foreach (var q in qry)
            {
                Employee e = new Employee();
                e.EmployeeID = q.EmployeeID;
                e.FirstName = q.FirstName;
                e.LastName = q.LastName;
                e.Email = q.Email;
                e.Phone = q.Phone;
                e.Address = q.Address;
                e.DepartmentID = q.DepartmentID;
                elst.Add(e);
            }
            return elst; //return the l
        }

        //For Supervisor
        public List<Employee> getDepartmentStoreEmployeeForSupervisor(int departmentID)
        {
            List<Employee> elst = new List<Employee>();
            var qry = (from e in context.Employees
                       join u in context.Users on e.EmployeeID equals u.EmployeeID
                       where e.DepartmentID == departmentID && !u.DelegatedRole.Equals("Store Supervisor") && //Need to confirm 
                       (!u.PrimaryRole.Equals("Store Manager") && !u.PrimaryRole.Equals("Store Supervisor")) && !u.DelegatedRole.Equals("Store Manager")
                       select new { e.EmployeeID, e.FirstName, e.LastName, e.Email, e.Phone, e.Address, e.DepartmentID }).ToList();

            foreach (var q in qry)
            {
                Employee e = new Employee();
                e.EmployeeID = q.EmployeeID;
                e.FirstName = q.FirstName;
                e.LastName = q.LastName;
                e.Email = q.Email;
                e.Phone = q.Phone;
                e.Address = q.Address;
                e.DepartmentID = q.DepartmentID;
                elst.Add(e);
            }
            return elst; //return the l
        }

        //Get department supervisor name by department ID
        public EmployeeBO getDepartmentSupervisor(int departmentID)
        {
            var qry = (from u in context.Users
                       join e in context.Employees on u.EmployeeID equals e.EmployeeID
                       join d in context.Departments on e.DepartmentID equals d.DepartmentID
                       where d.DepartmentID == departmentID && (u.DelegatedRole.Equals("Store Supervisor"))
                       select new { e.FirstName, e.LastName, e.EmployeeID,e.Email,e.Phone,e.Address,e.DepartmentID,u.PrimaryRole,u.DelegatedRole ,u.StartDate, u.EndDate}).FirstOrDefault();
            if (qry == null)
            {
                var qryPrimary = (from u in context.Users
                                  join e in context.Employees on u.EmployeeID equals e.EmployeeID
                                  join d in context.Departments on e.DepartmentID equals d.DepartmentID
                                  where d.DepartmentID == departmentID && (u.PrimaryRole.Equals("Store Supervisor"))
                                  select new { e.FirstName, e.LastName, e.EmployeeID, e.Email, e.Phone, e.Address, e.DepartmentID, u.PrimaryRole, u.DelegatedRole, u.StartDate, u.EndDate }).FirstOrDefault();

                EmployeeBO emp = new EmployeeBO();
                emp.EmployeeID = qryPrimary.EmployeeID;
                emp.FirstName = qryPrimary.FirstName;
                emp.LastName = qryPrimary.LastName;
                emp.Email = qryPrimary.Email;
                emp.Phone = qryPrimary.Phone;
                emp.Address = qryPrimary.Address;
                emp.DepartmentID = qryPrimary.DepartmentID;
                emp.PrimaryRole = qryPrimary.PrimaryRole;
                emp.DelegatedRole = qryPrimary.DelegatedRole;
                emp.StartDate = qryPrimary.StartDate;
                emp.EndDate = qryPrimary.EndDate;
                emp.FullName = qryPrimary.FirstName + " " + qryPrimary.LastName;
                return emp;
            }
            else
            {
                EmployeeBO emp = new EmployeeBO();
                emp.EmployeeID = qry.EmployeeID;
                emp.FirstName = qry.FirstName;
                emp.LastName = qry.LastName;
                emp.Email = qry.Email;
                emp.Phone = qry.Phone;
                emp.Address = qry.Address;
                emp.DepartmentID = qry.DepartmentID;
                emp.PrimaryRole = qry.PrimaryRole;
                emp.DelegatedRole = qry.DelegatedRole;
                emp.StartDate = qry.StartDate;
                emp.EndDate = qry.EndDate;
                emp.FullName = qry.FirstName + " " + qry.LastName;
                return emp;
            }
        }

        //Assign Supervisor
        public void assignSupervisor(int employeeID,DateTime startDate,DateTime endDate)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeID).ToList().FirstOrDefault();
            if (qry != null)
            { 
                if (qry.DelegatedRole.Equals(""))
                {
                    qry.DelegatedRole = "Store Supervisor";
                    qry.StartDate = startDate;
                    qry.EndDate = endDate;
                }
            }
            context.SaveChanges();
        }
        //Remove Supervisor
        public void removeDeptRep(int employeeid)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeid).ToList().FirstOrDefault();
            if (qry != null)
            {
                if (qry.DelegatedRole.Equals("Store Supervisor"))
                {
                    qry.DelegatedRole = "";
                }
            }
            context.SaveChanges();
        }

        //For delegate Manager
        public EmployeeBO getDepartmentManager(int departmentID)
        {
            var qry = (from u in context.Users
                       join e in context.Employees on u.EmployeeID equals e.EmployeeID
                       join d in context.Departments on e.DepartmentID equals d.DepartmentID
                       where d.DepartmentID == departmentID && (u.DelegatedRole.Equals("Store Manager"))
                       select new { e.FirstName, e.LastName, e.EmployeeID, e.Email, e.Phone, e.Address, e.DepartmentID,u.PrimaryRole,u.DelegatedRole,u.StartDate,u.EndDate }).FirstOrDefault();
            if (qry == null)
            {
                var qryPrimary = (from u in context.Users
                                  join e in context.Employees on u.EmployeeID equals e.EmployeeID
                                  join d in context.Departments on e.DepartmentID equals d.DepartmentID
                                  where d.DepartmentID == departmentID && (u.PrimaryRole.Equals("Store Manager"))
                                  select new { e.FirstName, e.LastName, e.EmployeeID, e.Email, e.Phone, e.Address, e.DepartmentID, u.PrimaryRole, u.DelegatedRole, u.StartDate, u.EndDate }).FirstOrDefault();

                EmployeeBO emp = new EmployeeBO();
                emp.EmployeeID = qryPrimary.EmployeeID;
                emp.FirstName = qryPrimary.FirstName;
                emp.LastName = qryPrimary.LastName;
                emp.Email = qryPrimary.Email;
                emp.Phone = qryPrimary.Phone;
                emp.Address = qryPrimary.Address;
                emp.DepartmentID = qryPrimary.DepartmentID;
                emp.PrimaryRole = qryPrimary.PrimaryRole;
                emp.DelegatedRole = qryPrimary.DelegatedRole;
                emp.StartDate = qryPrimary.StartDate;
                emp.EndDate = qryPrimary.EndDate;
                emp.FullName = qryPrimary.FirstName + " " + qryPrimary.LastName;
                return emp;
            }
            else
            {
                EmployeeBO emp = new EmployeeBO();
                emp.EmployeeID = qry.EmployeeID;
                emp.FirstName = qry.FirstName;
                emp.LastName = qry.LastName;
                emp.Email = qry.Email;
                emp.Phone = qry.Phone;
                emp.Address = qry.Address;
                emp.DepartmentID = qry.DepartmentID;
                emp.PrimaryRole = qry.PrimaryRole;
                emp.DelegatedRole = qry.DelegatedRole;
                emp.StartDate = qry.StartDate;
                emp.EndDate = qry.EndDate;
                emp.FullName = qry.FirstName + " " + qry.LastName;
                return emp;
            }
        }

        public void assignDeptManager(int employeeID, DateTime startDate, DateTime endDate)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeID).ToList().FirstOrDefault();
            if (qry != null)
            {
                if (qry.DelegatedRole.Equals(""))
                {
                    qry.DelegatedRole = "Store Manager";
                    qry.StartDate = startDate;
                    qry.EndDate = endDate;
                }
            }
            context.SaveChanges();
        }

        public void removeDeptManger(int employeeID)
        {
            var qry = context.Users.Where(u => u.EmployeeID == employeeID).ToList().FirstOrDefault();
            if (qry != null)
            {
                //qry.DelegatedRole = "";
                if (qry.DelegatedRole.Equals("Store Manager"))
                {
                    qry.DelegatedRole = "";
                }
            }
            context.SaveChanges();
        }
    }
}
