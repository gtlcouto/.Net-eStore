using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using eStoreDataObjects;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace eStoreWebObjects
{
    public class OrderWeb : eStoreConfigWeb
    {
        private int _orderid;

        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        private int _custid;

        public int CustID
        {
            get { return _custid; }
            set { _custid = value; }
        }

        private DateTime _orderdate;

        public DateTime OrderDate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }

        private DateTime _shipdate;

        public DateTime ShipDate
        {
            get { return _shipdate; }
            set { _shipdate = value; }
        }

        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// Add Order Method - adds order row, line item rows
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amt"></param>
        /// <param name="shipdate"></param>
        /// <returns>Hashtable containing order # and or messages</returns>
        public Hashtable AddOrder(CartItem[] items, int cid, double amt, DateTime shipDate)
        {
            Hashtable HashtableReturnValues = new Hashtable();
            Hashtable HashtableOrder = new Hashtable();

            try
            {
                OrderData myData = new OrderData();
                int idx = 0;
                string[] prodcds = new string[items.Length];
                int[] qty = new int[items.Length];
                Decimal[] sellPrice = new Decimal[items.Length];

                foreach (CartItem item in items)
                {
                    prodcds[idx] = item.prodcd;
                    sellPrice[idx] = item.msrp;
                    qty[idx++] = item.qty;
                }

                HashtableOrder["prodcd"] = prodcds;
                HashtableOrder["qty"] = qty;
                HashtableOrder["msrp"] = sellPrice;
                HashtableOrder["cid"] = cid;
                HashtableOrder["amt"] = amt;
                HashtableOrder["shipdate"] = shipDate;

                HashtableReturnValues = myData.AddOrder(HashtableOrder);

            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "Order", "AddOrder");
                HashtableReturnValues.Add("webmessage", ex.Message); 
            }
            return HashtableReturnValues;
        }

        /// <summary>
        /// Get order information for a single customer
        /// </summary>
        /// <returns>List of OrderWeb instances to presentation layer</returns>
        public List<OrderWeb> GetAllForCust()
        {
            List<OrderWeb> webOrders = new List<OrderWeb>();
            try
            {
                OrderData data = new OrderData();
                List<Order> dataOrders = data.GetAllForCust(_custid);

                //We return OrderWeb Instances as the Asp Layer has no knowledge of EF
                foreach (Order o in dataOrders)
                {
                    OrderWeb ordWeb = new OrderWeb();
                    ordWeb.OrderID = o.OrderID;
                    ordWeb.OrderDate = o.OrderDate;
                    ordWeb.ShipDate = o.ShipDate;
                    ordWeb.Amount = o.OrderAmount;
                    webOrders.Add(ordWeb);
                }
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderWeb", "GetAllForCust");
            }
            return webOrders;
        }
        /// <summary>
        /// Get Order details for a single order
        /// </summary>
        /// <returns>List of OrderDetailWeb instances to presentation layer</returns>
        public List<OrderDetailWeb> GetAllDetailsForOrder()
        {
            List<OrderDetailWeb> webDetails = new List<OrderDetailWeb>();
            try
            {
                OrderData data = new OrderData();
                Hashtable HashtableIDs = new Hashtable();
                HashtableIDs["oid"] = _orderid;
                HashtableIDs["cid"] = _custid;
                List<SingleOrderDetails> dataDetails = data.GetAllDetailsForOrder(HashtableIDs);
                //We return orderdetailweb instances as the asp layer should have no knowledge of EF objects
                foreach (SingleOrderDetails d in dataDetails)
                {
                    OrderDetailWeb od = new OrderDetailWeb();
                    od.Orderid = _orderid;
                    od.Msrp = d.SellingPrice;
                    od.Prodnam = d.prodnam;
                    od.Qtyo = d.qtyordered;
                    od.Qtyb = d.qtybackordered;
                    od.Qtys = d.qtysold;
                    od.OrderDate = d.OrderDate;
                    webDetails.Add(od);
                }
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderWeb", "GetAllDetailsForOrder");
            }
            return webDetails;
        }

        public string GetAllDetailsForAllOrders()
        {
            List<SingleOrderDetails> details;
            string sXML = "";
            XDocument document = new XDocument();

            try
            {
                OrderData myData = new OrderData();
                details = myData.GetAllDetailsForAllOrders(_custid);
                XmlSerializer formatter = new XmlSerializer(details.GetType());

                using (XmlWriter xmlWriter = document.CreateWriter())
                {
                    formatter.Serialize(xmlWriter, details);
                }
                document.Root.RemoveAttributes();
                sXML = document.Root.ToString();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "OrderWeb", "GetAllDetailsForAllOrders");
            }
            return sXML;
        }


    }
}
