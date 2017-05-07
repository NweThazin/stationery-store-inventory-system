using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace BusinessLayer
{
    public class PurchaseItemBL
    {
        PurchaseItemDA da = new PurchaseItemDA();

        //Get the item lists
        public List<ItemBO> getItemList()
        {
            List<ItemBO> ibo = new List<ItemBO>();
            var itemLst = da.getItemList();//check
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

        //Get Item ID by Item Number
        public int getItemIDByNumber(string number)
        {
            return da.getItemIDbyNumber(number);
        }

        //Get suppliers list
        public List<SupplierBO> getSuppliersList(string itemnumber)
        {
            int itemID = da.getItemIDbyNumber(itemnumber);
            List<SupplierBO> sbo = new List<SupplierBO>();
            var supplierList = da.getSupplierIDList(itemID);//check
            foreach (int s in supplierList)
            {
                SupplierBO sb = new SupplierBO();
                string supplier = da.getSupplier(s);
                sb.SupplierName = supplier;
                sb.SupplierID = s;
                sbo.Add(sb);
            }
            return sbo;
        }

        //Get description by Item Number
        public string getDescription(string itemnumber)
        {
            string description = da.getDescription(itemnumber);
            return description;
        }
        //Get Reorder Qty
        public string getReorderQty(string itemnumber)
        {
            return da.getReorderQty(itemnumber);
        }
        //Get Price by Item ID
        public double getPrice(int itemId,int supplierID)
        {
            double price = da.getPrice(itemId,supplierID);
            return price;
        }

        //Insert the Purchase Order lists to table
        public void addPurchaseOrderList(List<PurchaseOrderBO> purlst,int empID)
        {
            //Create purchase object
            Purchase purObj = new Purchase();
            purObj.PurchaseDate = DateTime.Now.Date;
            purObj.EmployeeID = empID;//Need to change user id

            List<Purchase_Item> pst = new List<Purchase_Item>();
            foreach (PurchaseOrderBO p in purlst)
            {
                Purchase_Item pi = new Purchase_Item();
               
                pi.QtyOrdered = p.Quantity;
                pi.TotalPrice = p.Amount;
                pi.ItemID = p.ItemID;
                pi.SupplierID = p.SupplierID;
                pi.DateExpectedDelivery = p.ExpectedDelivery;
                pi.QuantityReceived = 0;
                pst.Add(pi);
            }
            da.purchaseOrderList(purObj, pst);
        }

        public string getSupplier(int supplierID)
        {
            return da.getSupplier(supplierID);
        }
    }
}
