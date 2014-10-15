using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStoreDataObjects
{
    public class ProductData : eStoreConfigData 
    {
        public List<Product> GetAll()
        {
            eStoreDBEntities dbContext;
            List<Product> allProducts = null;
            try
            {
                dbContext = new eStoreDBEntities();
                allProducts = dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProductData", "GetAll");
            }
            return allProducts;
        }
    }
}
