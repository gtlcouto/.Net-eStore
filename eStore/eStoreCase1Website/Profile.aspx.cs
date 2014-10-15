using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CustomerID"] == null)
            {
                Response.Redirect("login2.aspx#login_popup");
            }
            else
            {
                CustomerWeb myCustomer = new CustomerWeb();
                myCustomer.CustomerID = Convert.ToInt32(Session["CustomerID"]);
                string strXML = myCustomer.GetAllDetailsForCustomer(Convert.ToInt32(Session["CustomerID"]));
                strXML = strXML.Replace("\r\n", "");
                strXML = strXML.Replace(">    <", "><");
                string script = @"<script type=""text/javascript"">var sXML = ";
                script += "\"" + strXML + "\" </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sXML", script);
                this.Title = "Customer Profile";
            }

        }
    }
}