using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject.StoreClerk;
using BusinessObject;

namespace ADProjectSA43_Team1.Employee
{
    public partial class StationeryCatalogue : System.Web.UI.Page
    {
      
        public const string SELECTED_ITEMS_INDEX = "SelectedItemsIndex";

        InventoryPageBL bl = new InventoryPageBL();
        ReqBL rl = new ReqBL();
        List<ReqBO> rlst = new List<ReqBO>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["login"].ToString().Equals("") || Session["userLoggedIn"].ToString().Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Bind the data in gridview first
                        catalogueGV.DataSource = bl.getInventoryList();
                        catalogueGV.DataBind();

                        ViewState["searchType"] = "";

                        rlst = (List<ReqBO>)Session["cart"];
                        //Second GridView
                        if (rlst.Count != 0)
                        {
                            cartView.DataSource = rlst;
                            cartView.DataBind();
                            btnSave.Visible = true; btnCancel.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false; btnCancel.Visible = false;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Search by Item Number
        //protected void lnkbtnItemNumber_Click(object sender, EventArgs e)
        //{
        //    txtEnterSearch.Attributes["placeholder"] = "Enter Item Number to search..";
        //    ViewState["searchType"] = "ItemNumber";
        //}
        //Search By Item Description
        protected void lnkbtnDescription_Click(object sender, EventArgs e)
        {
            txtEnterSearch.Attributes["placeholder"] = "Enter Description to search..";
            ViewState["searchType"] = "Description";
        }

        //Search Function
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
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
                blst = bl.getInventoryList();
            }
            if (blst.Count != 0)
            {
                flag = true;
            }
            if (flag == true)
            {
                catalogueGV.DataSource = blst;
                catalogueGV.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No inventory Lists')", true);
                catalogueGV.DataSource = bl.getInventoryList();
                catalogueGV.DataBind();
            }
        }

        //Pagination
        protected void catalogueGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (ViewState["searchType"].Equals(""))
                { 
                    catalogueGV.PageIndex = e.NewPageIndex;
                    catalogueGV.DataSource = bl.getInventoryList();
                    catalogueGV.DataBind();
                }
                else
                {
                    if (ViewState["searchType"].Equals("ItemNumber"))
                    {
                        catalogueGV.PageIndex = e.NewPageIndex;
                        catalogueGV.DataSource = bl.searchInventoryByNumberOrDescription(txtEnterSearch.Text, null);
                        catalogueGV.DataBind();

                    }
                    else if (ViewState["searchType"].Equals("Description"))
                    {
                        catalogueGV.PageIndex = e.NewPageIndex;
                        catalogueGV.DataSource = bl.searchInventoryByNumberOrDescription(null, txtEnterSearch.Text);
                        catalogueGV.DataBind();
                    }
                }
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private int  isExiting(int id)
        {
            List<ReqBO> cart = (List<ReqBO>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ItemID == id)
                    return i;
            return -1;
        }

        protected void cartView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField hd = (HiddenField)FindControl("HiddenField2");
                int itemID = Convert.ToInt32(hd.Value);
                rlst = (List<ReqBO>)Session["cart"];
                rlst.RemoveAll(x => x.ItemID == itemID); //Remove from the list of the item

                //Bind the data list to gridview
                cartView.DataSource = rlst;
                cartView.DataBind();

                if (cartView.Rows.Count >= 1)
                {
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    //Change need to ask Jane
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkb = (LinkButton)sender;
                HiddenField hif = (HiddenField)linkb.FindControl("HiddenField2");
                int id = Convert.ToInt32(hif.Value);

                List<ReqBO> cart = (List<ReqBO>)Session["cart"];
                cart.RemoveAt(isExiting(id));
                
                //Assign list to session again
                Session["cart"] = cart;

                this.cartView.DataSource = Session["cart"];
                this.cartView.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void LinkButtonOrder_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = (LinkButton)sender;
                HiddenField hd = (HiddenField)lb.FindControl("HiddenField1"); //get hidden field control from gridview
                TextBox tb = (TextBox)lb.FindControl("qty"); //get textbox control from gridview

                if (tb == null || int.Parse(tb.Text) == 0 || tb.Text=="") //check textbox
                {
                    //text box value is zero and null
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Zero and characters are not allowed')", true);
                }
                else
                { 
                    ReqBO rb = new ReqBO();
                    rlst = (List<ReqBO>)Session["cart"];

                    int id = Convert.ToInt32(hd.Value);
                    rb.ItemID = id;
                    rb.Description = rl.getDescription(id); //get item description
                    rb.ItemNumber = rl.getItemNumber(id); //get item number

                    int qty = Convert.ToInt32(tb.Text);
                    rb.RequestedQty = qty;
                    rb.Status = "Pending";
                    int index = isExiting(id);
                    if (index == -1)
                    {
                        rlst.Add(rb);
                        Session["cart"] = rlst;
                    }
                    else
                    {
                        rlst[index].RequestedQty += rb.RequestedQty;
                        Session["cart"] = rlst;
                    }

                    cartView.DataSource = Session["cart"];
                    cartView.DataBind();

                    if (cartView.Rows.Count >= 1)
                    {
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else //ask to jane
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                    tb.Text = "";//after add show nothing in textbox
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LoginBO loginBo = (LoginBO)Session["login"];
                rlst = (List<ReqBO>)Session["cart"];
                rl.addRequisitionList(rlst, loginBo);
                
                //Change to New Original Default // TELL JANE ABOUT this
                cartView.DataSource = null;
                cartView.DataBind();
                Session["cart"] = new List<ReqBO>();
                if (cartView.Rows.Count >= 1)
                {
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }
                else //ask to jane
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                cartView.DataSource = null;
                cartView.DataBind();
                if (cartView.Rows.Count >= 1)
                {
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    //Change need to ask Jane
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                Session["cart"] = new List<ReqBO>();
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void cartView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                cartView.PageIndex = e.NewPageIndex;
                cartView.DataSource = Session["cart"];
                cartView.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

    

