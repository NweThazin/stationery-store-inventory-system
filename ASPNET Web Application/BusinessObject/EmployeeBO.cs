using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class EmployeeBO
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private string email;
        private int? phone;
        private string address;
        private int? departmentID;

        //Web
        private int serialNo;
        private string fullName;

        //Mobile
        private DateTime? startDate;
        private DateTime? endDate;
        private string primaryRole;
        private string delegatedRole;

        //check button
        private string checkPrimaryDelegate;

        public int EmployeeID
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

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public int? Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public int? DepartmentID
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

        public int SerialNo
        {
            get
            {
                return serialNo;
            }

            set
            {
                serialNo = value;
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

        public DateTime? StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public string PrimaryRole
        {
            get
            {
                return primaryRole;
            }

            set
            {
                primaryRole = value;
            }
        }

        public string DelegatedRole
        {
            get
            {
                return delegatedRole;
            }

            set
            {
                delegatedRole = value;
            }
        }

        public string CheckPrimaryDelegate
        {
            get
            {
                return checkPrimaryDelegate;
            }

            set
            {
                checkPrimaryDelegate = value;
            }
        }
    }
}
