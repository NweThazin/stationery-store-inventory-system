using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using BusinessObject.StoreClerk;
using System.Globalization;
using System.IO;
using System.Net.Mail;

namespace ADProjectSA43_Team1.Employee
{
    public partial class DelegateHead : System.Web.UI.Page
    {
        DelegateHeadBL delegateBL = new DelegateHeadBL();
        EmployeeBL empBL = new EmployeeBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                compareValidatorStartToday.ValueToCompare = DateTime.Now.ToShortDateString();
                compareValidatorEndToday.ValueToCompare = DateTime.Now.ToShortDateString();

                if (!IsPostBack)
                {
                    if (Session["login"].Equals("") || Session["userLoggedIn"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Use Employee DA and User DA
                        LoginBO loginBO = (LoginBO)Session["login"];
                        ViewState["depID"] = loginBO.DepID;

                        UserBO userBO = (UserBO)Session["userLoggedIn"];
                        ViewState["newHead"] = "";

                        checkPrimaryOrDelegate(loginBO.DepID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Check Date
        protected void checkDate(EmployeeBO empBO)
        {
            if (empBO.PrimaryRole.Equals("Dept Head"))
            {
                lblStartDateLabel.Visible = false;
                lblStartDate.Visible = false;
                lblEndDateLabel.Visible = false;
                lblEndDate.Visible = false;
            }
            else
            {
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                startDate = DateTime.Parse(empBO.StartDate.ToString());
                lblStartDate.Text = startDate.ToString("dd-MM-yyyy");
                endDate = DateTime.Parse(empBO.EndDate.ToString());
                lblEndDate.Text = endDate.ToString("dd-MM-yyyy");
                lblStartDateLabel.Visible = true;
                lblStartDate.Visible = true;
                lblEndDateLabel.Visible = true;
                lblEndDate.Visible = true;
            }

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);

                if (DateTimeValidate(startDate, endDate))
                {
                    int empID = int.Parse(ViewState["newHead"].ToString());
                    delegateBL.assignDeptDelegateHead(empID, startDate, endDate);

                    int oldEmpID = int.Parse(ViewState["oldHead"].ToString());
                    delegateBL.removeDeptDelegateHead(oldEmpID);

                    checkPrimaryOrDelegate(int.Parse(ViewState["depID"].ToString()));

                    //Get the Employee By EmpID
                    EmployeeBO emp = delegateBL.getEmpByID(empID);
                    string fullName = emp.FirstName + " " + emp.LastName;
                    sendMail(emp.Email, fullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        protected void sendMail(string email,string fullName)
        {
            string subject = "Delegating Role";
            string body = "Dear "+fullName+"<br/>You has been assigned as Employee Head";
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
        private void checkPrimaryOrDelegate(int depID)
        {
            EmployeeBO empBO = delegateBL.getDepartmentDelegateHead(depID);
            lblDeptHead.Text = empBO.FirstName + " " + empBO.LastName;
            checkDate(empBO);//Check Date

            ViewState["oldHead"] = empBO.EmployeeID;

            string status = "head";
            delegateHeadGV.DataSource = empBL.getDepartmentEmployees(depID, status);
            delegateHeadGV.DataBind();

            if (empBO.PrimaryRole.Equals("Dept Head"))
            {
                btnRemoveHead.Visible = false;
            }
            else
            {
                btnRemoveHead.Visible = true;
            }

            //Check One Radio
            for (int i = 0; i < delegateHeadGV.Rows.Count; i++)
            {
                if (i == 0)
                {
                    RadioButton btn = delegateHeadGV.Rows[i].FindControl("radbtnChoose") as RadioButton;
                    btn.Checked = true;
                }

            }
        }

        protected Boolean DateTimeValidate(DateTime startDate, DateTime endDate)
        {
            if (startDate >= DateTime.Today)//Check with today date first
            {
                if (endDate >= startDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        protected void radbtnChoose_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow oldrow in delegateHeadGV.Rows)
                {
                    ((RadioButton)oldrow.FindControl("radbtnChoose")).Checked = false;
                }
                //Set the new selected row
                RadioButton rb = (RadioButton)sender;
                GridViewRow row = (GridViewRow)rb.NamingContainer;
                ((RadioButton)row.FindControl("radbtnChoose")).Checked = true;
                HiddenField hf = (HiddenField)rb.FindControl("hdnChoose");
                ViewState["newHead"] = Convert.ToInt32(hf.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnRemoveHead_Click(object sender, EventArgs e)
        {
            try
            {
                int empID = int.Parse(ViewState["oldHead"].ToString());
                delegateBL.removeDeptDelegateHead(empID);
                checkPrimaryOrDelegate(int.Parse(ViewState["depID"].ToString())); //change and show button
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void delegateHeadGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                string status = "head";
                delegateHeadGV.PageIndex = e.NewPageIndex;
                delegateHeadGV.DataSource = empBL.getDepartmentEmployees(int.Parse(ViewState["depID"].ToString()), status);
                delegateHeadGV.DataBind();

            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}