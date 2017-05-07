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
    public partial class Allocate : System.Web.UI.Page
    {
        List<RetrievalBO> retrievals;
        RetrievalBL retrievalBL;
        string status;
        string itemNumber;
        int toRetrieveQty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
            {
                Response.Redirect("~/LoggedInPage.aspx");
            }
            else
            {
                retrievals = new List<RetrievalBO>();
                retrievalBL = new RetrievalBL();
                status = Request.QueryString["status"];
                itemNumber = Request.QueryString["itemNumber"];
                toRetrieveQty = Int32.Parse(Request.QueryString["toRetrieveQty"]);
                if (toRetrieveQty != 0)
                {
                    retrievals = retrievalBL.getRetrievalBOsByStatusAndItemNumber(status, itemNumber);
                    if (!IsPostBack)
                    {
                        GVAllocate.DataSource = retrievals;
                        GVAllocate.DataBind();
                    }
                }
                else if (toRetrieveQty == 0)
                {
                    retrievals = retrievalBL.suggestAndGetRetrievalBOsByStatusAndItemNumber(status, itemNumber);
                    if (!IsPostBack)
                    {
                        GVAllocate.DataSource = retrievals;
                        GVAllocate.DataBind();
                    }
                }
                if (!IsPostBack)
                {
                    ViewState["totalAllocate"] = 0;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("RetrievalPage.aspx?status=" + status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e) //validation 
        {
            bool flag = true; bool isZero = true; bool isStock = true;
            try
            {
                int inStock = int.Parse(Request.QueryString["inStock"]);
                string iNumber = Request.QueryString["itemNumber"];

                for (int i = 0; i < GVAllocate.Rows.Count; i++)
                {
                    TextBox txtToRetrieve = GVAllocate.Rows[i].FindControl("txtToRetrieve") as TextBox;
                    int unfullfilled = int.Parse(GVAllocate.Rows[i].Cells[2].Text); //new added
                    int toRetrieve = Int32.Parse(txtToRetrieve.Text);

                    int totalAllocate = int.Parse(ViewState["totalAllocate"].ToString()) + toRetrieve; //newly added
                    ViewState["totalAllocate"] = totalAllocate;
                  
                    if (toRetrieve == 0)
                    {
                        Label lblShow = GVAllocate.Rows[i].FindControl("lblErrorMessage") as Label;
                        lblShow.Text = "zero is not allowed"; isZero = false;
                    }
                    else
                    {
                        Label lblShow = GVAllocate.Rows[i].FindControl("lblErrorMessage") as Label;
                        lblShow.Text = "";
                        if (toRetrieve <= unfullfilled)
                        {
                            retrievals[i].ToRetrieve = toRetrieve;//new added
                        }
                        else
                        {
                            retrievals[i].ToRetrieve = toRetrieve;//new added
                            flag = false;
                        }
                    }
                }//end of for loop
                int grandTotal = int.Parse(ViewState["totalAllocate"].ToString());
                if (grandTotal > inStock)
                {
                    isStock = false;
                }
                if (flag == true && isZero == true && isStock == true)
                {
                    retrievalBL.updateRetrievalBOs(retrievals);
                    Response.Redirect("RetrievalPage.aspx?status=" + status);
                }
                else if (flag == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Allocated Qty should be less than or equal to Unfullfilled Qty & InStock Qty')", true);
                }
                else if (isStock == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The total Retrieve Qty less than the InStock Qty')", true);
                }
            }catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
        }//end of event
    }
}

