using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class EditItem : System.Web.UI.Page
    {
        EncryptDecryptBL edbl = new EncryptDecryptBL();
        EditItemBL editItemBL = new EditItemBL();
        ItemBL bl = new ItemBL();
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
                        string itemNumber = Request.QueryString["itemNumber"];
                        txtItemNumber.Attributes.Add("readonly", "readonly");

                        //populate dropdown upon page load
                        ddlCategory.DataSource = bl.getCategoryList(); //checked
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryID";
                        ddlCategory.DataBind();

                        ItemBO itemObj = editItemBL.getItemByItemNumber(itemNumber); //checked
                        if (itemObj != null)
                        {
                            ViewState["itemID"] = itemObj.ItemID;//get the item id
                            txtItemNumber.Text = itemObj.ItemNumber;
                            //  ddlCategory.SelectedValue = itemObj.CategoryName;
                            ddlCategory.SelectedValue = itemObj.CategoryID.ToString();
                            txtReorderlevel.Text = itemObj.ReorderLevel.ToString();
                            txtReorderQty.Text = itemObj.ReorderQty.ToString();
                            txtUOM.Text = itemObj.UnitOfMeasure;
                            txtDescription.Text = itemObj.Description;
                            txtBin.Text = itemObj.Bin;
                        }
                        else
                        {

                            if (Session["role"].ToString().Equals("StoreManager") || Session["role"].ToString().Equals("StoreSupervisor"))
                            {
                                Response.Redirect("~/StoreClerk/InventoryPageEdit.aspx");
                            }
                            else if (Session["role"].ToString().Equals("ClerkManager") || Session["role"].ToString().Equals("ClerkSupervisor") || Session["role"].ToString().Equals("StoreClerk"))
                            {
                                Response.Redirect("~/StoreClerk/InventoryPage.aspx");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ItemBO ib = new ItemBO();
                List<String> list = bl.ListItemNumber(); //checked
                int itemID = int.Parse(ViewState["itemID"].ToString());

                //Cannot add if itemNumber is not original itemNumber AND in list
                if (txtItemNumber.Text != bl.getItem(itemID).ItemNumber && list.Contains(txtItemNumber.Text)) //item number should be included in the item number lists
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Unsuccessful! Please choose a new item number.')", true);
                }
                else
                {
                    ib.ItemID = itemID;
                    ib.ItemNumber = txtItemNumber.Text;
                    //ib.CategoryID = ddlCategory.SelectedIndex + 1;//ask
                    ib.CategoryID = int.Parse(ddlCategory.SelectedValue);
                    ib.ReorderLevel = Convert.ToInt32(txtReorderlevel.Text);
                    ib.ReorderQty = Convert.ToInt32(txtReorderQty.Text);
                    ib.UnitOfMeasure = txtUOM.Text;
                    ib.Description = txtDescription.Text;
                    ib.Bin = txtBin.Text;
                    bl.editItem(ib); //call the edit method of business layer

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successful!');window.location='InventoryPage.aspx';", true);

                    if (Session["role"].ToString().Equals("StoreManager") || Session["role"].ToString().Equals("StoreSupervisor"))
                    {
                        Response.Redirect("~/StoreClerk/InventoryPageEdit.aspx");
                    }
                    else if (Session["role"].ToString().Equals("ClerkManager") || Session["role"].ToString().Equals("ClerkSupervisor") || Session["role"].ToString().Equals("StoreClerk"))
                    {
                        Response.Redirect("~/StoreClerk/InventoryPage.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void txtItemNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<String> list = bl.ListItemNumber();
                int itemID = int.Parse(ViewState["itemID"].ToString());

                if (txtItemNumber.Text != bl.getItem(itemID).ItemNumber)
                {
                    if (list.Contains(txtItemNumber.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item number already registered. Please choose a new item number.')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}