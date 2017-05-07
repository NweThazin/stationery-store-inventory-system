using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Adjustment_ItemBO
    {
        private int adjustment_ItemID;
        private int? adjustmentID;
        private int? itemID;
        private int? adjustedQty;
        private string reason;
        private string condition;

        public int Adjustment_ItemID
        {
            get
            {
                return adjustment_ItemID;
            }

            set
            {
                adjustment_ItemID = value;
            }
        }

        public int? AdjustmentID
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

        public int? AdjustedQty
        {
            get
            {
                return adjustedQty;
            }

            set
            {
                adjustedQty = value;
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
    }
}
