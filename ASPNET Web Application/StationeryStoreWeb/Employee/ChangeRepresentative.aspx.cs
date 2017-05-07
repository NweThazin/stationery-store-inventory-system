using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using BusinessObject.StoreClerk;
using System.IO;
using System.Net.Mail;

namespace ADProjectSA43_Team1.Employee
{
    public partial class ChangeRepresentative : System.Web.UI.Page
    {
        EmployeeBL bl = new EmployeeBL();
        DelegateHeadBL delegatebl = new DelegateHeadBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Change representative a twat ka UserDA and EmployeeDA twae mhar yay htar tal
                try
                {
                    if (Session["login"].Equals("") || Session["userLoggedIn"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Get department Name by loggined user id (Get information after login)
                        LoginBO lbo = (LoginBO)Session["login"];
                        ViewState["depID"] = lbo.DepID;

                        UserBO ubo = (UserBO)Session["userLoggedIn"];//get the loggined user object
                        lblDepartmentName.Text = lbo.DeptName + " Department";

                        ViewState["newRep"] = 0;

                        checkPrimaryRole(lbo.DepID);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void changeRepGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                string status = "rep";
                LoginBO lbo = (LoginBO)Session["login"];
                changeRepGV.PageIndex = e.NewPageIndex;
                changeRepGV.DataSource = bl.getDepartmentEmployees(lbo.DepID, status);
                changeRepGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        protected void radbtnChoose_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //uncheck the old radio
                foreach (GridViewRow oldrow in changeRepGV.Rows)
                {
                    ((RadioButton)oldrow.FindControl("radbtnChoose")).Checked = false;
                }

                //Set the new selected row
                RadioButton rb = (RadioButton)sender;
                GridViewRow row = (GridViewRow)rb.NamingContainer;
                ((RadioButton)row.FindControl("radbtnChoose")).Checked = true;

                //Get the value from hidden field
                HiddenField hf = (HiddenField)rb.FindControl("hdnChoose");
                ViewState["newRep"] = Convert.ToInt32(hf.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                int newRep = int.Parse(ViewState["newRep"].ToString());
                bl.assignDeptRep(newRep);

                int oldRep = int.Parse(ViewState["oldRep"].ToString());
                bl.removeDeptRep(oldRep);

                checkPrimaryRole(int.Parse(ViewState["depID"].ToString()));

                EmployeeBO emp = delegatebl.getEmpByID(newRep);
                string fullName = emp.FirstName + " " + emp.LastName;
                sendMail(emp.Email, fullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void checkPrimaryRole(int deptID)
        {
            EmployeeBO emp = bl.getEmpRepNameByID(deptID); //No Need to chage
            if (emp != null)
            {
                lblCurrentRepresentative.Text = emp.FullName;
                ViewState["oldRep"] = emp.EmployeeID.ToString();

                string status = "rep";
                changeRepGV.DataSource = bl.getDepartmentEmployees(deptID, status);//Done
                changeRepGV.DataBind();
            }
        }

        protected void sendMail(string email, string fullName)
        {
            string subject = "Delegating Role";
            string body = "Dear " + fullName + "<br/>You has been assigned as Employee Rep";
            string last = "Regards,<br/>Dept Head";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringReader sr = new StringReader(sw.ToString());
                    MailMessage mm = new MailMessage("logicuniversitysstore@gmail.com", email);
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