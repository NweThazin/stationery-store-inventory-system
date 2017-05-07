using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class ItemDA
    {
        ADTeam1Entities context = new ADTeam1Entities();
        ItemBO ib = new ItemBO();

        //Add item to Items table
        public void addItem(Item i)
        {
            context.Items.Add(i);
            context.SaveChanges();
        }
        //get item
        public ItemBO getItem(int itemID)
        {
            var qry = context.Items.Where(x => x.ItemID == itemID).FirstOrDefault();
            ib.ItemNumber = qry.ItemNumber;
            ib.CategoryID = qry.CategoryID;
            ib.ReorderLevel = qry.ReorderLevel;
            ib.ReorderQty = qry.ReorderQty;
            ib.UnitOfMeasure = qry.UnitOfMeasure;
            ib.Description = qry.Description;
            ib.Bin = qry.Bin;
            return ib;
        }

        public void editItem(Item i)
        {
            var qry = context.Items.Where(x => x.ItemID == i.ItemID).First();
            qry.ItemNumber = i.ItemNumber;
            qry.CategoryID = i.CategoryID;
            qry.ReorderLevel = i.ReorderLevel;
            qry.ReorderQty = i.ReorderQty;
            qry.UnitOfMeasure = i.UnitOfMeasure;
            qry.Description = i.Description;
            qry.Bin = i.Bin;
            context.SaveChanges();
        }

        //return a list of existing itemNumbers 
        public List<String> listItemNumber()
        {
            List<String> lst = new List<String>();
            var qry = context.Items.Select(x => new { x.ItemNumber }).ToList();
            foreach (var q in qry)
            {
                string itemNumber = q.ItemNumber;
                lst.Add(itemNumber);
            }
            return lst;
        }
        //get all category lists
        public List<CategoryBO> getCategoryList()
        {
            var qry= (from x in context.Categories select new { x.CategoryName, x.CategoryID }).ToList();
            List<CategoryBO> lst = new List<CategoryBO>();
            foreach (var q in qry)
            {
                CategoryBO c = new CategoryBO();
                c.CategoryID = q.CategoryID;
                c.CategoryName = q.CategoryName;
                lst.Add(c);
            }
            return lst;
        }

    }
}
