using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;

namespace BusinessLayer
{
    public class EmployeeBL
    {
        EmployeeDA da = new EmployeeDA();
        LoginDA loginDA = new LoginDA();
        UserDA userDA = new UserDA();
        public List<EmployeeBO> getEmpList()
        {
            return da.getEmployeeList();
        }

        //Change Representatvie Page
        //1.. Get Department Employees ID this method is accessed by delegate head and change rep pages
        public List<EmployeeBO> getDepartmentEmployees(int departmentID,string status)
        {
            List<Employee> elst = new List<Employee>();
            List<EmployeeBO> eboLst = new List<EmployeeBO>();
            int count = 0;

            if (status.Equals("head"))
            {
                elst = da.getDeptEmployeeForDelegateHead(departmentID); //For Delegated Head Page
            }
            else if (status.Equals("rep"))
            {
                elst = da.getDepartmentEmployees(departmentID); //For Change Rep Page
            }
            foreach (Employee e in elst)
            {
                count++;
                EmployeeBO emp = new EmployeeBO();
                emp.SerialNo = count;
                emp.EmployeeID = e.EmployeeID;
                emp.FirstName = e.FirstName;
                emp.LastName = e.LastName;
                emp.FullName = e.FirstName + " " + e.LastName;
                emp.Email = e.Email;
                emp.Phone = e.Phone;
                emp.Address = e.Address;
                emp.DepartmentID = e.DepartmentID;
                eboLst.Add(emp);
            }
            return eboLst;
        }
        //Get Department Rep BY Dept ID (For Change Repr) //need to ask
        public EmployeeBO getDepartmentRep(int departmentID)
        {
            Employee e = da.getDepartmentRep(departmentID);
            EmployeeBO emp = new EmployeeBO();
            emp.EmployeeID = e.EmployeeID;
            emp.FirstName = e.FirstName;
            emp.LastName = e.LastName;
            emp.Email = e.Email;
            emp.Phone = e.Phone;
            emp.Address = e.Address;
            emp.DepartmentID = e.DepartmentID;
            return emp;
        }

        //3.. Assign Department Rep
        public void assignDeptRep(int employeeID)
        {
            userDA.assignDeptRep(employeeID);
        }
        //4.. Remove Department Rep
        public void removeDeptRep(int employeeid)
        {
            userDA.removeDeptRep(employeeid);
        }
        
        //Need to ask
        //For Change Representative page to show emp rep
        public EmployeeBO getEmpRepNameByID(int departmentID)
        {
            return loginDA.getDelegateRoleNameByDeptID(departmentID);
        }
    }
}
