using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.StoreClerk
{
    public class ItemSupplierJoinBO
    {
        private string itemCode;
        private string description;
        private string bin;
        private string uom;
        private string supplierName;
        private int? supplierID;

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

        public string Bin
        {
            get
            {
                return bin;
            }

            set
            {
                bin = value;
            }
        }

        public string Uom
        {
            get
            {
                return uom;
            }

            set
            {
                uom = value;
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
    }
}
