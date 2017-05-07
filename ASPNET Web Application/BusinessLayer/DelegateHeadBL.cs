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
    public class DelegateHeadBL
    {
        EmployeeDA empDA = new EmployeeDA();
        UserDA userDA = new UserDA();
        public EmployeeBO getDepartmentDelegateHead(int departmentID)
        {
            //From Employee DA CLASS
            return empDA.getDepartmentDelegateHead(departmentID);
        }

        public void assignDeptDelegateHead(int empID,DateTime startDate,DateTime endDate)
        {
            userDA.assignDeptDelegateHead(empID, startDate, endDate);
        }

        public void removeDeptDelegateHead(int empID)
        {
            userDA.removeDeptDelegateHead(empID);
        }
        public EmployeeBO getEmpByID(int empID)
        {
            return userDA.getEmpInfoByID(empID);
        }
    }
}
