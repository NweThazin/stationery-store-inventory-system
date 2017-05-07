using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace DataAccess
{
    public class EmployeeDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //get employee lists
        public List<EmployeeBO> getEmployeeList()
        {
            List<EmployeeBO> lst = new List<EmployeeBO>();
            var qry = context.Employees.ToList();
            foreach (var q in qry)
            {
                EmployeeBO b = new EmployeeBO();
                b.EmployeeID = q.EmployeeID;
                b.FirstName = q.FirstName;
                b.LastName = q.LastName;
                b.Email = q.Email;
                b.Phone = q.Phone;
                b.Address = q.Address;
                b.DepartmentID = q.DepartmentID;
                lst.Add(b);
            }
            return lst;
        }
        public Employee getEmployeeByID(int ID)
        {
            Employee employee = new Employee();
            employee = context.Employees.Where(x => x.EmployeeID == ID).FirstOrDefault();
            return employee;
        }

        //For Change Representative Page
        //1.. Get department employees lists by deaprtment id expect primary role dept head
        public List<Employee> getDepartmentEmployees(int departmentID)
        {
            List<Employee> elst = new List<Employee>();
            var qry = (from e in context.Employees
                       join u in context.Users on e.EmployeeID equals u.EmployeeID 
                       where e.DepartmentID == departmentID && !u.PrimaryRole.Equals("Dept Head") && !u.DelegatedRole.Equals("Employee Rep")
                       select new { e.EmployeeID,e.FirstName,e.LastName,e.Email,e.Phone,e.Address,e.DepartmentID }).ToList();

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
            return elst;
        }
        //For Delegate Head Page //Add New Function
        public List<Employee> getDeptEmployeeForDelegateHead(int departmentID)
        {
            List<Employee> elst = new List<Employee>();
            var qry = (from e in context.Employees
                       join u in context.Users on e.EmployeeID equals u.EmployeeID
                       where e.DepartmentID == departmentID && !u.PrimaryRole.Equals("Dept Head") && !u.DelegatedRole.Equals("Employee Head")
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
            return elst;
        }

        //2.. need to ask
        public Employee getDepartmentRep(int departmentID) //ask
        {
            var qry = context.Employees.Join(context.Users,
                r => r.EmployeeID, p => p.EmployeeID,
                (r, p) => new { r.EmployeeID, r.FirstName, r.LastName, r.Email, r.Phone, r.Address, r.DepartmentID, p.DelegatedRole })
                .Where(r => r.DepartmentID == departmentID)
                .Where(p => p.DelegatedRole == "Employee Rep")
                .FirstOrDefault();

            Employee emp = new Employee();
            emp.EmployeeID = qry.EmployeeID;
            emp.FirstName = qry.FirstName;
            emp.LastName = qry.LastName;
            emp.Email = qry.Email;
            emp.Phone = qry.Phone;
            emp.Address = qry.Address;
            emp.DepartmentID = qry.DepartmentID;

            return emp;
        }

        //For Delegate Head Page
        public EmployeeBO getDepartmentDelegateHead(int departmentID)
        {
            EmployeeBO emp = new EmployeeBO();
            var checkdelegated_qry = context.Employees.Join(context.Users,
                r => r.EmployeeID, p => p.EmployeeID,
                (r, p) => new { r.EmployeeID, r.FirstName, r.LastName, r.Email, r.Phone, r.Address, r.DepartmentID, p.PrimaryRole, p.DelegatedRole, p.StartDate, p.EndDate })
                .Where(r => r.DepartmentID == departmentID)
                .Where(p => p.DelegatedRole.Equals("Employee Head") || p.DelegatedRole.Equals("Employee RepHead"))
                .FirstOrDefault();

            var checkprimary_qry = context.Employees.Join(context.Users,
                 r => r.EmployeeID, p => p.EmployeeID,
                 (r, p) => new { r.EmployeeID, r.FirstName, r.LastName, r.Email, r.Phone, r.Address, r.DepartmentID, p.PrimaryRole, p.DelegatedRole, p.StartDate, p.EndDate })
                 .Where(r => r.DepartmentID == departmentID)
                 .Where(p => p.PrimaryRole.Equals("Dept Head"))
                 .FirstOrDefault();

            if (checkdelegated_qry != null)
            {
                var qry = checkdelegated_qry;
                emp.EmployeeID = qry.EmployeeID;
                emp.FirstName = qry.FirstName;
                emp.LastName = qry.LastName;
                emp.Email = qry.Email;
                emp.Phone = qry.Phone;
                emp.Address = qry.Address;
                emp.DepartmentID = qry.DepartmentID;
                emp.StartDate = qry.StartDate;
                emp.EndDate = qry.EndDate;
                emp.PrimaryRole = qry.PrimaryRole;
                emp.DelegatedRole = qry.DelegatedRole;
            }
            else
            {
                var qry = checkprimary_qry;
                emp.EmployeeID = qry.EmployeeID;
                emp.FirstName = qry.FirstName;
                emp.LastName = qry.LastName;
                emp.Email = qry.Email;
                emp.Phone = qry.Phone;
                emp.Address = qry.Address;
                emp.DepartmentID = qry.DepartmentID;
                emp.StartDate = qry.StartDate;
                emp.EndDate = qry.EndDate;
                emp.PrimaryRole = qry.PrimaryRole;
                emp.DelegatedRole = qry.DelegatedRole;
            }
            return emp;
        }
    }
}