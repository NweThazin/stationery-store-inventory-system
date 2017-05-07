using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Data;
using System.Drawing;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class ReorderReport : System.Web.UI.Page
    {
        ReorderReportBL bl = new ReorderReportBL();
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
                        //Show the date 
                        DateTime dt = DateTime.Now;
                        lblDate.Text = dt.ToString("dd-MM-yyyy");
                        radbtnChoose.SelectedValue = "0";

                        //For Reorder List
                        reorderReportGV.DataSource = bl.getReorderList(0);
                        reorderReportGV.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //protected void btnExportToPDF_Click(object sender, EventArgs e)
        //{
        //    string date = DateTime.Parse(lblDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        //    string pdfName = "Reorder/" + date;

        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + pdfName + ".pdf");

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //    reorderReportGV.AllowPaging = false;
        //    reorderReportGV.DataSource = bl.getReorderList(int.Parse(radbtnChoose.SelectedValue));
        //    reorderReportGV.DataBind();
        //    reorderReportGV.GridLines = GridLines.Both;
        //    HtmlForm frm = new HtmlForm();
        //    reorderReportGV.Parent.Controls.Add(frm);
        //    frm.Attributes["runat"] = "server";
        //    frm.Controls.Add(lblReorderReportTitle);
        //    frm.Controls.Add(lblDate);
        //    frm.Controls.Add(reorderReportGV);
        //    frm.RenderControl(hw);

        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}

        protected void radbtnChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(radbtnChoose.SelectedValue) == 0)
                {
                    Response.Redirect("~/StoreClerk/ReorderReport.aspx");
                }
                else if (int.Parse(radbtnChoose.SelectedValue) == 1)
                {
                    Response.Redirect("~/StoreClerk/ReorderPurchaseReport.aspx");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void reorderReportGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //Pagination of GridView
                reorderReportGV.PageIndex = e.NewPageIndex;
                reorderReportGV.DataSource = bl.getReorderList(0);
                reorderReportGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //Need to change
        protected void reorderReportGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                string itemNumber = reorderReportGV.Rows[e.NewSelectedIndex].Cells[0].Text;
                Session["pReorder"] = "reorder";
                Response.Redirect("~/StoreClerk/PurchaseOrder.aspx?itemNumber=" + itemNumber);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}