using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.StoreClerk;
using DataAccess;
using BusinessObject;

namespace BusinessLayer
{
    public class ReorderReportBL
    {
        ReportDA da = new ReportDA();

        //For Reorder Report Page
        public List<ReorderReportBO> getReorderList(int i)
        {
            return da.getReorderList(i);
        }

        //For Inventory Report Page
        InventoryStatusReportDA isDA = new InventoryStatusReportDA();
        public List<ItemBO> getInventoryList()
        {
            return isDA.getAllInventoryList();
        }

    }
}
