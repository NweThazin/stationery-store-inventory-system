using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Supplier_ItemBO
    {
        private int supplier_ItemID;
        private int? supplierID;
        private int itemID;
        private int? unitPrice;

        public int Supplier_ItemID
        {
            get
            {
                return supplier_ItemID;
            }

            set
            {
                supplier_ItemID = value;
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

        public int? UnitPrice
        {
            get
            {
                return unitPrice;
            }

            set
            {
                unitPrice = value;
            }
        }
    }
}
