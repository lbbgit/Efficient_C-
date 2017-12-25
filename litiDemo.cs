using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Timers;
namespace Efficient_CSHarp
{
    public class litiDemo
    {
        

        public static void d()
        {
            DateTime dt1 = DateTime.Now;
            for (var i = 0; i < 100; i++)
            {

            }

            DateTime dt2 = DateTime.Now;
            for (var i = 0; i < 100; i++)
            {

            }


            DateTime dt3 = DateTime.Now;
        }

        //new string[]{} 稍微快2/7
        public static string stringLists()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xdoc1 = xdoc.CreateElement("root");
            int count = 10000 * 100;
            DateTime dt1 = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
               string1(  i.ToString());
            }
            DateTime dt2 = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                strings(new string[] { i.ToString() });
            }
            DateTime dt3 = DateTime.Now;
            TimeSpan sp1String1 = dt2 - dt1;
            TimeSpan sp2Strings = dt3 - dt2;
            return "str1:" + sp1String1.Ticks + " strs:" + sp2Strings.Ticks;
        }
        public static string string1(string xdoc )
        {
            return xdoc;
        }
        public static string strings(string[] xdoc)
        {
            return xdoc[0];
        }

        //ref 快一倍
        public static string refXmlDocument()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xdoc1 = xdoc.CreateElement("root");
            int count = 10000 * 100;
            DateTime dt1 = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                xdoc1.AppendChild(createXmlElementDir(xdoc, i.ToString()));
            }
            DateTime dt2 = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                xdoc1.AppendChild(createXmlElementRef(ref xdoc, i.ToString()));
            }
            DateTime dt3 = DateTime.Now;
            TimeSpan sp1Dir = dt2 - dt1;
            TimeSpan sp2Ref = dt3 - dt2;
            return "dir:" + (dt2 - dt1).Ticks + " ref:" + (dt3 - dt2).Ticks;
        }

        public static XmlElement createXmlElementDir(XmlDocument xdoc, string name)
        {
            return xdoc.CreateElement(name);
        }
        public static XmlElement createXmlElementRef(ref XmlDocument xdoc, string name)
        {
            return xdoc.CreateElement(name);
        }


        //new string[]{} 稍微快2/7
        public static string createClassStruct()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xdoc1 = xdoc.CreateElement("root");
            int count = 10000;// *100;
            DateTime dt1 = DateTime.Now;
            object obj;
            for (var i = 0; i < count; i++)
            {
                obj = new classA();//XmlElement();
            }
            DateTime dt2 = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                obj = new structA();
            }
            DateTime dt3 = DateTime.Now;
            TimeSpan sp1class = dt2 - dt1;
            TimeSpan sp2struct = dt3 - dt2;
            return "str1:" + sp1class.Ticks + " strs:" + sp2struct.Ticks;
        }
    }

    public struct structA
    {
        public int A;
    }

    public class classA
    {
        public int A;
    }

}
