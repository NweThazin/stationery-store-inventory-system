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
    public partial class AdjustmentVouchersList : System.Web.UI.Page
    {
        AdjsutmentVouchersBL bl = new AdjsutmentVouchersBL();
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
                        LoginBO loginBO = (LoginBO)Session["login"];
                   
                        if (Session["role"].Equals("StoreManager") || Session["role"].Equals("ClerkManager"))
                        {
                            //adjustment Price is ABOVE 250
                            if (Session["role"].Equals("ClerkManager"))
                            {
                                adjustmentVoucherGV.DataSource = bl.getAdjustmentByManagerandSupervisor("manager",loginBO.EmpID);
                                adjustmentVoucherGV.DataBind();
                            }
                            else
                            {
                                adjustmentVoucherGV.DataSource = bl.getAdjustmentByManagerandSupervisor("manager",0);
                                adjustmentVoucherGV.DataBind();
                            }
                        }
                        else if (Session["role"].Equals("StoreSupervisor") || Session["role"].Equals("ClerkSupervisor"))
                        {
                            //adjustment Price is below 250
                            if (Session["role"].Equals("ClerkSupervisor"))
                            {
                                adjustmentVoucherGV.DataSource = bl.getAdjustmentByManagerandSupervisor("supervisor", loginBO.EmpID);
                                adjustmentVoucherGV.DataBind();
                            }
                            else
                            {
                                adjustmentVoucherGV.DataSource = bl.getAdjustmentByManagerandSupervisor("supervisor", 0);
                                adjustmentVoucherGV.DataBind();
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

        protected void adjustmentVoucherGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                string adjustmentNumber = adjustmentVoucherGV.Rows[e.NewSelectedIndex].Cells[0].Text;
                Response.Redirect("~/StoreClerk/AdjustmentVouchersDetail.aspx?adjNumber=" + adjustmentNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}