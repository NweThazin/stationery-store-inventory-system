using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.StoreClerk;
using BusinessObject;
using DataAccess;

namespace BusinessLayer
{
    public class UpdateStockCardBL
    {
        UpdateStockCardDA da = new UpdateStockCardDA();
        public ItemBO getEachItem(string itemNumber)
        {
            return da.getItemByItemNumber(itemNumber);
        }

        public List<ItemSupplierJoinBO> getSupplierListByItemNumber(string itemNumber)
        {
            return da.getSupplierListByItemNumber(itemNumber);
        }

        public bool checkSupplierinPurchaseItem(int supplierID, int itemID)
        {
            return da.checkSupplier(supplierID, itemID);
        }

        public void updateStockTable(int itemID, string itemNumber, int stock)
        {
            da.updateInstockTable(itemID, itemNumber, stock);
        }
        public int getItemQty(int itemID, string itemNumber)
        {
            return da.getInStockQty(itemID, itemNumber);
        }
        public int getPurchaseItem(string itemNumber, int itemID, int supplierID)
        {
            return da.getPurchaseItemID(itemNumber, itemID, supplierID);
        }
        public void updatePurchaseItem(int purchaseItemID, int qtyReceived, DateTime date, string itemNumber)
        {
            da.updatePurchaseItem(purchaseItemID, qtyReceived, date, itemNumber);
        }
    }
}
