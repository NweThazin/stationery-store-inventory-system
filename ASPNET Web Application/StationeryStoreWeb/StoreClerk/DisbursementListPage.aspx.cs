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

namespace ADProjectSA43_Team1.StoreClerk
{
    public partial class DisbursementListPage : System.Web.UI.Page
    {
        int departmentID;
        DestinationBL destinationBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["userLoggedIn"].Equals("") || Session["login"].Equals(""))
                {
                    Response.Redirect("~/LoggedInPage.aspx");
                }
                else
                {
                    collectionPoint.Text = Session["collectionPoint"].ToString();
                    destinationBL = new DestinationBL();

                    departmentID = Int32.Parse(Request.QueryString["departmentID"]);
                    string departmentName = destinationBL.getDepartmentName(departmentID);

                    DepartmentName.Text = departmentName;

                    Session["items"] = destinationBL.getDisbursementItems(departmentID);
                    disbursementGV.DataSource = Session["items"];
                    disbursementGV.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int collectionPointID = Int32.Parse(Request.QueryString["collectionPointID"]);
                Response.Redirect("DestinationPage.aspx?collectionPointID=" + collectionPointID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Today;
                string pdfName = "disbursement_/" + date.ToString("dd/MM/yyyy");

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + pdfName + ".pdf");

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                disbursementGV.AllowPaging = false;
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();
                disbursementGV.GridLines = GridLines.Both;
                HtmlForm frm = new HtmlForm();

                disbursementGV.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(collectionPoint);
                frm.Controls.Add(disbursementGV);
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

        protected void disbursementGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                disbursementGV.PageIndex = e.NewPageIndex;
                disbursementGV.DataSource = Session["items"];
                disbursementGV.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}