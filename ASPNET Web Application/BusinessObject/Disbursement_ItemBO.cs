using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Disbursement_ItemBO
    {
        private int disbursementItemID;
        private int? disbursementID;
        private int? itemID;
        private int? itemQty;
        private string status;
        private DateTime? date;

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

        public int? DisbursementID
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

        public int? ItemQty
        {
            get
            {
                return itemQty;
            }

            set
            {
                itemQty = value;
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

        public DateTime? Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
    }
}
