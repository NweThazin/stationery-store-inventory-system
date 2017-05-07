using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class PurchaseOrderBO
    {
        private string itemNo;
        private int itemID;
        private int purchaseID;
        private string itemDescription;
        private int quantity;
        private double price;
        private decimal amount;
        private DateTime date;
        //Added
        private int supplierID;
        private DateTime expectedDelivery;
        private string supplierName;
        public string ItemNo
        {
            get
            {
                return itemNo;
            }

            set
            {
                itemNo = value;
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

        public int PurchaseID
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

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public double Price
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

        public decimal Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }

        public DateTime Date
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

        public int SupplierID
        {
            get
            {
                return supplierID;
            }

            set
            {
                supplierID = value;
            }
        }

        public DateTime ExpectedDelivery
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

        public string SupplierName
        {
            get
            {
                return supplierName;
            }

            set
            {
                supplierName = value;
            }
        }
    }
}
