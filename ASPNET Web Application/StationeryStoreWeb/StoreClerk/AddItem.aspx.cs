using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BusinessLayer;
using System.Web.UI.WebControls;
using BusinessObject;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class AddItem : System.Web.UI.Page
    {
        ItemBL bl = new ItemBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                {
                    Response.Redirect("~/LoggedInPage.aspx");
                }
                else
                {
                    //populate dropdown upon page load
                    ddlCategory.DataSource = bl.getCategoryList();
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemBO ib = new ItemBO();
                List<String> list = bl.ListItemNumber(); //check

                //Check if item number already exists
                if (list.Contains(txtItemNumber.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Unsuccessful! Please choose a new item number.')", true);
                }
                else
                {
                    ib.ItemNumber = txtItemNumber.Text;
                    ib.CategoryID = int.Parse(ddlCategory.SelectedValue);
                    ib.ReorderLevel = Convert.ToInt32(txtReorderlevel.Text);
                    ib.ReorderQty = Convert.ToInt32(txtReorderQty.Text);
                    ib.UnitOfMeasure = txtUOM.Text;
                    ib.Description = txtDescription.Text;
                    ib.Bin = txtBin.Text;
                    bl.addItem(ib);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Success! Item is added.');window.location='InventoryPage.aspx';", true);
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
                if (list.Contains(txtItemNumber.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item number already registered. Please choose a new item number.')", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}