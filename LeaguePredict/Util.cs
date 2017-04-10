using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeaguePredict
{
    class Util
    {
        public static string GetMiddle(string data, string begin, string end)
        {
            string strReturn = "";
            string beginString = begin;
            string endString = end;

            int beginPosition = data.IndexOf(beginString, StringComparison.Ordinal);
            if (beginPosition >= 0)
            {
                int valueBegin = beginPosition + beginString.Length;
                int valueEnd = data.IndexOf(endString, valueBegin, StringComparison.Ordinal);

                if (valueEnd > valueBegin)
                    strReturn = data.Substring(valueBegin, valueEnd - valueBegin).Trim();
            }
            return strReturn;
        }

        // ReSharper disable once InconsistentNaming
        public static List<string> GetMiddleAL(string data, string begin, string end)
        {
            data = data.Replace("\n", "");
            data = data.Replace("\r", "");
            string pattern = begin + ".*?" + end;
            pattern = pattern.Replace("(", "\\(");
            var matches = Regex.Matches(data, pattern);
            return (from Match nextOne in matches
                select nextOne.Value
                into strTemp
                select GetMiddle(strTemp, begin, end).Replace("&amp; ", "")).ToList();
        }
    }
}
