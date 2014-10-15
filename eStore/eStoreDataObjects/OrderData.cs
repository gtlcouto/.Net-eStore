using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Transactions;

namespace eStoreDataObjects
{
    /// <summary>
    /// Add Order Method - adds order row, line item rows via transaction
    /// </summary>
    /// <param name="HashtableOrder"></param>
    /// <returns>populated hashtable with new order #, bo flag or error</returns>
    public class OrderData : eStoreConfigData
    {
        public Hashtable AddOrder(Hashtable HashtableOrder)
        {
            Hashtable HashtableReturnValues = new Hashtable();
            //deserialize into local variables
            int[] qty = (int[])HashtableOrder["qty"];
            string[] prodcd = (string[])HashtableOrder["prodcd"];
            Decimal[] sellPrice = (Decimal[])HashtableOrder["msrp"];
            int cid = Convert.ToInt32(HashtableOrder["cid"]);

            bool boFlg = false;
            DateTime oDate = Convert.ToDateTime(HashtableOrder["shipdate"]);
            Decimal oTotal = Convert.ToDecimal(HashtableOrder["amt"]);
            eStoreDBEntities dbContext;

            //defining a transaction

            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    dbContext = new eStoreDBEntities();
                    Order myOrder = new Order();

                    //back to the db to get the right customer based on the session customer id
                    var selectedCusts = dbContext.Customers.Where(c => c.CustomerID == cid);
                    myOrder.Customer = selectedCusts.First();

                    //build the order
                    myOrder.OrderDate = DateTime.Now;
                    myOrder.ShipDate = oDate;
                    myOrder.OrderAmount = oTotal;
                    dbContext.AddToOrders(myOrder); // add order and get order id for line item

                    for (int idx = 0; idx < qty.Length; idx++)
                    {
                        if (qty[idx] > 0)
                        {
                            OrderLineitem item = new OrderLineitem();
                            string pcd = prodcd[idx];
                            var selectedProds = dbContext.Products.Where(p => p.prodcd == pcd);
                            item.Product = selectedProds.First();//got product for item

                            if (item.Product.qoh > qty[idx]) // enough stock
                            {
                                item.Product.qoh = item.Product.qoh - qty[idx];
                                item.QtySold = qty[idx];
                                item.QtyOrdered = qty[idx];
                                item.QtyBackOrdered = 0;
                                item.SellingPrice = sellPrice[idx];
                            }
                            else // not enough stock
                            {
                                item.QtyBackOrdered = qty[idx] - item.Product.qoh;
                                item.Product.qob = item.Product.qob + (qty[idx] - item.Product.qoh);
                                item.Product.qoh = 0;
                                item.QtyOrdered = qty[idx];
                                item.QtySold = item.QtyOrdered - item.QtyBackOrdered;
                                item.SellingPrice = sellPrice[idx];
                                boFlg = true; // something backordered
                            }
                            myOrder.OrderLineitems.Add(item);
                        }
                    }


                    dbContext.SaveChanges();
//                    throw new Exception("Rollback"); // test trans by uncommenting this line

                    transaction.Complete();
                    HashtableReturnValues.Add("orderid", myOrder.OrderID);
                    HashtableReturnValues.Add("boflag", boFlg);
                    HashtableReturnValues.Add("message", "");
                }
                catch (Exception ex)
                {
                    ErrorRoutine(ex, "OrderData", "AddOrder");
                    HashtableReturnValues.Add("message", "Problem with Order");

                }
                return HashtableReturnValues;

            }

        }
        /// <summary>
        /// Retrieve Order information for a single customer
        /// </summary>
        /// <param name="int custid"></param>
        /// <returns>List of Order instances for a particular customer</returns>
        public List<Order> GetAllForCust(int custid)
        {
            eStoreDBEntities dbContext;
            List<Order> allOrders = null;
            try
            {
                dbContext = new eStoreDBEntities();
                allOrders = dbContext.Orders.Where(o => o.Customer.CustomerID == custid).ToList();
            }
            catch( Exception ex )
            {
                ErrorRoutine(ex, "OrderData", "GetAllForCust");
            }
            return allOrders;
        }

        /// <summary>
        /// Get All Details for a particular order
        /// </summary>
        /// <param name="hshIDs">both orderid and customerid</param>
        /// <returns>List of details if customer owns order</returns>
        public List<SingleOrderDetails> GetAllDetailsForOrder(Hashtable HashtableIDs)
        {
            eStoreDBEntities dbContext;
            List<SingleOrderDetails> orderDetails = new List<SingleOrderDetails>();

            try
            {
                dbContext = new eStoreDBEntities();
                System.Nullable<int> cid = Convert.ToInt32(HashtableIDs["cid"]);
                System.Nullable<int> oid = Convert.ToInt32(HashtableIDs["oid"]);
                // executes stored proc via EF function
                orderDetails = dbContext.pSingleOrderDetails(oid, cid).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderData", "GetAllDetailsForOrder");
            }
            return orderDetails;
        }

        public List<SingleOrderDetails> GetAllDetailsForAllOrders(int custid)
        {
            eStoreDBEntities dbContext;
            List<SingleOrderDetails> orderDetails = new List<SingleOrderDetails>();

            try
            {
                dbContext = new eStoreDBEntities();
                System.Nullable<int> cid = custid;
                orderDetails = dbContext.pAllOrderDetails(cid).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderData", "GetAllDetailsForAllOrder");
            }
            return orderDetails;
        }

    }
}
