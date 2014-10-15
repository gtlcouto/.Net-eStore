using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;

namespace eStoreCase1Website
{
    /// <summary>
    /// Summary description for CreditCardVerify
    /// </summary>
    [WebService(Namespace = "http://eStoreCase2/CreditCardVerify")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CreditCardVerify : System.Web.Services.WebService
    {

        [WebMethod]
        public string VerifyCard(string CardNumber, string CardType)
        {
            return CheckCCNumber(CardNumber, CardType);
        }
        private string CheckCCNumber(string CardNumber, string CardType)
        {
            string sLength = "";
            string sPrefix = "";
            string sNumber = "";

            switch (CardType.ToUpper())
            {
                case "VISA":                    // Visa
                    sLength = "13;16";
                    sPrefix = "4";
                    break;
                case "MASTERCARD":                    // Mastercard
                    sLength = "16";
                    sPrefix = "51;52;53;54;55";
                    break;
                case "AMERICAN EXPRESS":                    // American Express
                    sLength = "15";
                    sPrefix = "34;37";
                    break;
                case "DISCOVER":                    //Discover Card
                    sLength = "16";
                    sPrefix = "6011";
                    break;
                default:
                    sLength = "";
                    sPrefix = "";
                    break;
            }

            sNumber = TrimToDigits(CardNumber);
            bool bPrefixValid = false;
            bool bLengthValid = false;
            char[] ch = ";".ToCharArray();

            //
            //   Clean out non-numbers 
            //
            foreach (string sStr1 in sPrefix.Split(ch))
            {
                if (sNumber.IndexOf(sStr1) == 0)
                    bPrefixValid = true;
            }

            foreach (string sStr2 in sLength.Split(ch))
            {
                if (Convert.ToString(sNumber.Length) == sStr2)
                    bLengthValid = true;
            }

            int iResult = 0;

            if (bPrefixValid == false)
                iResult += 1;

            if (bLengthValid == false)
                iResult += 2;

            int iQSum = 0;
            int iSum = 0;
            int iRemainder;

            if (sNumber.Length % 2 == 0)
                iRemainder = 0;
            else
                iRemainder = 1;

            //
            // Actual Luhn Algorithm here
            //
            foreach (char c in sNumber)
            {
                if (iRemainder % 2 == 0) //(mod = remainder after iRemainder/2)
                    iSum = 2 * Convert.ToInt32(Convert.ToString(c));
                else
                    iSum = Convert.ToInt32(Convert.ToString(c));

                if (iSum <= 9)
                    iQSum += iSum;
                else
                    iQSum += (iSum - 9);

                iRemainder += 1;
            }

            if (iQSum % 10 != 0)
                iResult += 4;

            if (sLength == "")
                iResult += 8;

            string sResult = "";

            switch (iResult)
            {
                case 0:
                    sResult = "OK";
                    break;
                case 1:
                    sResult = "WRONG CARD TYPE";
                    break;
                case 2:
                    sResult = "WRONG LENGTH";
                    break;
                case 3:
                    sResult = "WRONG LENGTH AND CARD TYPE";
                    break;
                case 4:
                    sResult = "WRONG CHECKSUM";
                    break;
                case 5:
                    sResult = "WRONG CHECKSUM AND CARD TYPE";
                    break;
                case 6:
                    sResult = "WRONG CHECKSUM AND LENGTH";
                    break;
                case 7:
                    sResult = "WRONG CHECKSUM, LENGTH, AND CARD TYPE";
                    break;
                case 8:
                    sResult = "UNKNOWN CARD TYPE";
                    break;
                default:
                    sResult = "GENERAL FAILURE";
                    break;
            }

            return sResult;
        }

        private string TrimToDigits(string CardNumber)
        {
            string ch = "";
            string sResult = "";

            for (int i = 0; i < CardNumber.Length; i++)
            {
                ch = CardNumber.Substring(i, 1);
                ASCIIEncoding AE = new ASCIIEncoding();
                byte[] converted = AE.GetBytes(ch);

                if (converted[0] >= 48 && converted[0] <= 57) // number
                    sResult += ch;
            }

            return sResult;
        }
    }
}
