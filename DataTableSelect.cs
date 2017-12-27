using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Efficient_CSHarp
{
     public class DataTableSelect
    {
         public static void Select(DataTable dt)
         {
           
         }

         public static string[] GetNamesFromDataTable(DataTable dataTable, string col)
         {
             DataView dv = dataTable.DefaultView;
             dataTable = dv.ToTable(true, col);
             string[] names = new string[dataTable.Rows.Count];
             for (int i = 0; i < names.Length; i++)
             {
                 names[i] = dataTable.Rows[i][0].ToString();
             }
             return names;
         }
         public static DataTable GetDataTableDistinctCols(DataTable dataTable, string[] cols)
         {
             DataView dv = dataTable.DefaultView;
             return  dv.ToTable(true, cols); 
         }
         public static void test1()
         {
             DataTable dt = new DataTable();
             string[] ss= new string[]{"a","b","c"};
             foreach (string s in ss)
                 dt.Columns.Add(s);
             dt.Rows.Add(new object[] { 1, 2, 3 });
             dt.Rows.Add(new object[] { 1, 2, 3 });
             dt.Rows.Add(new object[] { 2, 2, 3 });
             dt.Rows.Add(new object[] { 1, 4, 1 });
             dt.Rows.Add(new object[] { 1, 4, 3 });

             DataTable dttemp;
             dttemp = GetDataTableDistinctCols(dt, new string[] { "a"});
             dttemp = GetDataTableDistinctCols(dt, new string[] { "b"});
             dttemp = GetDataTableDistinctCols(dt, new string[] { "a","b"});
             dttemp = GetDataTableDistinctCols(dt, new string[] { "a","b","c"});
             dttemp = dt;
         }
    }
}
