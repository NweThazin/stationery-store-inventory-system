using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObject;
using BusinessObject.StoreClerk;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UpdateStockCardDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        public ItemBO getItemByItemNumber(string itemNumber)
        {
            //Get one item by item number
            var qry = context.Items.Where(x => x.ItemNumber.Equals(itemNumber)).FirstOrDefault();
            ItemBO ibo = new ItemBO();
            ibo.ItemID = qry.ItemID;
            ibo.ItemNumber = qry.ItemNumber;
            ibo.CategoryID = qry.CategoryID;
            ibo.Description = qry.Description;
            ibo.InStockQty = qry.InStockQty;
            ibo.ReorderLevel = qry.ReorderLevel;
            ibo.ReorderQty = qry.ReorderQty;
            ibo.UnitOfMeasure = qry.UnitOfMeasure;
            ibo.Bin = qry.Bin;
            return ibo;
        }

        //Get the suppliers lists
        public List<ItemSupplierJoinBO> getSupplierListByItemNumber(string itemNumber)
        {
            var qry = (from i in context.Items
                       join si in context.Supplier_Item on i.ItemID equals si.ItemID
                       join s in context.Suppliers on si.SupplierID equals s.SupplierID
                       where i.ItemNumber == itemNumber                              
                       select new { i.ItemNumber, i.Description, i.Bin, i.UnitOfMeasure, s.SupplierName , s.SupplierID }).Distinct().ToList();

            List<ItemSupplierJoinBO> lst = new List<ItemSupplierJoinBO>();
            foreach (var q in qry)
            {
                ItemSupplierJoinBO obj = new ItemSupplierJoinBO();
                obj.ItemCode = q.ItemNumber;
                obj.Description = q.Description;
                obj.Bin = q.Bin;
                obj.Uom = q.UnitOfMeasure;
                obj.SupplierName = q.SupplierName;
                obj.SupplierID = q.SupplierID;
                lst.Add(obj);
            }
            return lst;

        }
        //Check supplier in Purchase Item Table
        public Boolean checkSupplier(int supplierID,int itemID)
        {
            var qry = context.Purchase_Item.Where(x => x.SupplierID == supplierID && x.ItemID == itemID).FirstOrDefault();
            if (qry == null)
            {//if the supplier is not exit
                return false;
            }
            else
                return true;
        }
        //Get the in stock quantity
        public int getInStockQty(int itemID, string itemNumber)
        {
            var qry = context.Items.Where(x => x.ItemID == itemID && x.ItemNumber==itemNumber).Select(x => x.InStockQty).FirstOrDefault();
            return int.Parse(qry.ToString());
        }
        //Update the stock table
        public void updateInstockTable(int itemID, string itemNumber,int stock)
        {
            var qry = context.Items.Where(x => x.ItemID == itemID && x.ItemNumber == itemNumber).FirstOrDefault();
            Item i = (Item)qry;
            i.InStockQty = qry.InStockQty + stock;
            context.SaveChanges();
        }

        public int getPurchaseItemID(string itemNumber, int itemID, int supplierID)
        {
            Purchase_Item pi = new Purchase_Item();
            var qrySupplier = (from i in context.Items
                               join pItem in context.Purchase_Item on i.ItemID equals pItem.ItemID
                               join p in context.Purchases on pItem.PurchaseID equals p.PurchaseID
                               join s in context.Suppliers on pItem.SupplierID equals s.SupplierID
                               where i.ItemNumber.Equals(itemNumber) && i.ItemID==itemID && pItem.SupplierID==supplierID
                               select new { pItem.Purchase_ItemID }).FirstOrDefault();
            pi.Purchase_ItemID = qrySupplier.Purchase_ItemID;
            return pi.Purchase_ItemID;
        }

        public void updatePurchaseItem(int purchaseItemID, int qtyReceived, DateTime date, string itemNumber)
        {
            var qry = context.Purchase_Item.Where(x => x.Purchase_ItemID == purchaseItemID).FirstOrDefault();
            Purchase_Item pi = (Purchase_Item)qry;
            pi.QuantityReceived = qtyReceived;
            pi.DateSupplied = date;

            var qryUpdate = context.Items.Where(x => x.ItemNumber.Equals(itemNumber)).FirstOrDefault();
            qryUpdate.InStockQty += pi.QuantityReceived;
            context.SaveChanges();
        }
    }
}
