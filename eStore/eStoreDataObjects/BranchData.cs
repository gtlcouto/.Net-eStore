using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStoreDataObjects
{
    public class BranchData : eStoreConfigData 
    {
        public List<GetThreeClosestBranches> GetAllDetailsForBranches(Hashtable HashtableAddresses)
        {
            eStoreDBEntities dbContext;
            List<GetThreeClosestBranches> branchDetails = new List<GetThreeClosestBranches>();

            try
            {
                dbContext = new eStoreDBEntities();
                System.Nullable<float> lat = Convert.ToSingle(HashtableAddresses["lat"]);
                System.Nullable<float> longi = Convert.ToSingle(HashtableAddresses["long"]);
                // executes stored proc via EF function
                branchDetails = dbContext.pGetThreeClosestBranches(lat, longi).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "BranchData", "GetAllDetailsForBranches");
            }
            return branchDetails;
        }

    }
}
