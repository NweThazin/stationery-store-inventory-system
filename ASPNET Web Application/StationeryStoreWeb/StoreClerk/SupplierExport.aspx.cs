using DataAccess;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class SupplierExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
            {
                Response.Redirect("~/LoggedInPage.aspx");
            }
            else
            {

                if (Upload.HasFile)
                {

                    //excel to gridview
                    string FileName = Path.GetFileName(Upload.PostedFile.FileName);
                    string Extension = Path.GetExtension(Upload.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    Upload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension/*, rbHDR.SelectedItem.Text*/);


                    //excel to sql
                    if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))
                    {
                        var excel = new ExcelPackage(Upload.FileContent);
                        var dt = excel.ToDataTable();
                        string fn = Upload.FileName;    //Test.xlsx
                        int index = fn.IndexOf(".xlsx");//return a number other than -1 if xlsx file      
                        if (index != -1)
                        {
                            string f = fn.Remove(index); //Test.xlsx Remove(.xlxs)
                            var table = f; //f= Test
                            using (var conn = new SqlConnection("Server=DESKTOP-M8T503R\\SQLSERVER;Database=ADTeam1;Integrated Security=SSPI"))
                            {
                                var bulkCopy = new SqlBulkCopy(conn);
                                bulkCopy.DestinationTableName = table;
                                conn.Open();
                                var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                                foreach (DataColumn sourceColumn in dt.Columns)
                                {
                                    foreach (DataRow row in schema.Rows)
                                    {
                                        if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                        {
                                            bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                            break;
                                        }
                                    }
                                }
                                bulkCopy.WriteToServer(dt);

                            }
                        }
                    }
                }
            }
        }
        private void Import_To_Grid(string FilePath, string Extension/*, string isHDR*/)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath/*, isHDR*/);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FileName = GridView1.Caption;
            string Extension = Path.GetExtension(FileName);
            string FilePath = Server.MapPath(FolderPath + FileName);

            Import_To_Grid(FilePath, Extension/*, rbHDR.SelectedItem.Text*/);
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}