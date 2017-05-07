using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace ADProjectSA43_Team1.Reports
{
    public partial class PurchaseReport : System.Web.UI.Page
    {
        SqlConnection conn;
        static PurchaseCrystalReport vcr;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ckblPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vcr != null)
            {
                vcr.Dispose();
            }
            vcr = new PurchaseCrystalReport();
            conn = new SqlConnection("Data Source=DESKTOP-M8T503R\\SQLSERVER;Initial Catalog=ADTeam1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            conn.Open();
            string query = "SELECT [CategoryName],[Description],[PurchaseDate],[QtyOrdered] FROM[ADTeam1].[dbo].[View_Purchase] where 1=2";
            foreach (ListItem item in chklPurchase.Items)
            {
                if (item.Selected)
                {
                    query = query + " or CategoryName=" + "'" + item.Value + "'";
                }
            }
            query = query + " and PurchaseDate >= DATEADD(MONTH,-3,GETDATE())";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ReportDataSet rds = new ReportDataSet();
            rds.EnforceConstraints = false;
            da.Fill(rds.View_Purchase);
            conn.Close();
            //PurchaseCrystalReport cr = new PurchaseCrystalReport();
            vcr.SetDataSource(rds);
            CrystalReportViewer1.ReportSource = vcr;
        }
    }
}