using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject.StoreClerk;
using BusinessObject;

namespace ADProjectSA43_Team1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        LoginBO loginBO = (LoginBO)Session["login"];
                        lblUserName.Text = loginBO.EmpName;

                        if (Session["role"].ToString().Equals("StoreManager"))
                        {
                            lnkRetrieval.Visible = false;
                            lnkDistribution.Visible = false;
                            lnkPurchase.Visible = false;
                            lnkbtnDelegateSupervisor.Visible = false;
                            lnkSupplier.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("StoreSupervisor"))
                        {
                            lnkRetrieval.Visible = false;
                            lnkDistribution.Visible = false;
                            lnkPurchase.Visible = false;
                            lnkbtnDelegateManager.Visible = false; //Confirm and ask and change
                            lnkSupplier.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("ClerkManager"))
                        {
                            lnkbtnDelegateManager.Visible = false;
                            lnkbtnDelegateSupervisor.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("ClerkSupervisor"))
                        {
                            lnkbtnDelegateManager.Visible = false;
                            lnkbtnDelegateSupervisor.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("StoreClerk"))
                        {
                            lnkbtnDelegateManager.Visible = false;
                            lnkbtnDelegateSupervisor.Visible = false;
                            lnkStockVocher.Visible = false;
                        }
                        else
                        {
                            Response.Redirect("~/LoggedInPage.aspx");
                        }
                    }
                }

            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void lnkPurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/PurchaseOrder.aspx");
        }

        protected void lnkInventory_Click(object sender, EventArgs e)
        {
            //check and allow store manager and store supervisor to edit the inventory lists
            if (Session["role"].ToString().Equals("StoreManager") || Session["role"].ToString().Equals("StoreSupervisor"))
            {
                Response.Redirect("~/StoreClerk/InventoryPageEdit.aspx");
            }
            else if (Session["role"].ToString().Equals("ClerkManager") || Session["role"].ToString().Equals("ClerkSupervisor") || Session["role"].ToString().Equals("StoreClerk"))
            {
                Response.Redirect("~/StoreClerk/InventoryPage.aspx");
            }
        }

        protected void lnkReorderReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/ReorderReport.aspx");
        }

        protected void lnkStatusReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/InventoryReport.aspx");
        }

        protected void lnkRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/RetrievalPage.aspx");
        }

        protected void lnkDistribution_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/Distribution.aspx");
        }

        protected void lnkStockVocher_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/AdjustmentVouchersList.aspx");
        }

        protected void lnkbtnDelegateSupervisor_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/DelegateBySupervisor.aspx");
        }

        protected void lnkbtnDelegateManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/DelegateByManager.aspx");
        }

        protected void lnkSupplier_Click(object sender, EventArgs e)
        {
                Response.Redirect("~/StoreClerk/SupplierExport.aspx");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session["userLoggedIn"] = ""; //For login
            Session["login"] = ""; //For login information
            Response.Redirect("~/LoggedInPage.aspx");
        }

        protected void lnkReqReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/RequisitionReport.aspx");
        }

        protected void lnkPurchaseTrendReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/PurchaseReport.aspx");
        }
    }
}