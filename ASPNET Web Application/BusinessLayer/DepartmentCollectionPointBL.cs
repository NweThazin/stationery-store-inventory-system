using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;

namespace BusinessLayer
{
    public class DepartmentCollectionPointBL
    {
        collectionDA da = new collectionDA();
        //For Check Collection Point Page
        //1.. get All Collection list();
        public List<CollectionPointBO> getCollectionList()
        {
            List<CollectionPointBO> collectionlist = new List<CollectionPointBO>();
            var clst = da.getCollectionList();
            foreach(CollectionPoint c in clst)
            {
                CollectionPointBO b = new CollectionPointBO();
                b.CollectionPointID = c.CollectionPointID;
                b.Name = c.Name;
                collectionlist.Add(b);
            }
            return collectionlist;
            
        }

        //2.. Get Department Name
        public string getDepartmentName(int departmentID)
        {
            return da.getDepartmentName(departmentID);
        }
        //3.. Get Colelction Point id
        public int? getCollectionPoint(int departmentID)
        {
            return da.getCollectionPoint(departmentID);
        }
        //4.. Get Colellection Point Nmae
        public string getName(int collectionPointid)
        {
            return da.getName(collectionPointid);
        }
        //5.. Update the database
        public void update(int collectionPointID, int departmentID)
        {
            da.update(collectionPointID, departmentID);
        }

    }
}
