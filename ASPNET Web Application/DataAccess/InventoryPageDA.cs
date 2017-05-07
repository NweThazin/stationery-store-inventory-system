using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class InventoryPageDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Stationery Catalogue Page
        //1..Get all inventory lists and show in inventory page and Stationery Catalogue Page
        public List<ItemBO> getInventoryList()
        {
            List<ItemBO> blst = new List<ItemBO>();
            //Join category table and item table and retrieve
            var qry = context.Categories.Join(context.Items, r => r.CategoryID, p => p.CategoryID, (r, p) => new
            {
                p.ItemID,
                p.ItemNumber,
                p.CategoryID,
                p.Description,
                p.InStockQty,
                p.ReorderLevel,
                p.ReorderQty,
                p.UnitOfMeasure,
                p.Bin,
                r.CategoryName
            });
            foreach (var b in qry)
            {
                ItemBO obj = new ItemBO();
                obj.ItemID = b.ItemID;
                obj.ItemNumber = b.ItemNumber;
                obj.CategoryID = b.CategoryID;
                obj.Description = b.Description;
                obj.InStockQty = b.InStockQty;
                obj.ReorderLevel = b.ReorderLevel;
                obj.ReorderQty = b.ReorderQty;
                obj.UnitOfMeasure = b.UnitOfMeasure;
                obj.Bin = b.Bin;
                obj.CategoryName = b.CategoryName;
                blst.Add(obj);
            }
            return blst;
        }

        //2..Search Inventory Lists by Item Number or Description
        public List<ItemBO> searchInventory(string itemNumber, string description)
        {
            var qry = context.Categories.Join(context.Items, r => r.CategoryID, p => p.CategoryID, (r, p) => new
            {
                p.ItemID, p.ItemNumber, p.CategoryID, p.Description, p.InStockQty, p.ReorderLevel, p.ReorderQty,
                p.UnitOfMeasure, p.Bin, r.CategoryName

            }).Where(x => (x.ItemNumber.Contains(itemNumber)) || (x.Description.Contains(description))).ToList();

            List<ItemBO> blst = new List<ItemBO>();
            foreach (var b in qry)
            {
                ItemBO obj = new ItemBO();
                obj.ItemID = b.ItemID;
                obj.ItemNumber = b.ItemNumber;
                obj.CategoryID = b.CategoryID;
                obj.Description = b.Description;
                obj.InStockQty = b.InStockQty;
                obj.ReorderLevel = b.ReorderLevel;
                obj.ReorderQty = b.ReorderQty;
                obj.UnitOfMeasure = b.UnitOfMeasure;
                obj.Bin = b.Bin;
                obj.CategoryName = b.CategoryName;
                blst.Add(obj);
            }
            return blst;
        }
    }
}