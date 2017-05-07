using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.StoreClerk;

namespace DataAccess
{
    public class ReportDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        
        //Show the ReorderList Report
        public List<ReorderReportBO> getReorderList(int status)
        {
            int count = 0;
            List<ReorderReportBO> lst = new List<ReorderReportBO>();
            if (status == 0)
            {
                //Reorder
                var qry = (from i in context.Items
                           where !context.Purchase_Item.Any(f => f.ItemID == i.ItemID) && i.InStockQty<i.ReorderLevel
                           select i).ToList();
                    foreach (var q in qry)
                    {
                        count++;
                        ReorderReportBO b = new ReorderReportBO();
                        b.Serialno = count;
                        b.ItemCode = q.ItemNumber;
                        b.Description = q.Description;
                        b.QuantityOnhand = q.InStockQty;
                        b.ReorderLevel = q.ReorderLevel;
                        b.ReorderQty = q.ReorderQty;
                        b.PurchaseID = "-";
                        lst.Add(b);
                    }
            }
            else if (status == 1)
            {
                //Purchase
                var qry = (from i in context.Items
                           join pi in context.Purchase_Item on i.ItemID equals pi.ItemID
                           where i.InStockQty < i.ReorderLevel
                           select new { i.ItemNumber, i.Description, i.InStockQty, i.ReorderLevel, i.ReorderQty, pi.PurchaseID, pi.DateExpectedDelivery }).ToList();
                foreach (var q in qry)
                {
                    count++;
                    ReorderReportBO b = new ReorderReportBO();
                    b.Serialno = count;
                    b.ItemCode = q.ItemNumber;
                    b.Description = q.Description;
                    b.QuantityOnhand = q.InStockQty;
                    b.ReorderLevel = q.ReorderLevel;
                    b.ReorderQty = q.ReorderQty;
                    b.PurchaseID = "R" + q.PurchaseID;
                    b.ExpectedDelivery = q.DateExpectedDelivery;
                    lst.Add(b);
                }
            }
            return lst;
        }
    }
}
