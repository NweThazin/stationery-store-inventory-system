using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class InventoryAdjustmentDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        //Get the item list
        public List<Item> getItemList()
        {
            var qry = context.Items.ToList<Item>();
            return qry;
        }

        //Get the item id by item Number
        public int getItemIDbyNumber(string itemNumber)
        {
            int itemID = context.Items.Where(x => x.ItemNumber == itemNumber).Select(x => x.ItemID).FirstOrDefault();
            return itemID;
        }

        //Save all inventory adjustments lists to Inventory Adjustment Table
        public void saveInventoryItemList(Adjustment adjustment, List<Adjustment_Item> adjItem)
        {
            foreach (Adjustment_Item i in adjItem)
            {
                adjustment.Adjustment_Item.Add(i);
            }
            context.Adjustments.Add(adjustment);
            context.SaveChanges();
        }

        //To Show the price
        public double getPriceByItemID(int itemID)
        {
            var qry = context.Supplier_Item.Where(x => x.ItemID == itemID).Select(x => x.UnitPrice).FirstOrDefault();
            return double.Parse(qry.ToString());
        }

    }
}
