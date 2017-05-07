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
    public class StoreCardBL
    {
        StockCardDA da = new StockCardDA();

        //Get the item and supplier information
        public List<ItemSupplierJoinBO> getItemInformation(string itemNumber)
        {
            return da.getStockCardInformation(itemNumber);
        }

        //Return Item By Item Number
        public ItemBO getItemByItemNumber(string itemNumber)
        {
            ItemBO b = new ItemBO();
            Item i = da.getItemByItemNumber(itemNumber);
            b.ItemNumber = i.ItemNumber;
            b.Description = i.Description;
            b.Bin = i.Bin;
            b.UnitOfMeasure = i.UnitOfMeasure;
            return b;
        }

        //Get Stock Card Information BY Each Item
        public ItemBO getItemByItemNumberBL(string itemNumber)
        {
            Item i = da.getItemByItemNumber(itemNumber);
            ItemBO b = new ItemBO();
            b.ItemNumber = i.ItemNumber;
            b.Description = i.Description;
            b.UnitOfMeasure = i.UnitOfMeasure;
            b.InStockQty = i.InStockQty;
            b.Bin = i.Bin;
            return b;
        }
        public List<StockCardBO> getStockCardInformationByEachItem(string itemNumber)
        {
            //Retreive Data and Sort By Date
            List<StockCardBO> SortedList = da.getStockCardofEachItem(itemNumber).OrderBy(o => o.Date).ToList();

            //Show Balance
            int flag = 0;
            int? originalBalance = 0;
            List<StockCardBO> showBalanceList = new List<StockCardBO>();
            foreach (StockCardBO s in SortedList)
            {
                flag ++; 
                StockCardBO sObj = new StockCardBO();
                sObj.Date = s.Date;
                sObj.Name = s.Name;
                sObj.Quantity = s.Quantity;

                if (flag == 1)
                {
                    originalBalance = s.Balance;
                    originalBalance = originalBalance + int.Parse(s.Quantity);
                    sObj.Balance = originalBalance;
                }
                else
                {
                    originalBalance = originalBalance + int.Parse(s.Quantity);
                    sObj.Balance = originalBalance;
                }
                showBalanceList.Add(sObj);
            }
            return showBalanceList;
        }
    }
}
