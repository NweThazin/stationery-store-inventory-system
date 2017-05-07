using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["adjustItem"] = new List<Inventory_AdjustmentBO>();
            Session["purchase"] = new List<PurchaseOrderBO>();
            Session["userLoggedIn"] = ""; //For login
            Session["role"] = ""; //For role assign
            Session["login"] = ""; //For login information
            Session["cart"] = new List<ReqBO>();
            Session["adjPrice"] = 0.0;
            Session["collectionPoint"] = "";
            Session["items"] = "";
            Session["pReorder"] = "";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}