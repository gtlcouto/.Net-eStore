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
    public partial class ViewCart : System.Web.UI.Page
    {
        private double amt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerID"] == null)
                Response.Redirect("Login2.aspx#login_popup");
            else
            {
                ButtonOrder.Visible = true;

                if (Session["Cart"] != null)
                    LoadItems();
                else
                {
                    LabelStatus.Text = "No Items to display!";
                    ButtonOrder.Visible = false;
                }
            }

        }
        //Creates a table displaying all the data from the cart.
        protected void LoadItems()
        {
            CartItem[] cart = (CartItem[])Session["Cart"];
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            int count = 0;
            System.Drawing.Color bgcolour;
            bool colorSwitch = false;
            amt = 0;
            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thd = new TableHeaderCell();
            thd.ColumnSpan = 5;
            thd.HorizontalAlign = HorizontalAlign.Right;
            thd.Text = DateTime.Now.ToShortDateString();
            th.Cells.Add(thd);
            tableItems.Rows.Add(th);
            th = new TableHeaderRow();
            thd = new TableHeaderCell();
            td.ColumnSpan = 5;
            td.HorizontalAlign = HorizontalAlign.Center;
            thd.Text = "";
            th.Cells.Add(thd);
            tableItems.Rows.Add(th);
            th = new TableHeaderRow();
            thd = new TableHeaderCell();
            thd.Text = "<img src=\"img/pi.png\" />";
            thd.HorizontalAlign = HorizontalAlign.Center;
            thd.ColumnSpan = 5;
            th.Cells.Add(thd);
            tableItems.Rows.Add(th);
            th = new TableHeaderRow();
            thd = new TableHeaderCell();
            thd.Text = "Product Code";
            th.Cells.Add(thd);
            thd = new TableHeaderCell();
            thd.Text = "Product Name";
            th.Cells.Add(thd);
            thd = new TableHeaderCell();
            thd.Text = "MSRP";
            th.Cells.Add(thd);
            thd = new TableHeaderCell();
            thd.Text = "QTY";
            th.Cells.Add(thd);
            thd = new TableHeaderCell();
            thd.Text = "Extended";
            th.Cells.Add(thd);
            tableItems.Rows.Add(th);

            foreach (CartItem item in cart)
            {
                if (item.qty > 0)
                {
                    if (colorSwitch == false)
                    {
                        bgcolour = bgcolour = System.Drawing.Color.White;
                        colorSwitch = true;
                    }
                    else
                    {
                        bgcolour = bgcolour = System.Drawing.Color.Tomato;
                        colorSwitch = false;
                    }
                    tr = new TableRow();
                    tr.BackColor = bgcolour;
                    //product code
                    tr.Cells.Add(GenerateCell(15, HorizontalAlign.Left, "Verdana", item.prodcd));
                    //product name
                    tr.Cells.Add(GenerateCell(43, HorizontalAlign.Left, "Verdana", item.prodname));
                    //product price
                    tr.Cells.Add(GenerateCell(17, HorizontalAlign.Right, "Verdana", String.Format("{0:C}", Convert.ToDecimal(item.msrp))));
                    //product qty
                    tr.Cells.Add(GenerateCell(15, HorizontalAlign.Center, "Verdana", Convert.ToString(item.qty)));
                    //product extended
                    tr.Cells.Add(GenerateCell(25, HorizontalAlign.Right, "Verdana", String.Format("{0:C}", Convert.ToDecimal(item.msrp * item.qty))));
                    tableItems.Rows.Add(tr);
                    count++;
                    amt += item.qty * Convert.ToDouble(item.msrp);
                }

            }

            TableFooterRow tf = new TableFooterRow();
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.ColumnSpan = 5;
            td.Text = "-----------";
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            tableItems.Rows.Add(tf);
            tf = new TableFooterRow();
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.ColumnSpan = 4;
            td.Text = "Total: ";
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.Text = String.Format("{0:C}", amt);
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            tableItems.Rows.Add(tf);
            tf = new TableFooterRow();
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.ColumnSpan = 4;
            td.Text = "Tax: ";
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.Text = String.Format("{0:C}", amt * 0.13);
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            tableItems.Rows.Add(tf);
            tf = new TableFooterRow();
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.ColumnSpan = 4;
            td.Text = "Order Tot: ";
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            td = new TableCell();
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.Text = String.Format("{0:C}", amt * 1.13);
            td.HorizontalAlign = HorizontalAlign.Right;
            tf.Cells.Add(td);
            tableItems.Rows.Add(tf);

            if ((amt * 1.13) > Convert.ToDouble(Application["MaximumOrderAmt"]))
            {
                LabelStatus.Text = "Maximum order ammount exceeded!";
                ButtonOrder.Visible = false;
            }
            else
                ButtonOrder.Visible = true;
        }
        //Routine for creating table cells.
        protected TableCell GenerateCell(int width, System.Web.UI.WebControls.HorizontalAlign align, string fontName, string text)
        {
            TableCell td = new TableCell();
            td.Width = new Unit(width, UnitType.Percentage);
            td.HorizontalAlign = align;
            td.Font.Name = fontName;
            td.Text = text;
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            return td;

        }
        //Button Order Event Handler - Creates an order in the db.
        protected void ButtonOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditCardInfo.aspx");
        }



    }
}