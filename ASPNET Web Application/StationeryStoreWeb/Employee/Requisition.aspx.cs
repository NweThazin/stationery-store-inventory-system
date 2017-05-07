using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.Employee
{
    public partial class CurrentRequisition : System.Web.UI.Page
    {
        RequisitionBL bl = new RequisitionBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["login"].ToString().Equals("") || Session["userLoggedIn"].ToString().Equals(""))
                {
                    Response.Redirect("~/LoggedInPage.aspx");
                }
                else
                {
                    LoginBO loginBO = (LoginBO)Session["login"];
                    UserBO userBO = (UserBO)Session["userLoggedIn"];

                    requisitionGV.DataSource = bl.getRequistionListBL(loginBO.DepID);
                    requisitionGV.DataBind();
                }
            }
        }

        protected void requisitionGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                HiddenField hd = requisitionGV.Rows[e.NewSelectedIndex].FindControl("hdnReqNumber") as HiddenField;
                string reqNumberStr = hd.Value;
                int reqNum = Convert.ToInt32(reqNumberStr.Substring(1, reqNumberStr.Length - 1));
                Response.Redirect("~/Employee/RequisitionDetail.aspx?rID=" + reqNum);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt1 = DateTime.Parse(txtFromDate.Text); //start Date
                DateTime dt2 = DateTime.Parse(txtToDate.Text);//End Date
                lblErrorMessage.Text = "";
                LoginBO loginBO = (LoginBO)Session["login"]; //Search BY Date and logged in dept ID
                requisitionGV.DataSource = bl.searchRequisitionList(txtFromDate.Text, txtToDate.Text, loginBO.DepID);
                requisitionGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void requisitionGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                LoginBO loginBO = (LoginBO)Session["login"];
                requisitionGV.PageIndex = e.NewPageIndex;
                requisitionGV.DataSource = bl.getRequistionListBL(loginBO.DepID);
                requisitionGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}