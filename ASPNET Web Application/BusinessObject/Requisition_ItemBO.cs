using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Requisition_ItemBO
    {
        private int requisition_ItemID;
        private int requisitionID;
        private int itemID;
        private int? requestedQty;
        private int? receivedQty;
        private int? unfulfilledQty;
        private string status;
        private int? retrievedQty;
        private int? toRetrievedQty;

        private string description;
        private string uom;
        private string itemNumber;
        public int Requisition_ItemID
        {
            get
            {
                return requisition_ItemID;
            }

            set
            {
                requisition_ItemID = value;
            }
        }

        public int RequisitionID
        {
            get
            {
                return requisitionID;
            }

            set
            {
                requisitionID = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        public int? RequestedQty
        {
            get
            {
                return requestedQty;
            }

            set
            {
                requestedQty = value;
            }
        }

        public int? ReceivedQty
        {
            get
            {
                return receivedQty;
            }

            set
            {
                receivedQty = value;
            }
        }

        public int? UnfulfilledQty
        {
            get
            {
                return unfulfilledQty;
            }

            set
            {
                unfulfilledQty = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public int? RetrievedQty
        {
            get
            {
                return retrievedQty;
            }

            set
            {
                retrievedQty = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Uom
        {
            get
            {
                return uom;
            }

            set
            {
                uom = value;
            }
        }

        public string ItemNumber
        {
            get
            {
                return itemNumber;
            }

            set
            {
                itemNumber = value;
            }
        }

        public int? ToRetrievedQty
        {
            get
            {
                return toRetrievedQty;
            }

            set
            {
                toRetrievedQty = value;
            }
        }
    }
}
