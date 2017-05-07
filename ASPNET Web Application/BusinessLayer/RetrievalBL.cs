using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.StoreClerk;
using DataAccess;
namespace BusinessLayer
{
    public class RetrievalBL
    {
        List<RetrievalBO> retrievalBOsDivided;

        public List<RetrievalBO> getRetrievalBOsByStatus(string status)//get Retrievals dividing each department
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            retrievalBOsDivided = retrievalDA.getRetrievalBOsByStatus(status);
            return retrievalBOsDivided;
        }
        public List<RetrievalBO> getCombinedRetrievalBOsByStatus(string status)//get Retrievals combining each department
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            List<RetrievalBO> retrievalBOsCombined = retrievalDA.getCombinedRetrievalBOsByStatus(status);
            return retrievalBOsCombined;
        }
        public List<RetrievalBO> getRetrievalBOsByStatusAndItemNumber(string status, string itemNumber)//get Retrievals dividing each department
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            retrievalBOsDivided = retrievalDA.getRetrievalBOsByStatusAndItemNumber(status, itemNumber);
            return retrievalBOsDivided;
        }

        public List<RetrievalBO> suggestAndGetRetrievalBOsByStatusAndItemNumber(string status, string itemNumber)//get Retrievals dividing each department
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            retrievalBOsDivided = retrievalDA.suggestAndGetRetrievalBOsByStatusAndItemNumber(status, itemNumber);
            return retrievalBOsDivided;
        }

        public int updateRetrievalBOs(List<RetrievalBO> retrievalBOs)
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            return retrievalDA.updateRetrievalBOs(retrievalBOs);
        }

        public int updateRetrievalBOsStatus(List<RetrievalBO> retrievalBOs,string status)
        {
            RetrievalDA retrievalDA = new RetrievalDA();
            return retrievalDA.updateRetrievalBOsStatus(retrievalBOs,status);
        }

        public int addDisbursementItem(List<RetrievalBO> retrievalBOs, string status)
        {
            DisbursementDA disbursementDA = new DisbursementDA();           
            return disbursementDA.addDisbursementItem(retrievalBOs, status);
        }
    }
}
