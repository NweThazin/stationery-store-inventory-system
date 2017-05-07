using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class ReorderReportBO
    {
        private int serialno;
        private string itemCode;
        private string description;
        private int? quantityOnhand;
        private int? reorderLevel;
        private int? reorderQty;
        private string purchaseID;
        private DateTime? expectedDelivery;

        public int Serialno
        {
            get
            {
                return serialno;
            }

            set
            {
                serialno = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }

            set
            {
                itemCode = value;
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

        public int? QuantityOnhand
        {
            get
            {
                return quantityOnhand;
            }

            set
            {
                quantityOnhand = value;
            }
        }

        public int? ReorderLevel
        {
            get
            {
                return reorderLevel;
            }

            set
            {
                reorderLevel = value;
            }
        }

        public int? ReorderQty
        {
            get
            {
                return reorderQty;
            }

            set
            {
                reorderQty = value;
            }
        }

        public string PurchaseID
        {
            get
            {
                return purchaseID;
            }

            set
            {
                purchaseID = value;
            }
        }

        public DateTime? ExpectedDelivery
        {
            get
            {
                return expectedDelivery;
            }

            set
            {
                expectedDelivery = value;
            }
        }
    }
}
