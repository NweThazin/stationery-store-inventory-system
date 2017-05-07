using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.StoreClerk;
using BusinessObject;

namespace DataAccess
{
    public class StockCardDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //Get all the supplier information
        public List<ItemSupplierJoinBO> getStockCardInformation(string itemNumber)
        {
            var qry = (from i in context.Items
                       join si in context.Supplier_Item on i.ItemID equals si.ItemID
                       join s in context.Suppliers on si.SupplierID equals s.SupplierID
                       where i.ItemNumber.Equals(itemNumber)                                 //Need to change or not
                       select new { i.ItemNumber, i.Description, i.Bin, i.UnitOfMeasure, s.SupplierName }).ToList();

            List<ItemSupplierJoinBO> lst = new List<ItemSupplierJoinBO>();
            foreach (var q in qry)
            {
                ItemSupplierJoinBO obj = new ItemSupplierJoinBO();
                obj.ItemCode = q.ItemNumber;
                obj.Description = q.Description;
                obj.Bin = q.Bin;
                obj.Uom = q.UnitOfMeasure;
                obj.SupplierName = q.SupplierName;
                lst.Add(obj);
            }
            return lst;
        }
        //Get Item Information
        public Item getItemByItemNumber(string itemNumber)
        {
            var qry = context.Items.Where(x => x.ItemNumber.Equals(itemNumber)).FirstOrDefault();
            return qry;
        }

        //Get the StockCard of each item
        public List<StockCardBO> getStockCardofEachItem(string itemNumber)
        {
            int flag = 0;
            List<StockCardBO> lst = new List<StockCardBO>();
            //For Department Side'
            var qryDepartment = (from i in context.Items
                                 join di in context.Disbursement_Item on i.ItemID equals di.ItemID
                                 join d in context.Disbursements on di.DisbursementID equals d.DisbursementID
                                 join dep in context.Departments on d.DepartmentID equals dep.DepartmentID
                                 where i.ItemNumber.Equals(itemNumber)
                                 select new { di.Date, dep.DepartmentName, di.Qty, i.InStockQty }).ToList();
            foreach (var s in qryDepartment)//have already changed to item date
            {
                flag = flag + 1;
                StockCardBO stock = new StockCardBO();
                stock.Date = s.Date;
                stock.Name = s.DepartmentName;
                stock.Quantity = "-" + s.Qty.ToString();
                stock.Balance = s.InStockQty;
                lst.Add(stock);
            }

            //For Supplier Side
            var qrySupplier = (from i in context.Items
                               join pItem in context.Purchase_Item on i.ItemID equals pItem.ItemID
                               join p in context.Purchases on pItem.PurchaseID equals p.PurchaseID
                               join s in context.Suppliers on pItem.SupplierID equals s.SupplierID
                               where i.ItemNumber.Equals(itemNumber)    //need to confirm
                               select new { pItem.DateSupplied, s.SupplierName, pItem.QuantityReceived,i.InStockQty}).ToList();
            foreach (var s in qrySupplier)
            {
                if (s.QuantityReceived == 0)
                {
                    //Quantity is zero didn't addd to the list
                }
                else
                {
                    StockCardBO stock = new StockCardBO();
                    stock.Date = s.DateSupplied;
                    stock.Name = s.SupplierName;
                    stock.Quantity = "+" + s.QuantityReceived.ToString();
                    stock.Balance = s.InStockQty;
                    lst.Add(stock);
                }
            }

            //For Adjustment
            var qryAdjustment = (from i in context.Items
                                 join adjI in context.Adjustment_Item on i.ItemID equals adjI.ItemID
                                 join adj in context.Adjustments on adjI.AdjustmentID equals adj.AdjustmentID
                                 where i.ItemNumber.Equals(itemNumber) && adj.Status.Equals("Approved")
                                 select new { adj.Date, adjI.Adjustment_ItemID, adjI.AdjustedQty, i.InStockQty }).ToList();
            foreach (var s in qryAdjustment)
            {
                StockCardBO stock = new StockCardBO();
                stock.Date = s.Date;
                stock.Name = "Stock Adjustment No: " + s.Adjustment_ItemID;
                if (s.AdjustedQty < 0)
                {
                    stock.Quantity = s.AdjustedQty.ToString();
                }
                else
                {
                    stock.Quantity = "+" + s.AdjustedQty.ToString();
                }
                stock.Balance = s.InStockQty;
                lst.Add(stock);
            }
            return lst;
        } 

    }
}
