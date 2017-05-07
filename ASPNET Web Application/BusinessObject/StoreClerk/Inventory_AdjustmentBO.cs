using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class Inventory_AdjustmentBO
    {
        private string itemNumber;
        private int itemID;
        private int adjustmentID;
        private string adjustmentQty;
        private string reason;
        private string condition;

        private double totalPrice;

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

        public int ItemID
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

        public int AdjustmentID
        {
            get
            {
                return adjustmentID;
            }

            set
            {
                adjustmentID = value;
            }
        }

        public string AdjustmentQty
        {
            get
            {
                return adjustmentQty;
            }

            set
            {
                adjustmentQty = value;
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

        public string Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = value;
            }
        }
    }
}
