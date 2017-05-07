using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class AdjustmentBO
    {
        private int adjustmentID;
        private DateTime? date;
        private int? empID;
        private string status;
        private decimal? price;
        private string adjID;

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

        public int? EmpID
        {
            get
            {
                return empID;
            }

            set
            {
                empID = value;
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

        public string AdjID
        {
            get
            {
                return adjID;
            }

            set
            {
                adjID = value;
            }
        }

        public decimal? Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
    }
}
