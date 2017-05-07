using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject.StoreClerk;
using BusinessObject;

namespace ADProjectSA43_Team1.Employee
{
    public partial class ApproveRequisition : System.Web.UI.Page
    {
        RequisitionBL bl = new RequisitionBL();
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
                        LoginBO loginBO = (LoginBO)Session["login"];
                        UserBO userBO = (UserBO)Session["userLoggedIn"];
                        //Show all requistion lists which status is Pending
                        approveRequistionGV.DataSource = bl.getDeptPendingRequisition(loginBO.DepID);
                        approveRequistionGV.DataBind();
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Approve Requisition Selected Index
        //2.. when click "Details" link go to Detailed Page of Approve Requsition
        protected void lnkbtnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = (LinkButton)sender;
                HiddenField hf = (HiddenField)lb.FindControl("hdnRequisitionID"); //get the value from the hidden field
                int reqID = Convert.ToInt32(hf.Value); //convert to int
                Response.Redirect("~/Employee/ApproveRequisitionDetail.aspx?reqNum=" + reqID); //go to detailed page
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}