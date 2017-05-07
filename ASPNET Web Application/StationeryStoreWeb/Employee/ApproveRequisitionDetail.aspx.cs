using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using System.IO;
using System.Net.Mail;

namespace ADProjectSA43_Team1.Employee
{
    public partial class ApproveRequisitionDetail : System.Web.UI.Page
    {
        RequisitionBL bl = new RequisitionBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string str = Request.QueryString["reqNum"]; //get requistion number from query string
                    int reqID = int.Parse(str);
                    //lblNumber.Text = "R"+reqID.ToString();
                    //Get the requisition item list by requistion ID
                    approveRequistionDetailGV.DataSource = bl.getRequisitionItem(reqID);
                    approveRequistionDetailGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //try
            //{
                //Approve Requistion
                string str = Request.QueryString["reqNum"];
                int reqID = int.Parse(str);
                bl.approveRequisition(reqID);
                sendGmail("Approved", reqID);
                Response.Redirect("~/Employee/ApproveRequisition.aspx");

            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                string str = Request.QueryString["reqNum"];
                int reqID = int.Parse(str); string reason;
                if (txtReason.Text == "")
                {
                    reason = "";
                }
                else
                {
                    reason = txtReason.Text;
                }
                sendGmail("Rejected", reqID);
                bl.rejectRequisition(reqID, reason);
                Response.Redirect("~/Employee/ApproveRequisition.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void approveRequistionDetailGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                string str = Request.QueryString["reqNum"]; //get requistion number from query string
                int reqID = int.Parse(str);
                approveRequistionDetailGV.PageIndex = e.NewPageIndex;
                approveRequistionDetailGV.DataSource = bl.getRequisitionItem(reqID);
                approveRequistionDetailGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void sendGmail(string status,int requOrderID)
        {
            string subject = "";
            string body = "";
            string last = "";

            //Creating the mail format
            if (status.Equals("Approved"))
            {
                subject = status + " Requisition Order R" + requOrderID;
                body = "Dear " + bl.getEmpName(requOrderID) + ",<br/>Your Requisition Order R" + requOrderID + " has been approved by Department Head";
                last = "Regards,<br/>Logic Univeristy Store";
            }
            else if (status.Equals("Rejected"))
            {
                subject = status + " Requisition Order R" + requOrderID;
                body = "Dear " + bl.getEmpName(requOrderID) + ",<br/>Your Requisition Order R" + requOrderID + " has been rejected by Department Head.";
                last = "<br/>Regards,<br/>Logic Univeristy Store";
            }
            
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    approveRequistionDetailGV.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    MailMessage mm = new MailMessage("logicuniversitysstore@gmail.com", bl.getEmpEmail(requOrderID));
                    mm.Subject = subject;
                    mm.Body = body + sw.ToString() + last;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "logicuniversitysstore@gmail.com";
                    NetworkCred.Password = "team1sa43";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }
    }
}