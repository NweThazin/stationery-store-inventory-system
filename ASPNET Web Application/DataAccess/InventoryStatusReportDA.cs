using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class InventoryStatusReportDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        //Inventory Status Report DA show all inventory lists
        public List<ItemBO> getAllInventoryList()
        {
            var qry = context.Items.ToList();
            List<ItemBO> lst = new List<ItemBO>();
            foreach (var q in qry)
            {
                ItemBO b = new ItemBO();
                b.ItemNumber = q.ItemNumber;
                b.Description = q.Description;
                b.Bin = q.Bin;
                b.UnitOfMeasure = q.UnitOfMeasure;
                b.InStockQty = q.InStockQty;
                b.ReorderLevel = q.ReorderLevel;
                lst.Add(b);
            }
            return lst;
        }
    }
}
