using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PurchaseItemDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        
        //Get the item lists
        public List<Item> getItemList() //check
        {
            var qry = context.Items.ToList<Item>();
            return qry;
        }

        //Get the item id by item Number
        public int getItemIDbyNumber(string itemNumber) //check
        {
            int itemID = context.Items.Where(x => x.ItemNumber == itemNumber).Select(x => x.ItemID).FirstOrDefault();
            return itemID;
        }

        //Get the supplier list
        public List<int?> getSupplierIDList(int itemID) //check
        {
            var qry = context.Supplier_Item.Where(x => x.ItemID == itemID).Select(x => x.SupplierID).ToList();
            return qry;
        }

        //Get supplier by supplier id 
        public string getSupplier(int supplierID)
        {
            string supplier = context.Suppliers.Where(x => x.SupplierID == supplierID).Select(x => x.SupplierName).FirstOrDefault();
            return supplier;
        }

        //Get description by itemNumber
        public string getDescription(string itemNumber)
        {
            string description = context.Items.Where(x => x.ItemNumber == itemNumber).Select(x => x.Description).FirstOrDefault();
            return description;
        }
        //Get reorder qty
        public string getReorderQty(string itemNumber)
        {
            int? reorder = context.Items.Where(x => x.ItemNumber == itemNumber).Select(x => x.ReorderQty).FirstOrDefault();
            return Convert.ToString(reorder);
        }

        //Get price by item number
        public double getPrice(int itemID,int supplierID)
        {
            double price = (double)context.Supplier_Item.Where(x => x.ItemID == itemID && x.SupplierID==supplierID).Select(x => x.UnitPrice).FirstOrDefault();
            return price;
        }

        //Add purchase order into the table
        public void purchaseOrderList(Purchase purchase, List<Purchase_Item> purItem)
        {
            foreach (Purchase_Item p in purItem)
            {
                purchase.Purchase_Item.Add(p);
            }
            context.Purchases.Add(purchase);
            context.SaveChanges();
        }

        ////Get the supplier item
        //public string getSupplier(int supplierID)
        //{
        //    string supplier = context.Suppliers.Where(x => x.SupplierID == supplierID).Select(x => x.SupplierName).FirstOrDefault();
        //    return supplier;
        //}
    }
}
