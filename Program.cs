﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Efficient_CSHarp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string s = "23,3,23";
            string[] slist={s};
            String[] Slist={"ha"};

            string a = "<asdjlfs>>";
            a = a.Trim(new char[]{'<','>'});
            //UseList(["2"],["","3"]);
            //UseStringLR({s},{s});
            //UseStringLRUpper({[s]},{[s]}); 
            DataTableSelect.test1();
            Json2List.json1();

            string result = litiDemo.buildStructTree();
             result = litiDemo.createClassStruct();
            result = litiDemo.refXmlDocument();
            result = litiDemo.stringLists();
            slist = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void UseStringLR(string[] s, String[] Slist)
        {
            s = Slist;
            
        }
        static void UseStringLRUpper(String[] s, String[] Slist)
        {
            s = Slist;

        }
        static void UseList(List<String> s, List<String> Slist)
        {
            s = Slist;

        }
    }
}
