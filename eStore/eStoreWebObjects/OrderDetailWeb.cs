using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eStoreDataObjects;

namespace eStoreWebObjects
{
    /// <summary>
    /// Container Class to abstract complex type generated from EF
    /// </summary>
    public class OrderDetailWeb
    {
        private int _orderid;

        public int Orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        private string _prodnam;

        public string Prodnam
        {
            get { return _prodnam; }
            set { _prodnam = value; }
        }

        private decimal _msrp;

        public decimal Msrp
        {
            get { return _msrp; }
            set { _msrp = value; }
        }

        private int _qtyo;

        public int Qtyo
        {
            get { return _qtyo; }
            set { _qtyo = value; }
        }

        private int _qtys;

        public int Qtys
        {
            get { return _qtys; }
            set { _qtys = value; }
        }

        private int _qtyb;

        public int Qtyb
        {
            get { return _qtyb; }
            set { _qtyb = value; }
        }

        private DateTime  _orderdate;

        public DateTime  OrderDate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }
        
        
        
        
        
    }
}
