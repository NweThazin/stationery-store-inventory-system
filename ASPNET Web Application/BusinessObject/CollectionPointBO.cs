using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CollectionPointBO
    {
        private int collectionPointID;
        private string name;
        private TimeSpan? time;
        private int? employeeID;
        private string employeeName;
        private int departmentID;
        private string departmentName;
        private string empFirstName;
        private string empLastName;
        private int? phone;
        private int totalItem;

        public int CollectionPointID
        {
            get
            {
                return collectionPointID;
            }

            set
            {
                collectionPointID = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public TimeSpan? Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
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

        public string EmployeeName
        {
            get
            {
                return employeeName;
            }

            set
            {
                employeeName = value;
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

        public string DepartmentName
        {
            get
            {
                return departmentName;
            }

            set
            {
                departmentName = value;
            }
        }

        public string EmpFirstName
        {
            get
            {
                return empFirstName;
            }

            set
            {
                empFirstName = value;
            }
        }

        public string EmpLastName
        {
            get
            {
                return empLastName;
            }

            set
            {
                empLastName = value;
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
    }
}
