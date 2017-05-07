using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace DataAccess
{
    public class RetrievalDA
    {
        ADTeam1Entities context;

        public List<RetrievalBO> getRetrievalBOsByStatus(string statusPar)//get all Retrievals divided by each department
        {
            string status = null;
            if (statusPar == "Unfullfill Orders")
            {
                status = "Unfulfilled";
            }
            else if (statusPar == "New Orders")
            {
                status = "New";
            }
            context = new ADTeam1Entities();
            List<RetrievalBO> retrievalBOs = new List<RetrievalBO>();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.Where(x => x.Status == status).ToList();
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                RetrievalBO retrievalBO = new RetrievalBO();
                Item item = r_Item.Item;
                retrievalBO.Bin = item.Bin;
                retrievalBO.ItemNumber = item.ItemNumber;
                retrievalBO.Description = item.Description;
                retrievalBO.UOM = item.UnitOfMeasure;
                retrievalBO.InStock = (int)item.InStockQty;
                retrievalBO.DepartmentID = (int)r_Item.Requisition.DepartmentID;
                retrievalBO.Requested = (int)r_Item.RequestedQty;
                retrievalBO.Unfulfilled = (int)r_Item.UnfulfilledQty;
                retrievalBO.ToRetrieve = (int)r_Item.ToRetrieveQty;
                retrievalBOs.Add(retrievalBO);
            }
            return retrievalBOs;
        }

        public List<RetrievalBO> getCombinedRetrievalBOsByStatus(string statusPar)
        {
            string status = null;
            if (statusPar == "Unfullfill Orders")
            {
                status = "Unfulfilled";
            }
            else if (statusPar == "New Orders")
            {
                status = "New";
            }
            context = new ADTeam1Entities();
            List<RetrievalBO> retrievalBOs = new List<RetrievalBO>();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.Where(x => x.Status == status && x.Requisition.Status == "Approved").GroupBy(x => x.ItemID).Select(x => x.FirstOrDefault()).ToList();
            //find Requisition_Item list group by ItemID
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                RetrievalBO retrievalBO = new RetrievalBO();
                Item item = r_Item.Item;
                retrievalBO.Bin = item.Bin;
                retrievalBO.ItemNumber = item.ItemNumber;
                retrievalBO.Description = item.Description;
                retrievalBO.UOM = item.UnitOfMeasure;
                retrievalBO.InStock = (int)item.InStockQty;
                retrievalBO.Requested = (int)context.Requisition_Item.Where(x => x.Status == status && x.Requisition.Status == "Approved" && x.ItemID == r_Item.ItemID).Sum(x => x.RequestedQty);
                // retrieve the sum of unfulfilledQutity group by ItemID
                retrievalBO.Unfulfilled = (int)context.Requisition_Item.Where(x => x.Status == status && x.Requisition.Status == "Approved" && x.ItemID == r_Item.ItemID).Sum(x => x.UnfulfilledQty);
                retrievalBO.ToRetrieve = (int)context.Requisition_Item.Where(x => x.Status == status && x.Requisition.Status == "Approved" && x.ItemID == r_Item.ItemID).Sum(x => x.ToRetrieveQty);
                retrievalBOs.Add(retrievalBO);
            }
            return retrievalBOs;
        }
        public List<RetrievalBO> getRetrievalBOsByStatusAndItemNumber(string statusPar, string itemNumber)//get spec itemNumber Retrievals divided by each department
        {
            string status = null;
            if (statusPar == "Unfullfill Orders")
            {
                status = "Unfulfilled";
            }
            else if (statusPar == "New Orders")
            {
                status = "New";
            }
            context = new ADTeam1Entities();
            List<RetrievalBO> retrievalBOs = new List<RetrievalBO>();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.Where(x => x.Status == status && x.Item.ItemNumber == itemNumber && x.Requisition.Status == "Approved").ToList();
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                RetrievalBO retrievalBO = new RetrievalBO();
                Item item = r_Item.Item;
                retrievalBO.Bin = item.Bin;
                retrievalBO.ItemNumber = item.ItemNumber;
                retrievalBO.Description = item.Description;
                retrievalBO.UOM = item.UnitOfMeasure;
                retrievalBO.InStock = (int)item.InStockQty;
                retrievalBO.DepartmentID = (int)r_Item.Requisition.DepartmentID;
                retrievalBO.Requested = (int)r_Item.RequestedQty;
                retrievalBO.Unfulfilled = (int)r_Item.UnfulfilledQty;
                retrievalBO.ToRetrieve = (int)r_Item.ToRetrieveQty;
                retrievalBO.DepartmentName = r_Item.Requisition.Department.DepartmentName;
                retrievalBO.RequisitionItemID = r_Item.Requisition_ItemID;
                retrievalBOs.Add(retrievalBO);
            }
            return retrievalBOs;
        }

        public List<RetrievalBO> suggestAndGetRetrievalBOsByStatusAndItemNumber(string status, string itemNumber)//get Retrievals dividing each department and suggest to retrieve number
        {
            List<RetrievalBO> retrievalBOs = getRetrievalBOsByStatusAndItemNumber(status, itemNumber);
            int requestedQty = 0; //change to null or int
            foreach (RetrievalBO retrievalBO in retrievalBOs)
            {
                requestedQty = retrievalBO.Unfulfilled + requestedQty;
            }
            Item item = context.Items.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
            int inStock = (int)item.InStockQty;
            if (requestedQty <= inStock)
            {
                foreach (RetrievalBO retrievalBO in retrievalBOs)
                {
                    retrievalBO.ToRetrieve = retrievalBO.Unfulfilled;
                }
            }
            else if (requestedQty > inStock)
            {

                foreach (RetrievalBO retrievalBO in retrievalBOs)
                {
                    retrievalBO.ToRetrieve = (int) Math.Round(inStock * 1.0 / requestedQty * retrievalBO.Unfulfilled);

                }

            }
            return retrievalBOs;
        }

        public int updateRetrievalBOs(List<RetrievalBO> retrievalBOs)
        {
            int changes;
            context = new ADTeam1Entities();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.ToList();
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                foreach (RetrievalBO retrievalBO in retrievalBOs)
                {
                    if (r_Item.Requisition_ItemID == retrievalBO.RequisitionItemID)
                    {
                        r_Item.ToRetrieveQty = retrievalBO.ToRetrieve;
                    }
                }
            }
            changes = context.SaveChanges();
            return changes;
        }

        public int updateRetrievalBOsStatus(List<RetrievalBO> retrievalBOs, string statusPar)
        {
            string status = null;
            if (statusPar == "Unfullfill Orders")
            {
                status = "Unfulfilled";
            }
            else if (statusPar == "New Orders")
            {
                status = "New";
            }
            int changes;
            context = new ADTeam1Entities();
            List<Requisition_Item> requisition_Items = context.Requisition_Item.Where(x => x.Status == status).ToList();
            foreach (Requisition_Item r_Item in requisition_Items)
            {
                foreach (RetrievalBO retrievalBO in retrievalBOs)
                {
                    if (r_Item.Item.ItemNumber == retrievalBO.ItemNumber)
                    {
                        r_Item.Item.InStockQty = r_Item.Item.InStockQty - (int)context.Requisition_Item.Where(x => x.Status == status && x.Requisition.Status == "Approved" && x.ItemID == r_Item.ItemID).Sum(x => x.ToRetrieveQty);
                        r_Item.Status = retrievalBO.Status;
                        r_Item.UnfulfilledQty = r_Item.UnfulfilledQty - r_Item.ToRetrieveQty;
                       // r_Item.UnfulfilledQty = r_Item.RequestedQty - r_Item.ToRetrieveQty;
                        r_Item.RetrievedQty = r_Item.ToRetrieveQty;
                        r_Item.ToRetrieveQty = 0;

                    }
                }
            }
            changes = context.SaveChanges();
            return changes;
        }
    }
}
