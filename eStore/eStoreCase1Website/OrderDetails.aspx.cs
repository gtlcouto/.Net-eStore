using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerID"] == null)
                Response.Redirect("Login2.aspx#login_popup");
            else
            {
                double amt;
                OrderWeb myOrder = new OrderWeb();
                myOrder.OrderID = Convert.ToInt32(Request.QueryString["oid"]);
                myOrder.CustID = Convert.ToInt32(Session["CustomerID"]);
                List<OrderDetailWeb> details = myOrder.GetAllDetailsForOrder();

                if (details.Count == 0) //not their order
                    LabelStatus.Text = "Your Request is not allowed at this time!";
                else
                {
                    LabelStatus.Text = "";
                    TableRow tr = new TableRow();
                    TableCell td = new TableCell();
                    int count = 0;
                    System.Drawing.Color bgcolour;
                    bool colorSwitch = false;
                    amt = 0;
                    TableHeaderRow th = new TableHeaderRow();
                    TableHeaderCell thd = new TableHeaderCell();
                    thd.ColumnSpan = 6;
                    thd.HorizontalAlign = HorizontalAlign.Center;
                    thd.Text = "ORDER " + myOrder.OrderID + "--" + details[0].OrderDate.ToShortDateString();
                    th.Cells.Add(thd);
                    tableItems.Rows.Add(th);
                    th = new TableHeaderRow();
                    thd = new TableHeaderCell();
                    td.ColumnSpan = 6;
                    td.HorizontalAlign = HorizontalAlign.Center;
                    thd.Text = "";
                    th.Cells.Add(thd);
                    tableItems.Rows.Add(th);
                    th = new TableHeaderRow();
                    thd = new TableHeaderCell();
                    thd.Text = "<img src=\"img/pi.png\" />";
                    thd.HorizontalAlign = HorizontalAlign.Center;
                    thd.ColumnSpan = 6;
                    th.Cells.Add(thd);
                    tableItems.Rows.Add(th);
                    th = new TableHeaderRow();
                    thd = new TableHeaderCell();
                    thd.Text = "Product Name";
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "MSRP";
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "QTY Ord";
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "QTY Sold";
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "QTY Bk";
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "Extended";
                    th.Cells.Add(thd);
                    tableItems.Rows.Add(th);

                    foreach (OrderDetailWeb d in details)
                    {
                        if (details.Count() > 0)
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
                            //product name
                            tr.Cells.Add(GenerateCell(30, HorizontalAlign.Left, "Verdana", d.Prodnam));
                            //product msrp
                            tr.Cells.Add(GenerateCell(12, HorizontalAlign.Center, "Verdana", String.Format("{0:C}", Convert.ToDecimal(d.Msrp))));
                            //product qty ordered
                            tr.Cells.Add(GenerateCell(12, HorizontalAlign.Center, "Verdana", Convert.ToString(d.Qtyo)));
                            //product qty sold
                            tr.Cells.Add(GenerateCell(12, HorizontalAlign.Center, "Verdana", Convert.ToString(d.Qtys)));
                            //product qty backordered
                            tr.Cells.Add(GenerateCell(12, HorizontalAlign.Center, "Verdana", Convert.ToString(d.Qtyb)));
                            //product extended
                            tr.Cells.Add(GenerateCell(22, HorizontalAlign.Right, "Verdana", String.Format("{0:C}", Convert.ToDecimal(d.Msrp * d.Qtyo))));
                            tableItems.Rows.Add(tr);
                            count++;
                            amt += d.Qtyo * Convert.ToDouble(d.Msrp);
                        }

                    }
                    TableFooterRow tf = new TableFooterRow();
                    td = new TableCell();
                    td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
                    td.ColumnSpan = 6;
                    td.Text = "-----------";
                    td.HorizontalAlign = HorizontalAlign.Right;
                    tf.Cells.Add(td);
                    tableItems.Rows.Add(tf);
                    tf = new TableFooterRow();
                    td = new TableCell();
                    td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
                    td.ColumnSpan = 5;
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
                    td.ColumnSpan = 5;
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
                    td.ColumnSpan = 5;
                    td.Text = "Order Tot: ";
                    td.HorizontalAlign = HorizontalAlign.Right;
                    tf.Cells.Add(td);
                    td = new TableCell();
                    td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
                    td.Text = String.Format("{0:C}", amt * 1.13);
                    td.HorizontalAlign = HorizontalAlign.Right;
                    tf.Cells.Add(td);
                    tableItems.Rows.Add(tf);
                }
            }
        }

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
    }
}