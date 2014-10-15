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
    public class CustomerWeb : eStoreConfigWeb
    {
        //instance variable
        private int _customerid;

public int CustomerID
{
  get { return _customerid; }
  set { _customerid = value; }
}
        private string _username;

public string Username
{
  get { return _username; }
  set { _username = value; }
}
        private string _firstname;

public string Firstname
{
  get { return _firstname; }
  set { _firstname = value; }
}
        private string _lastname;

public string Lastname
{
  get { return _lastname; }
  set { _lastname = value; }
}
        private string _email;

public string Email
{
  get { return _email; }
  set { _email = value; }
}
        private int _age;

public int Age
{
  get { return _age; }
  set { _age = value; }
}
        private string _address;

public string Address
{
  get { return _address; }
  set { _address = value; }
}
        private string _city;

public string City
{
  get { return _city; }
  set { _city = value; }
}
        private string _zip;

public string Zip
{
  get { return _zip; }
  set { _zip = value; }
}
        private string _province;

public string Province
{
  get { return _province; }
  set { _province = value; }
}
        private string _country;

public string Country
{
  get { return _country; }
  set { _country = value; }
}
        private string _cctype;

public string Cctype
{
  get { return _cctype; }
  set { _cctype = value; }
}
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="bytCustomer">Takes a byteArray Containing customer information</param>
        /// <returns>Logins to your customer account by validating username and password</returns>
        public void Login(string username, string password)
        {
            try
            {
                CustomerData custData = new CustomerData();
                Hashtable HashtableCustomer = new Hashtable();
                HashtableCustomer["username"] = username.Trim();
                HashtableCustomer["password"] = password.Trim();
                _customerid = custData.Login(Serializer(HashtableCustomer));

            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "Customer", "Login");
            }
        }
        /// <summary>
        /// Register Method - Takes all information necessary and puts it into a hashtable.
        /// </summary>
        /// <param>Takes all required information to create a new customer</param>
        /// <returns>new customer id</returns>
        public void Register(string username, string password, string firstname, string lastname,
                               string email, int age, string address, string city, string zip, string province,
                               string country, string cctype)
        {
            Hashtable HashtableCustomer = new Hashtable();
            try
            {
                CustomerData custData = new CustomerData();
                HashtableCustomer["username"] = username.Trim();
                HashtableCustomer["password"] = password.Trim();
                HashtableCustomer["firstname"] = firstname.Trim();
                HashtableCustomer["lastname"] = lastname;
                HashtableCustomer["email"] = email.Trim();
                HashtableCustomer["age"] = age;
                HashtableCustomer["address"] = address;
                HashtableCustomer["city"] = city;
                HashtableCustomer["zip"] = zip;
                HashtableCustomer["province"] = province;
                HashtableCustomer["country"] = country;
                HashtableCustomer["cctype"] = cctype;
                _customerid = custData.RegisterData(Serializer(HashtableCustomer));
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "Customer", "Register");
            } 
        }

        public string GetAllDetailsForCustomer(int custid)
        {
            List<AllCustomerInfo> details;
            string sXML = "";
            XDocument document = new XDocument();

            try
            {
                CustomerData myData = new CustomerData();
                details = myData.GetAllCustomerInfo(custid);
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
                ErrorRoutine(ex, "CustomerWeb", "GetAllDetailsForCustomer");
            }
            return sXML;
        }

         public string GetEmailAddress(int custid)
        {
            List<AllCustomerInfo> details = new List<AllCustomerInfo>();
            string email = "-1";
            try
            {
                CustomerData myData = new CustomerData();
                details = myData.GetAllCustomerInfo(custid);
                foreach (AllCustomerInfo cust in details)
                {
                    email = cust.Email;
                }
                
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "CustomerWeb", "GetEmailAddress");
            }
            return email;
        }
    }
}
