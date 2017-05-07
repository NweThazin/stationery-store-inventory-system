using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class PurchaseBO
    {
        private int? purchaseID;
        private DateTime? purchaseDate;
        private int? employeeID;

        public int? PurchaseID
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

        public DateTime? PurchaseDate
        {
            get
            {
                return purchaseDate;
            }

            set
            {
                purchaseDate = value;
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
    }
}
