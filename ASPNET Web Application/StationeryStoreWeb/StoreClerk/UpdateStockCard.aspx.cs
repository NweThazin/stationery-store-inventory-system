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
    public partial class UpdateStockCard : System.Web.UI.Page
    {
        EncryptDecryptBL edBl = new EncryptDecryptBL();
        UpdateStockCardBL sbl = new UpdateStockCardBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                {
                    Response.Redirect("~/LoggedInPage.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        string itemNumber = Request.QueryString["itemNumber"];
                        ItemBO i = sbl.getEachItem(itemNumber);
                        lblStock.Text = i.ItemNumber + " " + i.Description;

                        ddlSupplier.DataSource = sbl.getSupplierListByItemNumber(itemNumber);
                        ddlSupplier.DataTextField = "SupplierName";
                        ddlSupplier.DataValueField = "SupplierID";
                        ddlSupplier.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string itemNumber = Request.QueryString["itemNumber"];
                ItemBO i = sbl.getEachItem(itemNumber);
                int itemID = i.ItemID;
                int supplierID = int.Parse(ddlSupplier.SelectedValue);

                if (sbl.checkSupplierinPurchaseItem(supplierID, itemID))
                {
                    int purchaseItemID = sbl.getPurchaseItem(itemNumber, itemID, supplierID);
                    int qty = int.Parse(txtAmount.Text);
                    DateTime dt = Convert.ToDateTime(txtCalendar.Text);

                    sbl.updatePurchaseItem(purchaseItemID, qty, dt, itemNumber);//check update and item and purchase item
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successful..')", true);
                    Response.Redirect("~/StoreClerk/StockCard.aspx?itemCode="+itemNumber);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Didn't buy the item from this supplier')", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string itemNumber = Request.QueryString["itemNumber"];
            Response.Redirect("~/StoreClerk/StockCard.aspx?itemCode=" + itemNumber);
        }
    }
}