using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.Employee
{
    public partial class DisbursementList : System.Web.UI.Page
    {
        LoginBL bl = new LoginBL();
        DestinationBL destinationBL;
        int departmentID;

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
                        LoginBO user = (LoginBO)Session["login"];
                        departmentID = user.DepID;

                        if (!IsPostBack)
                        {
                            destinationBL = new DestinationBL();
                            Session["items"] = destinationBL.getDisbursementItems(departmentID);
                            disbursementGV.DataSource = Session["items"];
                            disbursementGV.DataBind();
                        }
                    }
                }//End of Ispost backevent
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void disbursementGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                disbursementGV.EditIndex = e.NewEditIndex;
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();

            } catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }
        }

        protected void disbursementGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                LoginBO user = (LoginBO)Session["login"];
                departmentID = user.DepID;

                HiddenField hdn = (HiddenField)disbursementGV.Rows[e.RowIndex].FindControl("HiddenField1");
                HiddenField hdf = (HiddenField)disbursementGV.Rows[e.RowIndex].FindControl("HiddenField2");

                int itemID = Convert.ToInt32(hdn.Value);
                int disbursementID = Convert.ToInt32(hdf.Value);
                TextBox txtQuantity = disbursementGV.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
                string qty = txtQuantity.Text;
                int quantity = Convert.ToInt32(qty);
                destinationBL = new DestinationBL();
                destinationBL.updateDisbursement(departmentID, disbursementID, itemID, quantity);
                disbursementGV.EditIndex = -1;
                Session["items"] = destinationBL.getDisbursementItems(departmentID);

                //Bind in the gridview
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        protected void disbursementGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                disbursementGV.EditIndex = -1;
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                LoginBO user = (LoginBO)Session["login"];
                departmentID = user.DepID;
                destinationBL = new DestinationBL();
                destinationBL.confirmDisbursement(departmentID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void disbursementGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                disbursementGV.PageIndex = e.NewPageIndex;
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}