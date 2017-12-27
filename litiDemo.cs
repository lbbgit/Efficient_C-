using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
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

        public static string buildStructTree()
        {
            structTree st = new structTree("name", "1", new string[] { "a", "aa" }, new string[] { "b", "bb" });

            structTree st1 = new structTree("name1", "1", new string[] { "a", "aa" }, new string[] { "b", "bb" });
            structTree st2 = new structTree("name2", "1", new string[] { "a", "aa" }, new string[] { "b", "bb" });

            structTree st11 = new structTree("name11", "1", new string[] { "a", "aa" }, new string[] { "b", "bb" }); 
            structTree st12 = new structTree("name12", "1", new string[] { "a", "aa" }, new string[] { "b", "bb" });
            st1.AddChild(st11);
            st1.AddChild(st12);
            st.AddChild(st1);
            st.AddChild(st2);

            st2.AddChild(st);
            return string.Empty;
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

        public static object testParstringlist()
        {
            object obj1 = new object[]{ "1,", "2" };
            //object obj11 = {[ "1,", "2"] };
            //object obj2=["a","b",[1,33]];
            //object obj3=["a","b",["1","33"]];
            //object obj4=["a","b","1","33"];
            return obj1;
        } 



        //class struct 简单的速度没检测到差别
        public static string createClassStruct()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xdoc1 = xdoc.CreateElement("root");
            int count = 10000  *100;
            DateTime dt1 = DateTime.Now;
            object obj;
            for (int i = 0; i < count; i++)
            {
                obj = new classA(i);//XmlElement();
            }
            DateTime dt2 = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                obj = new structA(i);
            }
            DateTime dt3 = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                obj = xdoc.CreateElement(i.ToString());
            }
            DateTime dt4 = DateTime.Now;
            TimeSpan sp1class = dt2 - dt1;//time:7
            TimeSpan sp2struct = dt3 - dt2;//time:6
            TimeSpan sp2newXmlDoc = dt4 - dt3;//time:676
            return "str1:" + sp1class.Ticks + " strs:" + sp2struct.Ticks;
        }
    }

    //public struct structA
    //{
    //    public int A;
    //} 
    //public class classA
    //{
    //    public int A;
    //}
    public struct structA
    {
        //public int A = 90; //错误:“structA.A”: 结构中不能有实例字段初始值
        public int A;
        //public structA() //错误:结构不能包含显式的无参数构造函数
        //{
        //    this.A = 0;
        //} 
        public structA(int paraA) //ok
        {
            this.A = paraA;
        }
    }

    public class classA
    {
        public int A = 90;
        public classA(int paraA)
        {
            this.A = paraA;
        }
    }
    public struct structAttr 
    {
        public string name, val;
        public structAttr(string _name, string _val) //ok
        {
            this.name = _name;
            this.val = _val;
        }
    }
    //public struct structTree
    //{
    //    public string name, innertext;
    //    List<structAttr> list  ;
    //    List<structTree> sons  ;
    //    public structTree(string _name,string _text=null,
    //        string[] names=null,string[] vals=null) //ok
    //    {
    //        this.name = _name;
    //        this.innertext=_text;
    //        list = new List<structAttr>(); 
    //        sons = new List<structTree>();
    //        if (names != null)
    //        {
    //            for (int i = 0; i < names.Length; i++)
    //                list.Add(new structAttr(names[i], vals[i]));
    //        }
    //    }
    //    public void AddChild(structTree sonTree)
    //    {
    //        sons.Add(sonTree);
    //    }
    //    public void AddChild(structTree[] sonTrees)
    //    {
    //        foreach (structTree sonTree in sonTrees)
    //            sons.Add(sonTree);
    //    }
    //}

    public struct structTree
    {
        public string name, innertext;
        Dictionary<string, string> attr ; 
        List<structTree> sons;
        public structTree(string _name, string _text = null,
            string[] names = null, string[] vals = null) //ok
        {
            this.name = _name;
            this.innertext = _text;
            this.sons = null;//must deniey it here
            this.attr = null;//must ver it here
            if (names != null)
            {
                attr = new Dictionary<string, string>();
                for (int i = 0; i < names.Length; i++)
                    attr.Add(names[i], vals[i]);
            }
        }
         public void AddChild(structTree sonTree)
        {
            if (this.sons == null)
                this.sons = new List<structTree>();
            sons.Add(sonTree);
        }
    }
}
