using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
namespace BusinessLayer
{
    public class CollectionPointBL
    {
        CollectionPointDA collectionPointDA;
        public List<CollectionPointBO> getCollectionPointList()
        {
            collectionPointDA = new CollectionPointDA();
            return collectionPointDA.getCollectionPointList();
        }
    }
}
