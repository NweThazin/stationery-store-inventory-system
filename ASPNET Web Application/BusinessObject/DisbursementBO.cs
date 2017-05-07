using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class DisbursementBO
    {
        private int disbursementID;
        private DateTime? collectionDate;
        private int? departmentID;
        private string status;
        private int disbursementItemID;
        private string itemDescription;
        private int qty;
        private string uom;
        private string itemNumber;
        private int? itemID;

        public int DisbursementID
        {
            get
            {
                return disbursementID;
            }

            set
            {
                disbursementID = value;
            }
        }

        public DateTime? CollectionDate
        {
            get
            {
                return collectionDate;
            }

            set
            {
                collectionDate = value;
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

        public int DisbursementItemID
        {
            get
            {
                return disbursementItemID;
            }

            set
            {
                disbursementItemID = value;
            }
        }

        public string ItemDescription
        {
            get
            {
                return itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }

        public int Qty
        {
            get
            {
                return qty;
            }

            set
            {
                qty = value;
            }
        }

        public string Uom
        {
            get
            {
                return uom;
            }

            set
            {
                uom = value;
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

        public int? ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }
    }
}
