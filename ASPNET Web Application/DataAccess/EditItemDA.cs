using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EditItemDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //Get one item by Item Number
        public Item getItemBYItemNumber(string itemNumber)
        {
            var item = context.Items.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
            if (item != null)
            {
                return (Item)item;
            }
            else
            {
                return null;
            }
        }

        //Get CategoryName By CategoryID
        public Category getCategoryBYID(int? categoryID)
        {
            var cat = context.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault();
            return (Category)cat;
        }

        //Get all categories lists
        public List<Category> getAllCategories()
        {
            var catQry = context.Categories.ToList<Category>();
            return catQry;
        }
    }
}
