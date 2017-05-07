using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;

namespace ADProjectSA43_Team1.Employee
{
    public partial class CurrentRequisitionDetailedPage : System.Web.UI.Page
    {
        RequisitionBO rbo = new RequisitionBO();
        RequisitionBL bl = new RequisitionBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string i = Request.QueryString["rID"];
                    rbo = bl.getRequistionByID(int.Parse(i));
                    lblItemCode.Text = rbo.ReqID;

                    DateTime dt = DateTime.Parse(rbo.OrderDate.ToString());
                    lblIssueDate.Text = dt.ToString("dd/MM/yyyy");

                    lblStatus.Text = rbo.Status;

                    string reqNumberStr = lblItemCode.Text;
                    int reqNum = Convert.ToInt32(reqNumberStr.Substring(1, reqNumberStr.Length - 1));

                    RequisitionDetailGV.DataSource = bl.getRequisitionDetailList(reqNum);
                    RequisitionDetailGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/Requisition.aspx");
        }
    }
}