using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;
using BusinessObject.StoreClerk;

namespace BusinessLayer
{
    public class InventoryAdjustmentBL
    {
        InventoryAdjustmentDA da = new InventoryAdjustmentDA();
        //Get the item lists
        public List<ItemBO> getItemList()
        {
            List<ItemBO> ibo = new List<ItemBO>();
            var itemLst = da.getItemList();
            foreach (Item i in itemLst)
            {
                ItemBO b = new ItemBO();
                b.ItemID = i.ItemID;
                b.ItemNumber = i.ItemNumber;
                b.CategoryID = i.CategoryID;
                b.Description = i.Description;
                b.InStockQty = i.InStockQty;
                b.ReorderLevel = i.ReorderLevel;
                b.ReorderQty = i.ReorderQty;
                b.UnitOfMeasure = i.UnitOfMeasure;
                b.Bin = i.Bin;
                ibo.Add(b);
            }
            return ibo;
        }

        //Get Item ID BY Item Number
        public int getItemIDByNumber(string number)
        {
            return da.getItemIDbyNumber(number);
        }

        //Insert the adjustment Item lists to table 
        public void insertAdjustmentItemList(List<Inventory_AdjustmentBO> adjlst,int empID,double totalPrice)
        {
            //Create adjustment object
            Adjustment adjObj = new Adjustment();
            adjObj.Date = DateTime.Now.Date;
            adjObj.EmployeeID = empID;
            adjObj.Price = Convert.ToDecimal(totalPrice);
            adjObj.Status = "Pending";

            List<Adjustment_Item> lst = new List<Adjustment_Item>();
            foreach (Inventory_AdjustmentBO i in adjlst)
            {
                Adjustment_Item b = new Adjustment_Item();
                b.ItemID = i.ItemID;
                b.AdjustedQty = int.Parse(i.AdjustmentQty);
                b.Reason = i.Reason;
                b.Condition = i.Condition;
                lst.Add(b);
            }
            da.saveInventoryItemList(adjObj, lst);
        }

        public double getPriceByItemID(int itemid)
        {
            return da.getPriceByItemID(itemid);
        }

        public double calculatePrice(string itemNumber,int qty)
        {
            //Get each item price by item number and item id
            double eachItemPrice = getPriceByItemID(getItemIDByNumber(itemNumber));
            double price = eachItemPrice * qty;
            return price;
        }

    }
}
