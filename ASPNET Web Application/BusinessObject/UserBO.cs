using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class UserBO
    {
        private string sessionID;
        private int userID;
        private string userName;
        private string password;
        private string primaryRole;
        private string delegatedRole;
        private int? employeeID;
        private DateTime? startDate;
        private DateTime? endDate;

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

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
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

        public string SessionID
        {
            get
            {
                return sessionID;
            }

            set
            {
                sessionID = value;
            }
        }
    }
}
