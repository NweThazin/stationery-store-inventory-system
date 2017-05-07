using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class AdjustmentVoucherDetailBO
    {
        private string itemNumber;
        private string itemDescription;
        private string quantityAdjusted;
        private string reason;
        private int itemID;

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

        public string QuantityAdjusted
        {
            get
            {
                return quantityAdjusted;
            }

            set
            {
                quantityAdjusted = value;
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

        public int ItemCode
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
