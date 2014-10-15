using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eStoreDataObjects;

namespace eStoreWebObjects
{
    public class CartItem : eStoreConfigWeb
    {
        //instanced variables
        private int _qty;
        private string _prodcd;
        private string _prodname;
        private string _graphic;
        private decimal _msrp;
        private string _description;
        //Properties
        public string prodname
        {
            get { return _prodname; }
            set { _prodname = value; }
        }
        public string graphic
        {
            get { return _graphic; }
            set { _graphic = value; }
        }
        public int qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        public string prodcd
        {
            get { return _prodcd; }
            set { _prodcd = value; }
        }
        public decimal msrp
        {
            get { return _msrp; }
            set { _msrp = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
