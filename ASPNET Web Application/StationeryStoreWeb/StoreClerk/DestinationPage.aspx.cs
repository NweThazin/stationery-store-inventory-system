using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class DestinationPage : System.Web.UI.Page
    {
        DestinationBL destinationBL;
        List<CollectionPointBO> destinationBOs;
        int collectionPointID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
            {
                Response.Redirect("~/LoggedInPage.aspx");
            }
            else
            {
                collectionPointID = Int32.Parse(Request.QueryString["collectionPointID"]);
                destinationBL = new DestinationBL();
                collectionPoint.Text = destinationBL.getCollectionPointName(collectionPointID);
                Session["collectionPoint"] = collectionPoint.Text;
                destinationBOs = destinationBL.getCollectionPointDeptList(collectionPointID);
                GVDestination.DataSource = destinationBOs;
                GVDestination.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Distribution.aspx");
        }
    }
}