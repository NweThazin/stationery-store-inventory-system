using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObject;
using BusinessObject.StoreClerk;

namespace ADProjectSA43_Team1.Employee
{
    public partial class CheckCollectionPoint : System.Web.UI.Page
    {
        DepartmentCollectionPointBL bl = new DepartmentCollectionPointBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["login"].Equals("") || Session["userLoggedIn"].Equals(""))
                    {
                        Response.Redirect("~/LoggedInPage.aspx");
                    }
                    else
                    {
                        //Get Logged in data from 
                        LoginBO loginBO = (LoginBO)Session["login"];
                        UserBO userBO = (UserBO)Session["userLoggedIn"];

                        collectionlistGV.DataSource = bl.getCollectionList();
                        collectionlistGV.DataBind();

                       // deptName.Text = bl.getDepartmentName(loginBO.DepID); //Get the department Name

                        int collectionPointID = (int)bl.getCollectionPoint(loginBO.DepID); //Get the collection point id and name by dept ID
                        collectionpoint.Text = bl.getName(collectionPointID);

                        //Call the method to change the method
                        changeCollectionPoint(collectionPointID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void LinkButtonOrder_Click(object sender, EventArgs e)
        {
            try
            {
                LoginBO loginBO = (LoginBO)Session["login"];

                LinkButton lb = (LinkButton)sender;
                HiddenField hf = (HiddenField)lb.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf.Value);
                bl.update(id, loginBO.DepID); //Update in department

                int collectionPointID = (int)bl.getCollectionPoint(loginBO.DepID);
                collectionpoint.Text = bl.getName(collectionPointID);

                changeCollectionPoint(collectionPointID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void changeCollectionPoint(int collectionPointID)
        {
            for (int i = 0; i < collectionlistGV.Rows.Count; i++)
            {
                HiddenField hdn = (HiddenField)collectionlistGV.Rows[i].FindControl("HiddenField1");
                LinkButton lnk = (LinkButton)collectionlistGV.Rows[i].FindControl("LinkButtonOrder");
                if (collectionPointID == Convert.ToInt32(hdn.Value))
                {
                    lnk.Text = "Current";
                    lnk.ForeColor = System.Drawing.Color.Red;
                    lnk.Enabled = false;
                }
                else
                {
                    lnk.Text = "Change";
                    lnk.ForeColor = System.Drawing.Color.Teal;
                    lnk.Enabled = true;
                }
            }
        } //end of the method
    }
}