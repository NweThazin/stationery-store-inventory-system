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
    public partial class StockCard : System.Web.UI.Page
    {
        StoreCardBL bl = new StoreCardBL();
        EncryptDecryptBL edBl = new EncryptDecryptBL();
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
                        //Show item information and supplier
                        showItemInformation();
                        getStockCardListByItem();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Methods
        public void showItemInformation()
        {
            int flag = 0;
            string itemNumber = Request.QueryString["itemCode"];

            //Show Item Informaiton
            ItemBO b = bl.getItemByItemNumberBL(itemNumber);//check
            lblItemCode.Text = b.ItemNumber;
            lblItemDescription.Text = b.Description;
            lblBin.Text = b.Bin;
            lblUOM.Text = b.UnitOfMeasure;
            lblInStock.Text = b.InStockQty.ToString();

            //Show Supplier Information
            ItemSupplierJoinBO sObj = new ItemSupplierJoinBO();
            foreach (ItemSupplierJoinBO s in bl.getItemInformation(itemNumber))
            {
                flag++;
                if (flag == 1)
                {
                    lblSupplier1.Text = s.SupplierName;
                }
                else if (flag == 2)
                {
                    lblSupplier2.Text = s.SupplierName;
                }
                else if (flag == 3)
                {
                    lblSupplier3.Text = s.SupplierName;
                }
            }
        }

        //Get Card Information By Item Number
        public void getStockCardListByItem()
        {
            string itemNumber = Request.QueryString["itemCode"];
            stockCardGV.DataSource= bl.getStockCardInformationByEachItem(itemNumber);
            stockCardGV.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string key = Request.QueryString["itemCode"];
                Response.Redirect("~/StoreClerk/UpdateStockCard.aspx?itemNumber=" + key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void stockCardGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                stockCardGV.PageIndex = e.NewPageIndex;
                string itemNumber = Request.QueryString["itemCode"];
                stockCardGV.DataSource = bl.getStockCardInformationByEachItem(itemNumber);
                stockCardGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}