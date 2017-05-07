using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public  class ReqDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //For Stationery Catalogue Page
        //Get the item list
        public List<Item> getItemList()
        {
            var qry = context.Items.ToList<Item>();
            return qry;
        }

        //Get the item id by item Number
        public string getItemNumberbyId(int itemId)
        {
           string itemNumber = context.Items.Where(x => x.ItemID == itemId).Select(x => x.ItemNumber).FirstOrDefault();
            return itemNumber;
        }

        //Get Description by item ID
        public string getDescription(int itemId)
        {
            string description = context.Items.Where(x => x.ItemID==itemId).Select(x => x.Description).FirstOrDefault();
            return description;
        }
        //Add to database
        public void addRequisitionList(Requisition requisition, List<Requisition_Item> rItem)
        {
            //Add many to many table data using entity framework
            foreach (Requisition_Item r in rItem)
            {
                requisition.Requisition_Item.Add(r);
            }
            context.Requisitions.Add(requisition);
            context.SaveChanges();
        }

    }
}
