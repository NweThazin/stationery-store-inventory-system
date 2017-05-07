using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;

namespace BusinessLayer
{
    public class InventoryPageBL
    {
        InventoryPageDA da = new InventoryPageDA();

        // For Stationery Catalogue Page
        //1..Get all inventory or item List
        public List<ItemBO> getInventoryList()
        {
            return da.getInventoryList();
        }

        //2..Search Inventory Lists by item number or description
        public List<ItemBO> searchInventoryByNumberOrDescription(string itemNumber,string description)
        {
            return da.searchInventory(itemNumber, description);
        }
    }
}
