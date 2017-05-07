using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class InventoryPage : System.Web.UI.Page
    {
        InventoryPageBL bl = new InventoryPageBL();
        EncryptDecryptBL edbl = new EncryptDecryptBL(); //For encrypt decrypt
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
                        inventoryPageGV.DataSource = bl.getInventoryList(); //get all inventory lists
                        inventoryPageGV.DataBind();
                        ViewState["searchType"] = "";
                    }
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/StoreClerk/AddItem.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/StoreClerk/InventoryAdjustment.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Pagination 
        protected void inventoryPageGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (ViewState["searchType"].Equals(""))
                {
                    inventoryPageGV.PageIndex = e.NewPageIndex;
                    inventoryPageGV.DataSource = bl.getInventoryList(); //show all data lists
                    inventoryPageGV.DataBind();
                }
                else
                {
                    if (ViewState["searchType"].Equals("ItemNumber"))
                    {
                        inventoryPageGV.PageIndex = e.NewPageIndex; //Show inventory lists which contain item number
                        inventoryPageGV.DataSource = bl.searchInventoryByNumberOrDescription(txtEnterSearch.Text, null);
                        inventoryPageGV.DataBind();
                    }
                    else if (ViewState["searchType"].Equals("Description"))
                    {
                        inventoryPageGV.PageIndex = e.NewPageIndex;//Show inventory list which contain description
                        inventoryPageGV.DataSource = bl.searchInventoryByNumberOrDescription(null, txtEnterSearch.Text);
                        inventoryPageGV.DataBind();
                    }
                }
            }catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void lnkbtnItemNumber_Click(object sender, EventArgs e)
        {
            try
            {
                txtEnterSearch.Attributes["placeholder"] = "Enter Item Number to search..";
                ViewState["searchType"] = "ItemNumber";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void lnkbtnDescription_Click(object sender, EventArgs e)
        {
            try
            {
                txtEnterSearch.Attributes["placeholder"] = "Enter Description to search..";
                ViewState["searchType"] = "Description";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Search Function
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemBO> blst = new List<ItemBO>();
                Boolean flag = false;

                if (ViewState["searchType"].Equals("ItemNumber"))
                {
                    blst = bl.searchInventoryByNumberOrDescription(txtEnterSearch.Text, null);
                }
                else if (ViewState["searchType"].Equals("Description"))
                {
                    blst = bl.searchInventoryByNumberOrDescription(null, txtEnterSearch.Text);
                }
                else if (ViewState["searchType"].Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Can Search By Item Number and Description')", true);
                    blst = bl.getInventoryList();
                }
                if (blst.Count != 0)
                {
                    flag = true;
                }
                if (flag == true)
                {
                    inventoryPageGV.DataSource = blst; //bind the data list
                    inventoryPageGV.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No inventory Lists')", true);
                    inventoryPageGV.DataSource = bl.getInventoryList();
                    inventoryPageGV.DataBind();
                    ViewState["searchType"] = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //When click edit button, go to edit item page
        protected void inventoryPageGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string itemNumber = inventoryPageGV.Rows[e.NewEditIndex].Cells[0].Text;
                Response.Redirect("~/StoreClerk/EditItem.aspx?itemNumber=" + itemNumber);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //when user click select button, go to Stock Card Page
        protected void inventoryPageGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                string itemNumber = inventoryPageGV.Rows[e.NewSelectedIndex].Cells[0].Text;
                Response.Redirect("~/StoreClerk/StockCard.aspx?itemCode=" + itemNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}