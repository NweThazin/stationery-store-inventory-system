using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace BusinessLayer
{
    public class AdjsutmentVouchersBL
    {
        AdjustmentVouchersDA da = new AdjustmentVouchersDA();

        //For Adjustment Vounchers list for Manager and Supervisor
        public List<AdjustmentBO> getAdjustmentByManagerandSupervisor(string status,int id)
        {
            return da.getAdjustmentByManagerandSupervisor(status,id);
        }

        public List<AdjustmentBO> getAdjustmentList()
        {
            return da.getAllAdjustmentList();
        }

        public List<AdjustmentVoucherDetailBO> getAdjustmentDetail(int adjID)
        {
            return da.getAdjustmentDetailed(adjID);
        }

        public AdjustmentBO getAdjustment(int id)
        {
            return da.getAdjustmentById(id);
        }

        public int ApprovedOrRejectedBL(int num, int adjID)
        {
            return da.ApprovedOrRejected(num, adjID);
        }

        public int CheckPending(int adjNum)
        {
            return da.CheckPending(adjNum);
        }

        public EmployeeBO getEmpInfoByAdjsutmentID(int adjNum)
        {
            return da.getEmpInfoByAdjsutmentID(adjNum);
        }

        public int checkInStockAdjustment(List<AdjustmentVoucherDetailBO> lst)
        {
            return da.changeInStockByApproveAndReject(lst);
        }
    }
}
