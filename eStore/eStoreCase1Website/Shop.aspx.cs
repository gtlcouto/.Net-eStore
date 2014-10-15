using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class Shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CustomerID"] == null)
                Response.Redirect("Login2.aspx#login_popup");
            else
            {
                if (Session["Cart"] == null)
                {
                    try
                    {
                        ProductWeb prod = new ProductWeb();
                        List<ProductWeb> Prods = prod.GetAll();
                        if (Prods.Count > 0)
                        {
                            CartItem[] myCart = new CartItem[Prods.Count];
                            int ctr = 0;

                            //build cartitem array from list contents
                            foreach (ProductWeb p in Prods)
                            {
                                CartItem item = new CartItem();
                                item.prodcd = p.ProdCode;
                                item.prodname = p.Prodnam;
                                item.graphic = p.Graphic;
                                item.msrp = p.Msrp;
                                item.qty = 0;
                                item.description = p.description;
                                myCart[ctr++] = item;
                            }
                            Session["Cart"] = myCart;
                            LoadDetails();
                        }
                    }
                    catch (Exception ex)
                    {
                        LabelStatus.Text = "Catalogue Problem - " + ex.Message;
                    }
                }
                else
                {
                    if (!Page.IsPostBack)
                        LoadDetails();
                    else
                        AddToCart();
                }
            }
        }
        //Load the catalogue by utilizing a Generic HTML Control to generate HTML code for each product in the cart(which is
        //loaded with database objects.
        protected void LoadDetails()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl dynLi = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
            CartItem[] cart;
            if (Session["Cart"] != null)
            {
                cart = (CartItem[])Session["Cart"];
                try
                {
                    foreach (CartItem item in cart)
                    {
                        dynLi.InnerHtml = "<div class=\"img\"><img alt=\"\" src=\"img/" + item.graphic + "\" id=\"Graphic" +
                            item.prodcd + "\" /></div>";
                        dynLi.InnerHtml += "<div class=\"info\"><h3 id=\"Name" + item.prodcd + "\">" +
                            item.prodname + "</h3>";
                        dynLi.InnerHtml += "<p id=\"Descr" + item.prodcd + "\" data-description=\"" + item.description +
                            "\">" + item.description.Substring(0, 20) + "...</p>";
                        dynLi.InnerHtml += "<div class=\"price\"><span class=\"st\">Our Price:</spank>";
                        dynLi.InnerHtml += "<strong id=\"Price" + item.prodcd + "\">" +
                            String.Format("{0:C}", Convert.ToDecimal(item.msrp));
                        dynLi.InnerHtml += "</strong></div><div class=\"actions\">";
                        dynLi.InnerHtml += "<a href=\"#details_popup\" onclick=\"copyProdInfoToPopup('" + item.prodcd +
                            "');\">Details</a></div></div>";
                        plcLi.Controls.Add(dynLi);
                        dynLi = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                    }

                }
                catch (Exception ex)
                {
                    dynLi.InnerHtml += "Problem getting catalogue info " + ex.Message;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                    LoadDetails();
                else
                    AddToCart();
            }
        }
        //adds items with que quantity field over 0 to the cart
        protected void AddToCart()
        {
            CartItem[] Cart;

            Cart = (CartItem[])Session["Cart"];

            foreach (CartItem item in Cart)
            {
                if (item.prodcd == detailsProdcd.Value)
                {
                    if (Convert.ToInt16(qty.Text) >= 0)
                        item.qty = Convert.ToInt16(qty.Text);
                    break;

                }
            }
            Session["Cart"] = Cart;
            LabelStatus.Text = qty.Text + " - item(s) Added!";
            LabelStatus.ForeColor = System.Drawing.Color.Green;
            LoadDetails();
        }
        //Event Handler for AddtoCart CLick
        protected void ButtonAddtoCart_Click(object sender, EventArgs e)
        {
            AddToCart();
        }
        //Event Handler for ViewCart Click
        protected void ButtonViewCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewCart.aspx");
        }

    }
}