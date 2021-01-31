using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BackendProcessor
{
    class UtilityClass
    {
        public static string[] SplitMultiSpaceWithRegex(string inputString)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            string patter = "[ ]{2,}";
            return Regex.Split(inputString, patter);
        }

        public static string CleanString(string inputstring)
        {
            return inputstring.Trim(new char[] { ' ', '>' });
        }
        public static string[] CleanStringArray(string[] inputStringArray)
        {
            List<string> cleanStringList = new List<string>();
            foreach(string inputString in inputStringArray)
            {
                cleanStringList.Add(CleanString(inputString));
            }
            return cleanStringList.ToArray();
        }
    }
}
