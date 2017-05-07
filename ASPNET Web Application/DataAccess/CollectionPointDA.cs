using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class CollectionPointDA
    {
        ADTeam1Entities context;
        public CollectionPointDA()
        {
            context = new ADTeam1Entities();
        }

        public List<CollectionPointBO> getCollectionPointList()
        {
            List<CollectionPoint> collectionPointsEF = new List<CollectionPoint>();
            List<CollectionPointBO> collectionPointsBO = new List<CollectionPointBO>();
            collectionPointsEF = context.CollectionPoints.ToList();

            foreach (CollectionPoint cp in collectionPointsEF)
            {
                CollectionPointBO collectionPointBO = new CollectionPointBO();
                collectionPointBO.CollectionPointID = cp.CollectionPointID;
                collectionPointBO.Name = cp.Name;
                collectionPointBO.Time = cp.Time;
                collectionPointBO.EmployeeID = cp.EmployeeID;
                EmployeeDA employeeDA = new EmployeeDA();
               collectionPointBO.EmployeeName = employeeDA.getEmployeeByID((int)cp.EmployeeID).FirstName + " " + employeeDA.getEmployeeByID((int)cp.EmployeeID).LastName;
                collectionPointsBO.Add(collectionPointBO);
            }
            return collectionPointsBO;
        }

        public List<CollectionPoint> test()
        {
            List<CollectionPoint> collectionPointsEF = new List<CollectionPoint>();
            List<CollectionPointBO> collectionPointsBO = new List<CollectionPointBO>();
            collectionPointsEF = context.CollectionPoints.ToList();
            return collectionPointsEF;
        }
    }
}
