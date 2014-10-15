using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;
using System.Net.Mail;

namespace eStoreCase1Website
{
    public partial class Resgister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Wizard1.PreRender += new EventHandler(Wizard1_PreRender);

            if (!Page.IsPostBack)
            {
                Wizard1.ActiveStepIndex = 0;
            }

        }

        protected void Wizard1_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = Wizard1.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = Wizard1.WizardSteps;
            SideBarList.DataBind();
        }

        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = Wizard1.WizardSteps.IndexOf(step);

            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

            try
            {
                CustomerWeb cust = new CustomerWeb();
                cust.Register(TextBoxUser.Text, TextBoxPassword.Text, TextBoxFirst.Text, TextBoxLast.Text,
                              TextBoxEmail.Text, Convert.ToInt32(TextBoxAge.Text), TextBoxAddress.Text, TextBoxCity.Text,
                              TextBoxPostal.Text, TextBoxRegion.Text, DropDownListCountry.SelectedItem.ToString(),
                              DropDownListCredit.SelectedItem.ToString());

                if (cust.CustomerID > 0)
                {
                    Session.Add("CustomerID", cust.CustomerID);
                    MailMessage msg = new MailMessage();
                    msg.Subject = "New Registration";
                    msg.From = new MailAddress("registrar@tomatosource.com", "Tomato Source Registrar");
                    msg.To.Add(new MailAddress(TextBoxEmail.Text));
                    msg.Body = "Congratulations " + TextBoxFirst.Text +
                                ", you have been registered at Tomato Source!" +
                                " your new username is " + TextBoxUser.Text +
                                " with a password of " + TextBoxPassword.Text;
                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Send(msg);
                    LabelStatus.ForeColor = System.Drawing.Color.Green;
                    LabelStatus.Text = "Customer " + cust.CustomerID + " created! Confirmation Sent via Email";
                }
                else
                {
                    LabelStatus.ForeColor = System.Drawing.Color.Red;
                    LabelStatus.Text = "Failed to register user, please try again";
                }
            }
            catch (Exception ex)
            {
                LabelStatus.ForeColor = System.Drawing.Color.Red;
                LabelStatus.Text = "Problem registering in " + ex.Message;
            }

        }

    }
}