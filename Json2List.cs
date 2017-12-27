using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Web.Script.Serialization;//System.Web.Extensions.dll。

using System.Collections;
using System.Runtime.Serialization;//.dll
using System.Runtime.Serialization.Json;

namespace Efficient_CSHarp
{
    public class nv { public string name, value;}
    public class nvgs
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class Json2List
    {
        public static List<string> jsons = new List<string>
        {  
            "[{\"name\":\"下限\",\"value\":\"9000\"},{\"name\":\"上限\",\"value\":\"11000\"}]",
            "[{\"name\":\"中心值\",\"value\":\"360\"},{\"name\":\"下公差\",\"value\":\"10\"},{\"name\":\"上公差\",\"value\":\"10\"}]", 
            "[{\"name\":\"枚举\",\"value\":\"中\"},{\"name\":\"枚举\",\"value\":\"上\"},{\"name\":\"枚举\",\"value\":\"下\"},{\"name\":\"枚举\",\"value\":\"左\"},{\"name\":\"枚举\",\"value\":\"右\"},{\"name\":\"枚举\",\"value\":\"左上\"},{\"name\":\"枚举\",\"value\":\"右上\"},{\"name\":\"枚举\",\"value\":\"左下\"},{\"name\":\"枚举\",\"value\":\"右下\"}]"

        };


        public static void json1()
        {
             Dictionary<string, string> obj1=new  Dictionary<string, string>();
             Hashtable ht = new Hashtable();
             List<nv> nv1 = new List<nv>();
             List<nvgs> nvgs1 = new List<nvgs>();
            object obj,obj2;
            int i = 0;
            foreach (string s in jsons)
            {
                obj = JsonToObject(s, nv1);
                obj2 = JsonToObject(s, nvgs1);
                nv1 = obj as List<nv>;
                nvgs1 = obj2 as List<nvgs>;
                i++;
            }
        }

        /// <summary>  
        /// 对象转为json  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string ObjToJsonString<ObjType>(ObjType obj) where ObjType : class
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string s = jsonSerializer.Serialize(obj);
            return s;
        }

        /// <summary>  
        /// json转为对象  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="JsonString"></param>  
        /// <returns></returns>  
        public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ObjType s = jsonSerializer.Deserialize<ObjType>(JsonString);
            return s;
        }

        // 从一个Json串生成对象信息
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }

    }
}
