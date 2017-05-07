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
    public class DestinationBL
    {
        DisbursementDA disbursementDA = new DisbursementDA();
        public List<CollectionPointBO> getCollectionPointDeptList(int CollectionPointID)
        {
            return disbursementDA.getCollectionPointDeptList(CollectionPointID);
        }

        public string getCollectionPointName(int collectionPointID)
        {
            return disbursementDA.getCollectionPointName(collectionPointID);
        }

        public string getDepartmentName(int departmentID)
        {
            return disbursementDA.getDepartmentName(departmentID);
        }

        public List<DisbursementBO> getDisbursementItems(int departmentID)
        {
            return disbursementDA.getDisbursementItems(departmentID);
        }

        public void updateDisbursement(int departmentID, int disbursementID, int itemID, int qty)
        {
            disbursementDA.updateDisbursement(departmentID, disbursementID, itemID, qty);
        }

        public void confirmDisbursement(int departmentID)
        {
            disbursementDA.confirmDisbursement(departmentID);
        }
    }
}
