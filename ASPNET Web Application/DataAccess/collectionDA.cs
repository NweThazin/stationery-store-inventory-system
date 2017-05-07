using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class collectionDA
    {
        ADTeam1Entities context = new ADTeam1Entities();

        //get the collection point list
        public List<CollectionPoint> getCollectionList()
        {
            var qry = context.CollectionPoints.ToList<CollectionPoint>();
            return qry;
          
        }

        //get the departmentName and show in the screen
        public string getDepartmentName(int departmentID)
        {
            string name = context.Departments.Where(x => x.DepartmentID == departmentID).Select(x => x.DepartmentName).FirstOrDefault();
            return name;
        }
        //Get Collection Point id
        public int? getCollectionPoint(int departmentID)
        {
            int? collectionpoint= context.Departments.Where(x => x.DepartmentID == departmentID).Select(x => x.CollectionPointID).FirstOrDefault();
            return collectionpoint;
        }

        //getCollectionPointName
        public string getName(int collectionPointid)
        {
            string cname = context.CollectionPoints.Where(x => x.CollectionPointID== collectionPointid).Select(x => x.Name).FirstOrDefault();
            return cname;
        }

        //updatecollectionPoint
        public void update(int collectionPointID,int departmentID)
        {
            var qry=context.Departments.Where(x => x.DepartmentID == departmentID).FirstOrDefault();
            Department c = (Department)qry;
            if (c.CollectionPointID != collectionPointID)
            {
                c.CollectionPointID = collectionPointID;
            }
            context.SaveChanges();
        }
    }
}
