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
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class AdjustmentVouchersDetail : System.Web.UI.Page
    {
        AdjsutmentVouchersBL bl = new AdjsutmentVouchersBL();
        AdjustmentBO b = new AdjustmentBO();
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
                        string number = Request.QueryString["adjNumber"];
                        int adjNum = Convert.ToInt32(number.Substring(1, number.Length-1));
                        b = bl.getAdjustment(adjNum); //search Adjustment By ID

                        lblAdjustmentCode.Text = number; //show number and date 
                        DateTime dt = DateTime.Parse(b.Date.ToString());
                        lblDateIssue.Text = dt.ToString("dd/MM/yyyy");

                        //Show all data in gridview
                        adjustDetailGV.DataSource = bl.getAdjustmentDetail(adjNum);
                        adjustDetailGV.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string number = Request.QueryString["adjNumber"];
                int adjNum = Convert.ToInt32(number.Substring(1, number.Length - 1));

                if (bl.CheckPending(adjNum) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have already approved')", true);
                }
                else if (bl.CheckPending(adjNum) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have already rejected')", true);
                }
                else if (bl.CheckPending(adjNum) == 2)
                {
                    int num = bl.ApprovedOrRejectedBL(1, adjNum); //1 mean approve

                    //To Update the quantity in stock
                    List<AdjustmentVoucherDetailBO> lst = new List<AdjustmentVoucherDetailBO>();
                    foreach (AdjustmentVoucherDetailBO b in bl.getAdjustmentDetail(adjNum))
                    {
                        AdjustmentVoucherDetailBO obj = new AdjustmentVoucherDetailBO();
                        obj.ItemNumber = b.ItemNumber;
                        obj.QuantityAdjusted = b.QuantityAdjusted;
                        lst.Add(obj);
                    }
                    int i = bl.checkInStockAdjustment(lst);

                    sendGmail("Approved", adjNum);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved Successful')", true);
                    Response.Redirect("~/StoreClerk/AdjustmentVouchersList.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                string number = Request.QueryString["adjNumber"];
                int adjNum = Convert.ToInt32(number.Substring(1, number.Length-1));
                if (bl.CheckPending(adjNum) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have already approved')", true);
                }
                else if (bl.CheckPending(adjNum) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have already rejected')", true);
                }
                else if (bl.CheckPending(adjNum) == 2)
                {
                    int num = bl.ApprovedOrRejectedBL(0, adjNum);
                    sendGmail("Rejected", adjNum);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Reject Sucessful')", true);
                    Response.Redirect("~/StoreClerk/AdjustmentVouchersList.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Send Email Part
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void sendGmail(string status, int adjOrderID)
        {
            string subject = ""; string body = ""; string last = "";
            EmployeeBO e = bl.getEmpInfoByAdjsutmentID(adjOrderID);

            //Creating the mail format
            if (status.Equals("Approved"))
            {
                subject = status + " Adjustment A" + adjOrderID;
                body = "Dear " + e.FullName + ",<br/>Your Adjustment Number A" + adjOrderID + " has been approved";
                last = "Regards,<br/>Logic Univeristy Store";
            }
            else if (status.Equals("Rejected"))
            {
                subject = status + " Adjustment A" + adjOrderID;
                body = "Dear " + e.FullName + ",<br/>Your Adjustment Number A" + adjOrderID + " has been rejected";
                last = "<br/>Regards,<br/>Logic Univeristy Store";
            }

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    adjustDetailGV.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    MailMessage mm = new MailMessage("logicuniversitysstore@gmail.com",e.Email);
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