using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;
using System.Net.Mail;

namespace eStoreCase1Website
{
    public partial class CreditCardInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonOrder_Click(object sender, EventArgs e)
        {
            CreditCardVerify objCChk = new CreditCardVerify();
            String strRetVal = "";

            OrderWeb myOrder = new OrderWeb();
            Hashtable hshRet = new Hashtable();
            LabelStatus.Text = "";

            try
            {
                strRetVal = objCChk.VerifyCard(TextBoxCardNumber.Text, DropDownListCard.SelectedItem.Value);

                if (strRetVal == "OK")
                {
                    CartItem[] cart = (CartItem[])Session["Cart"];
                    int orderID = -1;
                    int boFlag = 0;
                    double amt = 0;
                    CustomerWeb myCustomer = new CustomerWeb();

                    try
                    {
                        hshRet = myOrder.AddOrder((CartItem[])Session["Cart"],
                                                            Convert.ToInt32(Session["CustomerID"]),
                                                            amt, DateTime.Now);
                        orderID = Convert.ToInt32(hshRet["orderid"]);
                        boFlag = Convert.ToInt32(hshRet["boflag"]);
                        string msg = Convert.ToString(hshRet["message"]);
                        string email = myCustomer.GetEmailAddress(Convert.ToInt32(Session["CustomerID"]));

                        if (orderID > 0)//Order Added
                        {

                            MailMessage msgorder = new MailMessage();
                            msgorder.Subject = "New Order";
                            msgorder.From = new MailAddress("orders@tomatosource.com", "Tomato Source Orders");
                            msgorder.To.Add(new MailAddress(email));
                            msgorder.Body = "Congratulations, you have just made a purchase at Tomato Source!\n";
                            msgorder.Body += "Items Purchased:\n ";
                            foreach (CartItem item in cart)
                            {
                                if (item.qty > 0)
                                {
                                    msgorder.Body += "Item: " + item.prodname + " x " + item.qty + " = " + String.Format("{0:C}", Convert.ToDecimal(item.msrp * item.qty)) + "\n";
                                    amt += item.qty * Convert.ToDouble(item.msrp);
                                }

                            }
                            msgorder.Body += "Your total is : " + String.Format("{0:C}", Convert.ToDecimal(amt * 1.13)) + " after taxes";
                            SmtpClient mailClient = new SmtpClient();
                            mailClient.Send(msgorder);
                            LabelStatus.Text = "Order " + orderID + " created! An email has been sent";
                            if (boFlag > 0)
                                LabelStatus.Text = LabelStatus.Text + " Some good were backordered!";
                            Session["Cart"] = null;
                        }
                        else // problem
                            LabelStatus.Text = msg + ", try again later!";
                        ButtonOrder.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        LabelStatus.Text = "Order was not created, try again later! - " + ex.Message;
                        ButtonOrder.Visible = false;
                    }
                }
                else
                {
                    LabelStatus.Text = "Problem - " + strRetVal;
                    ButtonOrder.Visible = false;
                }


            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Order was not created, try again later! - " + ex.Message;
                ButtonOrder.Visible = false;
            }

        }

    }
}