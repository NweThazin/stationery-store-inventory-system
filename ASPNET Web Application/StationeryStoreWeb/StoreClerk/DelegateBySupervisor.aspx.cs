using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject.StoreClerk;
using BusinessObject;
using System.IO;
using System.Net.Mail;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class ChangeSupervisor : System.Web.UI.Page
    {
        StoreDelegateRoleBL bl = new StoreDelegateRoleBL();
        DelegateHeadBL delegateBl = new DelegateHeadBL();
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
                        //Get department Name by loggined user id
                        LoginBO lbo = (LoginBO)Session["login"];
                        ViewState["depID"] = lbo.DepID;

                        //get the loggined user object
                        UserBO ubo = (UserBO)Session["userLoggedIn"];
                        lblCurrentRepresentative.Text = lbo.DeptName;

                        ViewState["newSup"] = 0;

                        checkPrimaryRole(lbo.DepID);
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void checkPrimaryRole(int deptID)
        {
            //Get Rep Name
            EmployeeBO emp = bl.getDepartmentSupervisor(deptID);
            lblCurrentRepresentative.Text = emp.FullName;

            checkDate(emp);

            ViewState["oldSup"] = emp.EmployeeID.ToString();

            changeSupervisorGV.DataSource = bl.getDepartmentStoreEmployeeForSupervisor(deptID);
            changeSupervisorGV.DataBind();

            if (emp.PrimaryRole.Equals("Store Supervisor"))
            {
                btnRemoveManager.Visible = false;
            }
            else
            {
                btnRemoveManager.Visible = true;
            }

            //Check One Radio
            for (int i = 0; i < changeSupervisorGV.Rows.Count; i++)
            {
                if (i == 0)
                {
                    RadioButton btn = changeSupervisorGV.Rows[i].FindControl("radbtnChoose") as RadioButton;
                    btn.Checked = true;
                }

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
                    int empID = int.Parse(ViewState["newSup"].ToString());
                    bl.assignSupervisor(empID, startDate, endDate);

                    int oldEmpID = int.Parse(ViewState["oldSup"].ToString());
                    bl.removeDeptRep(oldEmpID);

                    checkPrimaryRole(int.Parse(ViewState["depID"].ToString()));

                    //Get the Employee By EmpID
                    EmployeeBO emp = delegateBl.getEmpByID(empID);
                    string fullName = emp.FirstName + " " + emp.LastName;
                    sendMail(emp.Email, fullName);
                }
                else
                {
                   // lblshow.Text = "Start Date shouldn't be earlier than Today Date";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        protected void sendMail(string email, string fullName)
        {
            string subject = "Delegating Role for Manager";
            string body = "Dear " + fullName + "<br/>You has been assigned as Store Manager";
            string last = "Regards,<br/>Store Manager";
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
        protected void radbtnChoose_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow oldrow in changeSupervisorGV.Rows)
                {
                    ((RadioButton)oldrow.FindControl("radbtnChoose")).Checked = false;
                }
                //Set the new selected row
                RadioButton rb = (RadioButton)sender;
                GridViewRow row = (GridViewRow)rb.NamingContainer;
                ((RadioButton)row.FindControl("radbtnChoose")).Checked = true;
                HiddenField hf = (HiddenField)rb.FindControl("hdnChoose");
                ViewState["newSup"] = Convert.ToInt32(hf.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        protected void checkDate(EmployeeBO empBO)
        {
            if (empBO.PrimaryRole.Equals("Store Supervisor"))
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

        protected void btnRemoveManager_Click(object sender, EventArgs e)
        {
            try
            {
                int empID = int.Parse(ViewState["oldSup"].ToString());
                bl.removeDeptRep(empID);
                checkPrimaryRole(int.Parse(ViewState["depID"].ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void changeSupervisorGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                changeSupervisorGV.PageIndex = e.NewPageIndex;
                changeSupervisorGV.DataSource = bl.getDepartmentStoreEmployeeForSupervisor(int.Parse(ViewState["depID"].ToString()));
                changeSupervisorGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
    
        }
    }
}