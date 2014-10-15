using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eStoreDataObjects;
using System.Collections;

namespace eStoreWebObjects
{
    public class BranchWeb : eStoreConfigWeb
    {
        private int _branchnumber;

        public int BranchNumber
        {
            get { return _branchnumber; }
            set { _branchnumber = value; }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _region;

        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }

        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        
        public List<BranchWeb> GetThreeClosetBranches()
        {
            List<BranchWeb> webBranches = new List<BranchWeb>();
            try
            {
                BranchData data = new BranchData();
                Hashtable HashtableAddresses = new Hashtable();
                HashtableAddresses["lat"] = _latitude;
                HashtableAddresses["long"] = _longitude;
                List<GetThreeClosestBranches> dataBranches = data.GetAllDetailsForBranches(HashtableAddresses);
                //We return orderdetailweb instances as the asp layer should have no knowledge of EF objects
                foreach (GetThreeClosestBranches b in dataBranches)
                {
                    BranchWeb bw = new BranchWeb();
                    bw.BranchNumber = b.BranchNumber;
                    bw.City = b.City;
                    bw.Distance = Convert.ToDouble(b.DistanceFromAddress);
                    bw.Latitude = Convert.ToDouble(b.Latitude);
                    bw.Longitude = Convert.ToDouble(b.Longitude);
                    bw.Region = b.Region;
                    bw.Street = b.Street;
                    webBranches.Add(bw);
                }
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "BranchWeb", "GetThreeClosetBranches");
            }
            return webBranches;
        }
        
        
        
    }
}
