using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eStoreDataObjects;

namespace eStoreWebObjects
{
    public class ProductWeb : eStoreConfigWeb
    {

        //instance variable
        private string _ProdCode;
        private string _Prodnam;
        private string _Graphic;
        private decimal _Costprice;
        private decimal _Msrp;
        private int _Qob;
        private int _Qoh;
        private string _description;


        //properties
        public string ProdCode
        {
            get { return _ProdCode; }
            set { _ProdCode = value; }
        }
        public string Prodnam
        {
            get { return _Prodnam; }
            set { _Prodnam = value; }
        }
        public string Graphic
        {
            get { return _Graphic; }
            set { _Graphic = value; }
        }
        public decimal Costprice
        {
            get { return _Costprice; }
            set { _Costprice = value; }
        }
        public decimal Msrp
        {
            get { return _Msrp; }
            set { _Msrp = value; }
        }
        public int Qob
        {
            get { return _Qob; }
            set { _Qob = value; }
        }
        public int Qoh
        {
            get { return _Qoh; }
            set { _Qoh = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<ProductWeb> GetAll()
        {
            List<ProductWeb> webProducts = new List<ProductWeb>();
            try
            {
                ProductData data = new ProductData();
                List<Product> dataProducts = data.GetAll();

                //We return ProductWebt instances as the asp layer has no knowledge of EF
                foreach (Product prod in dataProducts)
                {
                    ProductWeb prodWeb = new ProductWeb();
                    prodWeb.ProdCode = prod.prodcd;
                    prodWeb.Prodnam = prod.prodnam;
                    prodWeb.Graphic = prod.graphic;
                    prodWeb.Costprice = prod.costprice;
                    prodWeb.Msrp = prod.msrp;
                    prodWeb.Qob = prod.qob;
                    prodWeb.Qoh = prod.qoh;
                    prodWeb.description = prod.description;
                    webProducts.Add(prodWeb); // add to the list
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProductWeb", "GetAll");
            }
            return webProducts;
        }
    }
}
