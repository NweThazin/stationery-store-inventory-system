using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Globalization;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class InventoryReport : System.Web.UI.Page
    {
        ReorderReportBL bl = new ReorderReportBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Show today date and show all inventory lists in gridview
                    DateTime dt = DateTime.Now;
                    String myString_new = dt.ToString("dd-MM-yyyy");
                    lblDate.Text = myString_new;

                    inventoryReportGV.DataSource = bl.getInventoryList();
                    inventoryReportGV.DataBind();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }

        protected void inventoryReportGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                inventoryReportGV.PageIndex = e.NewPageIndex;
                inventoryReportGV.DataSource = bl.getInventoryList();
                inventoryReportGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string date = DateTime.Parse(lblDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string pdfName = "InventoryStatus/" + date;

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + pdfName + ".pdf");

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                inventoryReportGV.AllowPaging = false;
                inventoryReportGV.DataSource = bl.getInventoryList();
                inventoryReportGV.DataBind();
                inventoryReportGV.GridLines = GridLines.Both;
                HtmlForm frm = new HtmlForm();
                inventoryReportGV.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(lblTitle);
                frm.Controls.Add(lblDate);
                frm.Controls.Add(inventoryReportGV);
                frm.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}