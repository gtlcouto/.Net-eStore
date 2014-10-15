using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStoreDataObjects
{
    public class CustomerData : eStoreConfigData
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="bytCustomer">Takes a byteArray Containing customer information</param>
        /// <returns>Logins to your customer account by validating username and password</returns>
        public int Login(byte[] bytCustomer)
        {
          
            eStoreDBEntities dbContext;
            int retCustID = -1;
            try
            {
                Hashtable HashtableCustomer = (Hashtable)Deserializer(bytCustomer);
                String username = Convert.ToString(HashtableCustomer["username"]);
                String password = Convert.ToString(HashtableCustomer["password"]);

                dbContext = new eStoreDBEntities();
                //use Linq to Entities syntax to retrive desired customer
                Customer cust = dbContext.Customers.FirstOrDefault(c => c.UserName == username);
                if (cust.Password == password)
                    retCustID = cust.CustomerID;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "CustomerData", "Login");
                retCustID = -1;
            }
            return retCustID;
        }
        /// <summary>
        /// Register Method - Registers a Customer to the DB
        /// </summary>
        /// <param name="bytCustomer">Takes a byteArray Containing customer information</param>
        /// <returns>Registers a new customer to the database</returns>
        public int RegisterData(byte[] bytCustomer)
        {
            eStoreDBEntities dbContext;
            int retCustID = -1;

            try
            {
                dbContext = new eStoreDBEntities();
                Customer cust = new Customer();
                Hashtable HashtableCustomer = (Hashtable)Deserializer(bytCustomer);
                cust.UserName = Convert.ToString(HashtableCustomer["username"]);
                cust.Password = Convert.ToString(HashtableCustomer["password"]);
                cust.FirstName = Convert.ToString(HashtableCustomer["firstname"]);
                cust.LastName = Convert.ToString(HashtableCustomer["lastname"]);
                cust.Email = Convert.ToString(HashtableCustomer["email"]);
                cust.Age = Convert.ToInt32(HashtableCustomer["age"]);
                cust.Address1 = Convert.ToString(HashtableCustomer["address"]);
                cust.City = Convert.ToString(HashtableCustomer["city"]);
                cust.Mailcode = Convert.ToString(HashtableCustomer["zip"]);
                cust.Region = Convert.ToString(HashtableCustomer["province"]);
                cust.Country = Convert.ToString(HashtableCustomer["country"]);
                cust.Creditcardtype = Convert.ToString(HashtableCustomer["cctype"]);
                dbContext.Customers.AddObject(cust);
                dbContext.SaveChanges();
                retCustID = cust.CustomerID;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "CustomerData", "Register");
                retCustID = -1;
            }

            return retCustID;
        }


        public List<AllCustomerInfo> GetAllCustomerInfo(int custid)
        {
            eStoreDBEntities dbContext;
            List<AllCustomerInfo> customerInfo = new List<AllCustomerInfo >();

            try
            {
                dbContext = new eStoreDBEntities();
                System.Nullable<int> cid = custid;
                customerInfo = dbContext.pAllCustomerInfo(cid).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "CustomerData", "GetAllCustomerInfo");
            }
            return customerInfo;
        }


    }
}
