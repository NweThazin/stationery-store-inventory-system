using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;

namespace BusinessLayer
{
    public class EditItemBL
    {
        EditItemDA eiDA = new EditItemDA();

        //Get all item information and category by item number
        public ItemBO getItemByItemNumber(string itemNumber)
        {
            //Retrieve Item Object and Category Object
            Item i = eiDA.getItemBYItemNumber(itemNumber);
            if (i == null)
            {
                return null;
            }
            else
            {
                Category c = eiDA.getCategoryBYID(i.CategoryID);
                //Then assign to ItemBO object again
                ItemBO itemObj = new ItemBO();
                itemObj.ItemID = i.ItemID;
                itemObj.ItemNumber = i.ItemNumber;
                itemObj.CategoryID = i.CategoryID;
                itemObj.Description = i.Description;
                itemObj.InStockQty = i.InStockQty;
                itemObj.ReorderLevel = i.ReorderLevel;
                itemObj.ReorderQty = i.ReorderQty;
                itemObj.UnitOfMeasure = i.UnitOfMeasure;
                itemObj.Bin = i.Bin;
                itemObj.CategoryName = c.CategoryName;
                return itemObj;
            }
        }

        //get all categories list
        public List<CategoryBO> getCategoryList()
        {
            var qry = eiDA.getAllCategories(); //get all categories lists from the da class
            List<CategoryBO> catLst = new List<CategoryBO>();
            foreach (Category i in qry)
            {
                CategoryBO cat = new CategoryBO();
                cat.CategoryID = i.CategoryID;
                cat.CategoryName = i.CategoryName;
                catLst.Add(cat);
            }
            return catLst;
        }
    }
}
