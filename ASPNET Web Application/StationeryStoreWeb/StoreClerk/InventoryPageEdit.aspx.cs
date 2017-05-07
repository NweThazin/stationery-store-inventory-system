using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject.StoreClerk;
using BusinessLayer;
using BusinessObject;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class InventoryPageEdit : System.Web.UI.Page
    {
        InventoryPageBL bl = new InventoryPageBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Session Check
                    if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //get all inventory list and 
                        inventoryEditGV.DataSource = bl.getInventoryList();
                        inventoryEditGV.DataBind();
                        ViewState["searchType"] = "";
                    }
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void lnkbtnItemNumber_Click(object sender, EventArgs e)
        {
            try
            {
                txtEnterSearch.Attributes["placeholder"] = "Enter Item Number to search..";
                ViewState["searchType"] = "ItemNumber";

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void lnkbtnDescription_Click(object sender, EventArgs e)
        {
            try
            {
                //add text to place holder
                txtEnterSearch.Attributes["placeholder"] = "Enter Description to search..";
                ViewState["searchType"] = "Description";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

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
                    inventoryEditGV.DataSource = blst; //bind the data list
                    inventoryEditGV.DataBind();

                }else{

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No inventory Lists')", true);
                    inventoryEditGV.DataSource = bl.getInventoryList();
                    inventoryEditGV.DataBind();
                    ViewState["searchType"] = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void inventoryEditGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //Go to Edit Item Page
                string itemNumber = inventoryEditGV.Rows[e.NewEditIndex].Cells[0].Text;
                Response.Redirect("~/StoreClerk/EditItem.aspx?itemNumber=" + itemNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void inventoryEditGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (ViewState["searchType"].Equals(""))
                {
                    inventoryEditGV.PageIndex = e.NewPageIndex;
                    inventoryEditGV.DataSource = bl.getInventoryList(); //show all data lists
                    inventoryEditGV.DataBind();
                }
                else
                {
                    if (ViewState["searchType"].Equals("ItemNumber"))
                    {
                        inventoryEditGV.PageIndex = e.NewPageIndex; //Show inventory lists which contain item number
                        inventoryEditGV.DataSource = bl.searchInventoryByNumberOrDescription(txtEnterSearch.Text, null);
                        inventoryEditGV.DataBind();

                    }
                    else if (ViewState["searchType"].Equals("Description"))
                    {
                        inventoryEditGV.PageIndex = e.NewPageIndex;//Show inventory list which contain description
                        inventoryEditGV.DataSource = bl.searchInventoryByNumberOrDescription(null, txtEnterSearch.Text);
                        inventoryEditGV.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}