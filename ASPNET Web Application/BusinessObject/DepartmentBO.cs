using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class DepartmentBO
    {
        private int departmentID;
        private string deparmentCode;
        private string deparmentName;
        private int? phone;
        private int? collectionPointID;

        private int? contactEmpID;
        private int? headEmpID;
        private int? representativeEmpID;
        

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

        public string DeparmentCode
        {
            get
            {
                return deparmentCode;
            }

            set
            {
                deparmentCode = value;
            }
        }

        public string DeparmentName
        {
            get
            {
                return deparmentName;
            }

            set
            {
                deparmentName = value;
            }
        }

        public int? ContactEmpID
        {
            get
            {
                return contactEmpID;
            }

            set
            {
                contactEmpID = value;
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

        public int? HeadEmpID
        {
            get
            {
                return headEmpID;
            }

            set
            {
                headEmpID = value;
            }
        }

        public int? RepresentativeEmpID
        {
            get
            {
                return representativeEmpID;
            }

            set
            {
                representativeEmpID = value;
            }
        }

        public int? CollectionPointID
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
    }
}
