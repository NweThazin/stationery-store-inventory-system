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
    public class LoginBL
    {
        LoginDA da = new LoginDA();
        public UserBO LoggedInUser(string userName, string password,string sessionID)
        {
            User u = da.LoggedInUser(userName, password);
            if (u != null)
            {
                if (u.UserName.Equals(userName) && u.Password.Equals(password))
                {
                    UserBO ubo = new UserBO();
                    ubo.SessionID = sessionID;
                    ubo.UserID = u.UserID;
                    ubo.UserName = u.UserName;
                    ubo.Password = u.Password;
                    ubo.PrimaryRole = u.PrimaryRole;
                    ubo.DelegatedRole = u.DelegatedRole;
                    ubo.EmployeeID = u.EmployeeID;
                    ubo.StartDate = u.StartDate;
                    ubo.EndDate = u.EndDate;
                    return ubo;
                }
            }
            else
            {
                return null;
            }

            return null;
        }
        public LoginBO getLoginInfo(int userID)
        {
            return da.getLoginInfo(userID);
        }
    }
}
