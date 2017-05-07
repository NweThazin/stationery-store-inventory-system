using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;

namespace BusinessLayer
{
    public class ItemBL
    {
        ItemDA da = new ItemDA();

        //Add
        //ItemBO to Item so that da can use
        public void addItem(ItemBO ib)
        {
            Item i = new Item();
            i.ItemNumber = ib.ItemNumber;
            i.CategoryID = ib.CategoryID;
            i.ReorderLevel = ib.ReorderLevel;
            i.ReorderQty = ib.ReorderQty;
            i.UnitOfMeasure = ib.UnitOfMeasure;
            i.Description = ib.Description;
            i.Bin = ib.Bin;
            da.addItem(i);
        }

        //get
        public ItemBO getItem(int itemID)
        {
            return da.getItem(itemID);
        }

        //edit
        public void editItem(ItemBO ib)
        {
            Item i = new Item();
            i.ItemID = ib.ItemID;
            i.ItemNumber = ib.ItemNumber;
            i.CategoryID = ib.CategoryID;
            i.Description = ib.Description;
            i.ReorderLevel = ib.ReorderLevel;
            i.ReorderQty = ib.ReorderQty;
            i.UnitOfMeasure = ib.UnitOfMeasure;
            i.Bin = ib.Bin;
            da.editItem(i);
        }

        //return a list of existing itemNumbers 
        public List<string> ListItemNumber()
        {
            return da.listItemNumber();
        }

        //Get category lists (check)
        public List<CategoryBO> getCategoryList()
        {
            return da.getCategoryList();
        }
    }
}
