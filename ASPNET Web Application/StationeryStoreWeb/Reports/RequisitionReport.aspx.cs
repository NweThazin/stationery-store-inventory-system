using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace ADProjectSA43_Team1.Reports
{
    public partial class RequisitionReport : System.Web.UI.Page
    {
        SqlConnection conn;
        static RequisitionCrystalReport vcr;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void chklDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vcr != null)
            {
                vcr.Dispose();
            }
            vcr = new RequisitionCrystalReport();
            conn = new SqlConnection("Data Source=DESKTOP-M8T503R\\SQLSERVER;Initial Catalog=ADTeam1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            conn.Open();
            string query = "SELECT [DepartmentName],[CategoryName],[Description],[OrderDate],[RequestedQty] FROM [ADTeam1].[dbo].[View_Requisition] where 1=2";
            foreach (ListItem item in chklDepartment.Items)
            {
                if (item.Selected)
                {
                    query = query + " or DepartmentName=" + "'" + item.Value + "'";
                }
            }
            query = query + " and OrderDate >= DATEADD(MONTH,-3,GETDATE())";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            ReportDataSet rds = new ReportDataSet();
            DataTable dataTable = rds.View_Requisition;
            rds.EnforceConstraints = false;
            da.Fill(dataTable);
            conn.Close();
            //RequisitionCrystalReport cr = new RequisitionCrystalReport();
            vcr.SetDataSource(rds);
            CrystalReportViewer1.ReportSource = vcr;
            //ViewState["cr"] = cr;       
        }
    }
}