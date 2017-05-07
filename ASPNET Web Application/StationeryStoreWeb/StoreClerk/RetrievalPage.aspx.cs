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
    public partial class RetrievalPage : System.Web.UI.Page
    {
        string status;//whether its unfullfilled or new order
        List<RetrievalBO> retrievals;
        RetrievalBL retrievalBL;
        List<int> ckbList;
        List<int> toRetrieveList;
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
                        try
                        {
                            if (Request.QueryString["status"] == "Unfullfill Orders")
                            {
                                radbtnReterival.ClearSelection();
                                radbtnReterival.Items[0].Selected = true;
                            }
                            else if (Request.QueryString["status"] == "New Orders")
                            {
                                radbtnReterival.ClearSelection();
                                radbtnReterival.Items[1].Selected = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                    }
                    else
                    {
                        try
                        {
                            ckbList = new List<int>();
                            toRetrieveList = new List<int>();
                            for (int i = 0; i < GVRetrieval.Rows.Count; i++)
                            {
                                CheckBox ckb = GVRetrieval.Rows[i].FindControl("Ckb") as CheckBox;
                                int toRetreive = Int32.Parse(GVRetrieval.Rows[i].Cells[8].Text);
                                if (ckb.Checked)
                                {
                                    ckbList.Add(i);
                                    toRetrieveList.Add(toRetreive);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                    }
                    status = radbtnReterival.SelectedValue;
                    retrievals = new List<RetrievalBO>();
                    retrievalBL = new RetrievalBL();
                    retrievals = retrievalBL.getCombinedRetrievalBOsByStatus(status);
                    if (retrievals.Count == 0) {
                        btnConfirm.Visible = false;
                        lblListName.Text = "No orders list.";
                    }
                    else
                    {
                        btnConfirm.Visible = true;
                       lblListName.Text = "";
                    }
                    GVRetrieval.DataSource = retrievals;
                    GVRetrieval.DataBind();
                    //lblListName.Text = status + " List";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void GVRetrieval_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                string itemNumber = GVRetrieval.Rows[e.NewSelectedIndex].Cells[2].Text;
                int toRetrieveQty = Int32.Parse(GVRetrieval.Rows[e.NewSelectedIndex].Cells[8].Text);
                int inStockQty = Int32.Parse(GVRetrieval.Rows[e.NewSelectedIndex].Cells[5].Text);//new added
                Response.Redirect("Allocate.aspx?status=" + status + "&&itemNumber=" + itemNumber + "&&toRetrieveQty=" + toRetrieveQty + "&&inStock=" + inStockQty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                List<RetrievalBO> confirmedRetrievals = new List<RetrievalBO>();
                bool validate = true;
                for (int i = 0; i < ckbList.Count; i++)
                {
                    int index = ckbList[i];
                    RetrievalBO confirmedRetrievalBO = new RetrievalBO();
                    confirmedRetrievalBO = retrievals[index];
                    confirmedRetrievalBO.Status = "Pending";
                    confirmedRetrievals.Add(confirmedRetrievalBO);
                    if (toRetrieveList[i] == 0)
                    {
                        validate = false;
                    }
                }
                if (validate)
                {
                    retrievalBL.addDisbursementItem(confirmedRetrievals, status).ToString();
                    retrievalBL.updateRetrievalBOsStatus(confirmedRetrievals, status).ToString();
                    Response.Redirect("RetrievalPage.aspx?status=" + status);
                }
                else
                {
                    LblValidate.Text = "must allocate before retrieve";
                }
            }catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}