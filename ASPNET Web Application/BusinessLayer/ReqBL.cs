using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;
using BusinessObject.StoreClerk;

namespace BusinessLayer
{
   public  class ReqBL
    {
        ReqDA da = new ReqDA();

        //For Stationery Catalogue Page
        //1..Get Description
        public string getDescription(int itemId)
        {
            string description = da.getDescription(itemId);
            return description;
        }
        //2.. Get Item Number
        public string getItemNumber(int itemId)
        {
            string itemNumber = da.getItemNumberbyId(itemId);
            return itemNumber;
        }

        //3..Save to Database
        public void addRequisitionList(List<ReqBO> rlst,LoginBO loginBo)
        {
            //create purchase object
            Requisition robj = new Requisition();
            robj.DepartmentID = loginBo.DepID;
            robj.EmployeeID = loginBo.EmpID;
            robj.Status = "Pending";
            robj.OrderDate = DateTime.Now; 
            robj.Reason = "";

            //For requistion items list
            List<Requisition_Item> rst = new List<Requisition_Item>();
            foreach (ReqBO r in rlst) //from parameter pass
            {
                Requisition_Item ri = new Requisition_Item();
                ri.ItemID = r.ItemID;
                ri.Status = "New";
                ri.RequestedQty = r.RequestedQty;
                ri.ReceivedQty = 0;
                ri.UnfulfilledQty = r.RequestedQty;
                ri.RetrievedQty = 0;
                ri.ToRetrieveQty = 0;
                rst.Add(ri);
            }
            da.addRequisitionList(robj, rst);
        }
    }
}
