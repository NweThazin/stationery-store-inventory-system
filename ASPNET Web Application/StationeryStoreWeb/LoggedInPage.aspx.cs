using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BusinessLayer;

namespace ADProjectSA43_Team1
{
    public partial class LoggedInPage : System.Web.UI.Page
    {
        LoginBL bl = new LoginBL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Login Event
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Get the data
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string sessionID = Session.SessionID;

            //Check the logined in user is correct or not
            UserBO ubo = bl.LoggedInUser(userName, password, sessionID);
            if (ubo == null)
            {
                //If the user name and password are not correct
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User Name and Password is not correct')", true);
            }
            else
            {
                //User Name and Password are correct
                Session["userLoggedIn"] = ubo;
                Session["login"] = bl.getLoginInfo(ubo.UserID);

                if (ubo.PrimaryRole.Equals("Employee") || ubo.PrimaryRole.Equals("Dept Head"))
                {
                    //to check emplyee role
                    checkEmployee(ubo);
                }
                else if (ubo.PrimaryRole.Equals("Store Clerk") || ubo.PrimaryRole.Equals("Store Supervisor") || ubo.PrimaryRole.Equals("Store Manager"))
                {
                    //To check Store role
                    checkStoreClerk(ubo);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wrong Role')", true);
                }
            }
        }

        //Check Employee Role
        protected void checkEmployee(UserBO ubo)
        {
            if (ubo.PrimaryRole.Equals("Dept Head"))
            {
                //1
                Session["role"] = "DeptHead";
                Response.Redirect("~/Employee/ApproveRequisition.aspx");

            } else if (ubo.PrimaryRole.Equals("Employee"))
            {
                if (ubo.DelegatedRole.Equals("Employee Rep"))
                {
                    //3
                    Session["role"] = "EmpRep";
                    Response.Redirect("~/Employee/StationeryCatalogue.aspx");
                }
                else if (ubo.DelegatedRole.Equals("Employee Head"))
                {
                    if (checkDate(ubo))
                    {
                        //4
                        Session["role"] = "EmpHead";
                        Response.Redirect("~/Employee/ApproveRequisition.aspx");
                    }
                    else
                    {
                        Session["role"] = "Emp";
                        Response.Redirect("~/Employee/StationeryCatalogue.aspx");
                    }
                }
                else if (ubo.DelegatedRole.Equals("Employee RepHead"))
                {
                    if (checkDate(ubo))
                    {
                        //5
                        Session["role"] = "EmpRepHead";
                        Response.Redirect("~/Employee/ApproveRequisition.aspx");
                    }
                    else
                    {
                        Session["role"] = "EmpRep";
                        Response.Redirect("~/Employee/StationeryCatalogue.aspx");
                    }
                }
                else
                {
                    //2
                    Session["role"] = "Emp";
                    Response.Redirect("~/Employee/StationeryCatalogue.aspx");
                }
            }
        }

        //Check Store Clerk Role
        protected void checkStoreClerk(UserBO ubo)
        {
            if (ubo.PrimaryRole.Equals("Store Manager"))
            {
                Session["role"] = "StoreManager"; //1
                Response.Redirect("~/StoreClerk/InventoryPageEdit.aspx");
            }
            else if (ubo.PrimaryRole.Equals("Store Supervisor"))
            {
                Session["role"] = "StoreSupervisor";//2
                Response.Redirect("~/StoreClerk/InventoryPageEdit.aspx");
            }
            else //Primary Role is store clerk
            {
                if (ubo.DelegatedRole.Equals("Store Manager"))
                {
                    if (checkDate(ubo))
                    {
                        Session["role"] = "ClerkManager";//4
                        Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
                    }
                    else
                    {
                        Session["role"] = "StoreClerk";
                        Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
                    }
                  
                }
                else if (ubo.DelegatedRole.Equals("StoreSupervisor"))
                {
                    if (checkDate(ubo))
                    {
                        Session["role"] = "ClerkSupervisor"; //5
                        Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
                    }
                    else
                    {
                        Session["role"] = "StoreClerk";
                        Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
                    }
                }
                else
                {
                    Session["role"] = "StoreClerk";//3
                    Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
                }
            }
        }

        //Check Start Date and End Date
        private Boolean checkDate(UserBO ubo)
        {
            DateTime todayDate = DateTime.Today;
            if (ubo != null)
            {
                //Check date with today date
                if (ubo.StartDate <= todayDate)
                {
                    if (todayDate <= ubo.EndDate)
                    {
                        return true;
                    }
                    else{
                        return false;
                    }

                }else{
                    return false;
                }

            }else{
                return false;
            }

        } //End of Check Date
    }
}