using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Json;

namespace WebApplication.Helpers
{
    public static class JSonHelpers
    {

        public static string SerializeJSon<T>(T t)
        {
            using (var stream = new MemoryStream())
            {
                var ds = new DataContractJsonSerializer(typeof (T));
                var s = new DataContractJsonSerializerSettings();
                ds.WriteObject(stream, t);
                string jsonString = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return jsonString;
            }
        }

        public static T DeserializeJSon<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T obj = (T) ser.ReadObject(stream);
                return obj;
            }
        }
    }
}