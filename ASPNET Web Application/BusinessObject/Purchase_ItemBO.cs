using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Purchase_ItemBO
    {
        private int purchase_ItemID;
        private int? qtyOrdered;
        private DateTime? dateSupplied;
        private decimal? totalPrice;
        private int? itemID;
        private int? purchaseID;
        private int? supplierID;
        private DateTime? dateExpectedDelivery;
        private int? qtyReceived;

        public int Purchase_ItemID
        {
            get
            {
                return purchase_ItemID;
            }

            set
            {
                purchase_ItemID = value;
            }
        }

        public int? QtyOrdered
        {
            get
            {
                return qtyOrdered;
            }

            set
            {
                qtyOrdered = value;
            }
        }

        public DateTime? DateSupplied
        {
            get
            {
                return dateSupplied;
            }

            set
            {
                dateSupplied = value;
            }
        }

        public decimal? TotalPrice
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

        public int? SupplierID
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

        public DateTime? DateExpectedDelivery
        {
            get
            {
                return dateExpectedDelivery;
            }

            set
            {
                dateExpectedDelivery = value;
            }
        }

        public int? QtyReceived
        {
            get
            {
                return qtyReceived;
            }

            set
            {
                qtyReceived = value;
            }
        }
    }
}
