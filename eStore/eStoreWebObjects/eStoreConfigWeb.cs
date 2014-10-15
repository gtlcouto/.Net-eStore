using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStoreWebObjects
{
    public class eStoreConfigWeb
    {
        /// <sumary>
        /// Static error routine to be used by all methods
        /// </summary>
        /// <param name="e">Exception thrown</param>
        /// <param name="obj">Class throwing</param>
        /// <param name="method">method throwing</param>
        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            //create log and source with LogCreaTor app before using this method
            EventLog log = new EventLog();
            log.Source = "Info3067 Case 1";
            log.Log = "Info3067";
            if (e.InnerException != null)
            {
                log.WriteEntry("Error in eStoreWebObjects, object = " + obj + ", method = " + method + ", message = " + e.Message + " " + e.InnerException.Message, EventLogEntryType.Error);
                throw e.InnerException;
            }
            else
            {
                log.WriteEntry("Error in eStoreWebObjects, object = " + obj + ", method = " + method + ", message = " + e.Message, EventLogEntryType.Error);
                throw e;
            }

        }
        
        /// <sumary>
        /// Serializer
        /// </sumary>
        /// <param name="inObject">Object to be serialized</param>
        /// <returns>Serialized Object in Byte Array</returns>
        public static byte[] Serializer(Object inObject)
        {
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream();
            frm.Serialize(strm, inObject);
            byte[] ByteArrayObject = strm.ToArray();
            return ByteArrayObject;
        }

        /// <sumary>
        /// Deserializer
        /// </sumary>
        /// <param name="ByteArrayIn">Serialized object from BusinessUser Layer</param>
        /// <returns>Reconstructed Object</returns>
        public static Object Deserializer(byte[] ByteArrayIn)
        {
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream();
            Object returnObject = frm.Deserialize(strm);
            return returnObject;
        }
        
    }
}
