using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ItemBO
    {
        private int itemID;
        private string itemNumber;
        private int? categoryID;
        private string description;
        private int? inStockQty;
        private int? reorderLevel;
        private int? reorderQty;
        private string unitOfMeasure;
        private string bin;

        private string categoryName;

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

        public int? CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
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

        public int? InStockQty
        {
            get
            {
                return inStockQty;
            }

            set
            {
                inStockQty = value;
            }
        }

        public int? ReorderLevel
        {
            get
            {
                return reorderLevel;
            }

            set
            {
                reorderLevel = value;
            }
        }

        public int? ReorderQty
        {
            get
            {
                return reorderQty;
            }

            set
            {
                reorderQty = value;
            }
        }

        public string UnitOfMeasure
        {
            get
            {
                return unitOfMeasure;
            }

            set
            {
                unitOfMeasure = value;
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

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }
    }
}
