using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;

namespace BusinessLayer
{
    public class RequisitionBL
    {
        RequisitionDA da = new RequisitionDA();

        //For Requistion Page
        //1..Get all requistion list
        public List<RequisitionBO> getRequistionListBL(int deptID)
        {
            return da.getRequistionList(deptID);
        }
        //2..Get Requistion by Requistion ID for Detailed Page
        public RequisitionBO getRequistionByID(int id)
        {
            return da.getRequisitionIDByNumber(id);
        }
        //3.. Get Requisition Detailed List for requisition detailed page
        public List<Requisition_ItemBO> getRequisitionDetailList(int reqID)
        {
            return da.getRequisitionDetail(reqID);
        }
        //4.. Search Requisition List
        public List<RequisitionBO> searchRequisitionList(string fromDate, string toDate,int deptID)
        {
            return da.searchRequisitionByDate(fromDate, toDate, deptID);
        }

        //For Approve Requistion Page
        //1.. Show all requisition list which status is "Pending"
        public List<RequisitionBO> getDeptPendingRequisition(int departmentID)
        {
            return da.getDeptPendingRequisition(departmentID);
        }

        //For Approve Requsition Detailed Page
        //1.. Get requisition item list by ID
        public List<Requisition_ItemBO> getRequisitionItem(int requisitionID)
        {
            return da.getRequisitionItem(requisitionID);
        }
        //2.. Approve Requisition
        public void approveRequisition(int reqID)
        {
            da.approveRequisition(reqID);
        }
        //3.. Reject Requisition
        public void rejectRequisition(int reqID,string reason)
        {
            da.rejectRequisition(reqID, reason);
        }

        public string getEmpName(int reqID)
        {
            return da.getEmpName(reqID);
        }
        public string getEmpEmail(int reqID)
        {
            return da.getEMail(reqID);
        }
    }
}
