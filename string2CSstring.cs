using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Efficient_CSHarp
{
    public class string2CSstring
    {
        public static string Convert(string s)
        {
            return s.Replace("\"", "\\\"");
        }
    }
}
