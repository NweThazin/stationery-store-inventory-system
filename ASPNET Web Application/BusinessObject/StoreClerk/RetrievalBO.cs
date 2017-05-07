using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class RetrievalBO
    {
        private int requisitionItemID;
        private string bin;
        private string itemNumber;
        private string description;
        private string uOM;
        private int inStock;
        private int requested;
        private int toRetrieve;
        private int departmentID;
        private string departmentName;
        private string status;
        private int unfulfilled;
        public string Bin
        {
            get
            {
                return bin;
            }

            set
            {
                bin = value;
            }
        }

        public string ItemNumber
        {
            get
            {
                return itemNumber;
            }

            set
            {
                itemNumber = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string UOM
        {
            get
            {
                return uOM;
            }

            set
            {
                uOM = value;
            }
        }

        public int InStock
        {
            get
            {
                return inStock;
            }

            set
            {
                inStock = value;
            }
        }

        public int Requested
        {
            get
            {
                return requested;
            }

            set
            {
                requested = value;
            }
        }

        public int ToRetrieve
        {
            get
            {
                return toRetrieve;
            }

            set
            {
                toRetrieve = value;
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

        public int RequisitionItemID
        {
            get
            {
                return requisitionItemID;
            }

            set
            {
                requisitionItemID = value;
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

        public int Unfulfilled
        {
            get
            {
                return unfulfilled;
            }

            set
            {
                unfulfilled = value;
            }
        }
    }
}
