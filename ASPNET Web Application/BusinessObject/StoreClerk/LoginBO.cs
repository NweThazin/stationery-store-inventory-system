using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class LoginBO
    {
        private int userID;
        private int empID;
        private int depID;

        private string deptName;
        private string depCode;
        private string empName;

        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public int EmpID
        {
            get
            {
                return empID;
            }

            set
            {
                empID = value;
            }
        }

        public int DepID
        {
            get
            {
                return depID;
            }

            set
            {
                depID = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }

            set
            {
                deptName = value;
            }
        }

        public string DepCode
        {
            get
            {
                return depCode;
            }

            set
            {
                depCode = value;
            }
        }

        public string EmpName
        {
            get
            {
                return empName;
            }

            set
            {
                empName = value;
            }
        }
    }
}
