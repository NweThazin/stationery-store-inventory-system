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
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Globalization;

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class ReorderPurchaseReport : System.Web.UI.Page
    {
        ReorderReportBL bl = new ReorderReportBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Show the date 
                        DateTime dt = DateTime.Now;
                        lblDate.Text = dt.ToString("dd-MM-yyyy");

                        radbtnChoose.SelectedValue = "1";

                        //For Purchase List
                        purchaseReportGV.DataSource = bl.getReorderList(1);
                        purchaseReportGV.DataBind();

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

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

        protected void purchaseReportGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                purchaseReportGV.PageIndex = e.NewPageIndex;
                purchaseReportGV.DataSource = bl.getReorderList(1);
                purchaseReportGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string date = DateTime.Parse(lblDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string pdfName = "PurchaseReorder/" + date;

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + pdfName + ".pdf");

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                purchaseReportGV.AllowPaging = false;
                purchaseReportGV.DataSource = bl.getReorderList(1);
                purchaseReportGV.DataBind();
                purchaseReportGV.GridLines = GridLines.Both;
                HtmlForm frm = new HtmlForm();
                purchaseReportGV.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(lblReorderReportTitle);
                frm.Controls.Add(lblDate);
                frm.Controls.Add(purchaseReportGV);
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
                Console.WriteLine(ex.ToString());
            }
        }
    }
}