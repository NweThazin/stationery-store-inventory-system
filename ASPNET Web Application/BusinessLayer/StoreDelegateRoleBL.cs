using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace BusinessLayer
{
    public class StoreDelegateRoleBL
    {
        StoreDelegateRoleDA da = new StoreDelegateRoleDA();
        //For Manager
        public List<EmployeeBO> getDepartmentStoreEmployeeForManager(int departmentID)
        {
            List<Employee> elst = da.getDepartmentStoreEmployeeForManager(departmentID);
            List<EmployeeBO> eboLst = new List<EmployeeBO>();
            int count = 0;
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

        public List<EmployeeBO> getDepartmentStoreEmployeeForSupervisor(int departmentID)
        {
            List<Employee> elst = da.getDepartmentStoreEmployeeForSupervisor(departmentID);
            List<EmployeeBO> eboLst = new List<EmployeeBO>();
            int count = 0;
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
        //Get Department Current Supervisor Name
        public EmployeeBO getDepartmentSupervisor(int departmentID)
        {
            return da.getDepartmentSupervisor(departmentID);
        }
        //Assign Supervisor
        public void assignSupervisor(int employeeID, DateTime startDate, DateTime endDate)
        {
            da.assignSupervisor(employeeID,startDate,endDate);
        }
        //Remove Supervisor
        public void removeDeptRep(int employeeid)
        {
            da.removeDeptRep(employeeid);
        }

        //Get the department Manger name
        public EmployeeBO getDepartmentManager(int departmentID)
        {
            return da.getDepartmentManager(departmentID);
        }
        //Assign Department Manager
        public void assignDeptManager(int employeeID, DateTime startDate, DateTime endDate)
        {
            da.assignDeptManager(employeeID, startDate, endDate);
        }
        //Remove Department Manager
        public void removeDeptManager(int employeeID)
        {
            da.removeDeptManger(employeeID);
        }
    }
}
