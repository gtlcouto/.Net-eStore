using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerWeb cust = new CustomerWeb();
                cust.Login(TextBoxUsername.Text, TextBoxPassword.Text);

                if (cust.CustomerID > 0)
                {
                    Session.Add("CustomerID", cust.CustomerID);
                    LabelStatus.ForeColor = System.Drawing.Color.Green;
                    LabelStatus.Text = "An Multi Tier Welcome " + TextBoxUsername.Text;
                }
                else
                {
                    LabelStatus.ForeColor = System.Drawing.Color.Red;
                    LabelStatus.Text = "Username or password are invalid - try again";
                }
            }
            catch (Exception ex)
            {
                LabelStatus.ForeColor = System.Drawing.Color.Red;
                LabelStatus.Text = "Problem loggin in " + ex.Message;
            }
        }
    }
}