using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CustomerID"] == null)
                {
                    Response.Redirect("login2.aspx#login_popup");
                }
                else
                {
                    OrderWeb myOrder = new OrderWeb();
                    myOrder.CustID = Convert.ToInt32(Session["CustomerID"]);
                    List<OrderWeb> Orders = myOrder.GetAllForCust();

                    TableRow tr = new TableRow();
                    TableCell td = new TableCell();
                    TableHeaderRow th = new TableHeaderRow();
                    TableHeaderCell thd = new TableHeaderCell();
                    thd.Text = "ID";
                    thd.CssClass = "tblItemHead";
                    thd.Width = new System.Web.UI.WebControls.Unit(20, UnitType.Percentage);
                    th.Cells.Add(thd);
                    thd = new TableHeaderCell();
                    thd.Text = "Date";
                    thd.CssClass = "tblItemHead";
                    thd.Width = new System.Web.UI.WebControls.Unit(80, UnitType.Percentage);
                    th.Cells.Add(thd);
                    TableMain.Rows.Add(th);

                    foreach (OrderWeb order in Orders)
                    {
                        tr = new TableRow();
                        tr.Cells.Add(GenerateCell(20, HorizontalAlign.Left, "Verdana", order.OrderID.ToString(),"id","OrderNo"));
                        tr.Cells.Add(GenerateCell(80, HorizontalAlign.Right, "Verdana", String.Format("{0:yyyy/MM/dd}", order.OrderDate)));
                        TableMain.Rows.Add(tr);
                    }

                    //tr.Cells.Add(td);
                    //TableMain.Rows.Add(tr);
                    //tr = new TableRow();
                    //td = new TableCell();
                    //td.Text = "<table id=\"tblDetails\"></table>";
                    //tr.Cells.Add(td);
                    //TableMain.Rows.Add(tr);

                    //add the data to the page in string variable sXML

                    myOrder.CustID = Convert.ToInt32(Session["CustomerID"]);
                    string strXML = myOrder.GetAllDetailsForAllOrders();
                    strXML = strXML.Replace("\r\n", "");
                    strXML = strXML.Replace(">    <", "><");
                    string script = @"<script type=""text/javascript"">var sXML = ";
                    script += "\"" + strXML + "\" </script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sXML", script);
                    TableMain.Height = new System.Web.UI.WebControls.Unit(15, UnitType.Percentage);
                    this.Title = "Past Orders";
                }
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Problem Gettin Order List Information, " + ex.Message;
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
        protected TableCell GenerateCell(int width, System.Web.UI.WebControls.HorizontalAlign align, string fontName, string text, string attribute, string attributeName)
        {
            TableCell td = new TableCell();
            td.Width = new Unit(width, UnitType.Percentage);
            td.HorizontalAlign = align;
            td.Font.Name = fontName;
            td.Text = text;
            td.Font.Size = new FontUnit(System.Web.UI.WebControls.FontSize.Smaller);
            td.Attributes.Add(attribute, attributeName);
            return td;

        }


    }
}