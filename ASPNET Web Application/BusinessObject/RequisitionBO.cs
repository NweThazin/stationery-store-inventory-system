using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class RequisitionBO
    {
        private int requisitionID;
        private int departmentID;
        private int? employeeID;
        private DateTime? orderDate;
        private string status;
        private string reason;

        private string reqID;
        private int totalItem;
        private string firstName;
        private string lastName;
        private string fullName;

        public int RequisitionID
        {
            get
            {
                return requisitionID;
            }

            set
            {
                requisitionID = value;
            }
        }

        public int DepartmentID
        {
            get
            {
                return departmentID;
            }

            set
            {
                departmentID = value;
            }
        }

        public int? EmployeeID
        {
            get
            {
                return employeeID;
            }

            set
            {
                employeeID = value;
            }
        }

        public DateTime? OrderDate
        {
            get
            {
                return orderDate;
            }

            set
            {
                orderDate = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string ReqID
        {
            get
            {
                return reqID;
            }

            set
            {
                reqID = value;
            }
        }

        public string Reason
        {
            get
            {
                return reason;
            }

            set
            {
                reason = value;
            }
        }

        public int TotalItem
        {
            get
            {
                return totalItem;
            }

            set
            {
                totalItem = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string FullName
        {
            get
            {
                return fullName;
            }

            set
            {
                fullName = value;
            }
        }
    }
}
