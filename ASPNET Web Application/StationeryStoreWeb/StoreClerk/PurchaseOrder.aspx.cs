using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        PurchaseItemBL bl = new PurchaseItemBL();
        List<PurchaseOrderBO> purlst;
        static decimal total = 0;
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
                        txtItemNumber.Attributes.Add("readonly", "readonly");
                        //bind the data for list box to 
                        lstboxShowItems.DataSource = bl.getItemList();
                        lstboxShowItems.DataTextField = "Description";
                        lstboxShowItems.DataValueField = "ItemNumber";
                        lstboxShowItems.DataBind();

                        btnPurchase.Visible = false;

                        //Show the purchase item link from reorder page purchase link
                        if (Session["pReorder"].Equals("reorder"))
                        {
                            txtItemNumber.Text = Request.QueryString["itemNumber"];
                            ddlSupplier.DataSource = bl.getSuppliersList(txtItemNumber.Text);
                            ddlSupplier.DataTextField = "SupplierName";
                            ddlSupplier.DataValueField = "SupplierID";
                            ddlSupplier.DataBind();
                            txtDescription.Text = bl.getDescription(txtItemNumber.Text);
                            txtQuantity.Text = bl.getReorderQty(txtItemNumber.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                txtItemNumber.Text = lstboxShowItems.SelectedValue;

                //populate dropdown upon page load
                ddlSupplier.DataSource = bl.getSuppliersList(txtItemNumber.Text);
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierID";
                ddlSupplier.DataBind();

                txtDescription.Text = bl.getDescription(txtItemNumber.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private int isExiting(int id,int supplierID)
        {
            List<PurchaseOrderBO> cart = (List<PurchaseOrderBO>)Session["purchase"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ItemID == id&&cart[i].SupplierID==supplierID)
                    return i;
            return -1;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //For only show the 
                PurchaseOrderBO obj = new PurchaseOrderBO();
                PurchaseItemBL bl = new PurchaseItemBL();

                //Get the item list from session
                purlst = (List<PurchaseOrderBO>)Session["purchase"];
                obj.ItemID = bl.getItemIDByNumber(txtItemNumber.Text);
                obj.ItemNo = txtItemNumber.Text;
                obj.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                obj.SupplierName = bl.getSupplier(Convert.ToInt32(ddlSupplier.SelectedValue));
                obj.ItemDescription = txtDescription.Text;
                obj.Quantity = int.Parse(txtQuantity.Text);
                obj.Price = bl.getPrice(obj.ItemID,obj.SupplierID);
                obj.Amount = (decimal)obj.Quantity * (decimal)obj.Price;
                //added new tell to jane
                obj.SupplierID = int.Parse(ddlSupplier.SelectedValue);
                obj.ExpectedDelivery = DateTime.Parse(txtExpectedDeliveryDate.Text);

                //Add object to the lists sessionlist
                int index = isExiting(obj.ItemID,obj.SupplierID);
                if (index == -1)
                {


                    purlst.Add(obj);
                    total += obj.Amount;
                    Session["purchase"] = purlst;
                }
                else
                {

                    purlst[index].Amount += obj.Amount;
                    purlst[index].Quantity += obj.Quantity;
                    total += obj.Amount;
                    Session["purchase"] = purlst;

                }
               
                

                //Bind the data list to gridview
                GridView1.DataSource = purlst;
                GridView1.DataBind();

                //Show Button
                btnPurchase.Visible = true;
                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                GridView1.FooterRow.Cells[5].Text = total.ToString();

                txtItemNumber.Text = txtDescription.Text = txtQuantity.Text = "";
                ddlSupplier.DataSource = "";
                ddlSupplier.DataBind();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //When Click Purchase Order Save all
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                purlst = (List<PurchaseOrderBO>)Session["purchase"];
                LoginBO login = (LoginBO)Session["login"];
                int empID = login.EmpID;
                bl.addPurchaseOrderList(purlst,empID);//added function
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Purchase Successful')", true);
                //Added
                
                GridView1.DataSource = "";
                GridView1.DataBind();
                Session["purchase"] = new List<PurchaseOrderBO>();
                btnPurchase.Visible = false;
                txtExpectedDeliveryDate.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}