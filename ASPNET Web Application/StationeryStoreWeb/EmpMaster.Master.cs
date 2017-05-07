using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject.StoreClerk;
using BusinessObject;

namespace ADProjectSA43_Team1
{
    public partial class EmpMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //If there is no login user, go to login page
                    if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Check disbursement list
                        LoginBO loginBO = (LoginBO)Session["login"];

                        lblUserName.Text = loginBO.EmpName;
                        if (Session["role"].ToString().Equals("DeptHead"))
                        {
                            lnkbtnCatalogue.Visible = false;
                            lnkbtnRequisition.Visible = false;
                            //btnNotification.Visible = false;
                            lnkbtnDisbursement.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("Emp"))
                        {
                            lnkbtnApprove.Visible = false;
                            lnkbtnDelegateRole.Visible = false;
                            lnkbtnChange.Visible = false;
                            lnkbtnChangeRep.Visible = false;
                            //  btnNotification.Visible = false;
                            lnkbtnDisbursement.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("EmpRep"))
                        {
                            lnkbtnApprove.Visible = false;
                            lnkbtnDelegateRole.Visible = false;
                            lnkbtnChangeRep.Visible = false;
                        }
                        else if (Session["role"].ToString().Equals("EmpHead"))
                        {
                            lnkbtnCatalogue.Visible = false;
                            lnkbtnRequisition.Visible = false;
                            lnkbtnDelegateRole.Visible = false;
                            //Disbursement myin kwint ma shi
                        }
                        else if (Session["role"].ToString().Equals("EmpRepHead"))
                        {
                            lnkbtnCatalogue.Visible = false;
                            lnkbtnRequisition.Visible = false;
                            lnkbtnDelegateRole.Visible = false;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void lnkbtnCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/StationeryCatalogue.aspx");
        }

        protected void lnkbtnRequisition_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/Requisition.aspx");
        }

        protected void lnkbtnApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/ApproveRequisition.aspx");
        }

        protected void lnkbtnDelegateRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/DelegateHead.aspx");
        }

        //Change Collection Point
        protected void lnkbtnChange_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/CheckCollectionPoint.aspx");
        }

        protected void lnkbtnChangeRep_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/ChangeRepresentative.aspx");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session["cart"] = new List<ReqBO>();
            Session["userLoggedIn"] = ""; //For login
            Session["login"] = ""; //For login information
            Response.Redirect("~/LoggedInPage.aspx");
        }

        protected void lnkbtnDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/DisbursementList.aspx");
        }
    }
}