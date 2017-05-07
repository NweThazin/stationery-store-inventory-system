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
    public partial class InventoryAdjustment : System.Web.UI.Page
    {
        InventoryAdjustmentBL bl = new InventoryAdjustmentBL();
        List<Inventory_AdjustmentBO> adjItemlst;
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
                        lstboxShowItems.DataSource = bl.getItemList(); //check
                        lstboxShowItems.DataTextField = "Description";
                        lstboxShowItems.DataValueField = "ItemNumber";
                        lstboxShowItems.DataBind();
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                txtItemNumber.Text = lstboxShowItems.SelectedValue;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //For only show the 
                Inventory_AdjustmentBO obj = new Inventory_AdjustmentBO();
                double totalPrice = 0.0;

                //Get the item list from session
                adjItemlst = (List<Inventory_AdjustmentBO>)Session["adjustItem"];
                obj.ItemID = bl.getItemIDByNumber(txtItemNumber.Text);//check
                obj.ItemNumber = txtItemNumber.Text;
                obj.Reason = txtReason.Text;

                //get calculated price and sum
                totalPrice = Double.Parse(Session["adjPrice"].ToString());
                int qty = Convert.ToInt16(txtAdjustment.Text);

                if (radbtnChoose.SelectedValue.Equals("Add"))
                {
                    obj.Condition = "1";
                    obj.AdjustmentQty = "+" + txtAdjustment.Text;
                }
                else
                {
                    obj.Condition = "0";
                    obj.AdjustmentQty = "-" + txtAdjustment.Text;
                }

                totalPrice = totalPrice + bl.calculatePrice(txtItemNumber.Text, qty);//check
                Session["adjPrice"] = totalPrice;

                //Add object to the session lists
                adjItemlst.Add(obj);

                //Bind the data list to gridview
                inventoryAdjustmentGV.DataSource = adjItemlst;
                inventoryAdjustmentGV.DataBind();
                inventoryAdjustmentGV.FooterRow.Cells[1].Text = Session["adjPrice"].ToString(); //show the price in footer

                //Show Button
                btnSave.Visible = true;
                txtReason.Text = txtItemNumber.Text = txtAdjustment.Text = "";
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            if (Double.Parse(Session["adjPrice"].ToString()) < 250.00)
            {
                //Send Email to Supervisor
                flag = true;
                lblShow.Text = "The total price of adjustment is less than 250.So,this adjustment is sent to Supervisor";

            }
            else
            {
                //Send Email to Manager
                flag = true;
                lblShow.Text = "The total price of adjustment is greater than 250.So,this adjustment is sent to 'Manager'";
            }
            if (flag == true)
            {
                UserBO userBO = (UserBO)Session["userLoggedIn"];
                double totalPrice = Convert.ToDouble(Session["adjPrice"].ToString());
                adjItemlst = (List<Inventory_AdjustmentBO>)Session["adjustItem"];
                bl.insertAdjustmentItemList(adjItemlst, (int)userBO.EmployeeID, totalPrice);//check

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Adjustment Successful')", true);

                //Set to assign value null and blank
                inventoryAdjustmentGV.DataSource = "";
                inventoryAdjustmentGV.DataBind();
                lblShow.Text = "";
                Session["adjustItem"] = new List<Inventory_AdjustmentBO>();
                btnSave.Visible = false;
            }
        }

        protected void inventoryAdjustmentGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string itemNo = inventoryAdjustmentGV.Rows[e.RowIndex].Cells[0].Text;
                adjItemlst = (List<Inventory_AdjustmentBO>)Session["adjustItem"];

                //Decrease totalPrice
                double totalPrice = double.Parse(Session["adjPrice"].ToString()); int qty = 1;

                foreach (Inventory_AdjustmentBO b in adjItemlst)
                {
                    if (b.ItemNumber.Equals(itemNo))
                    {
                        qty = int.Parse(b.AdjustmentQty);
                    }
                }

                totalPrice = totalPrice - bl.calculatePrice(itemNo, qty);
                Session["adjPrice"] = totalPrice;

                //Remove from the list
                adjItemlst.RemoveAll(x => x.ItemNumber == itemNo);

                //Bind the data list to gridview
                inventoryAdjustmentGV.DataSource = adjItemlst;
                inventoryAdjustmentGV.DataBind();
                inventoryAdjustmentGV.FooterRow.Cells[1].Text = Session["adjPrice"].ToString();

                if (inventoryAdjustmentGV.Rows.Count < 1)
                {
                    btnSave.Visible = false;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}