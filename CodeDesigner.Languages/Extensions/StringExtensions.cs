using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeDesigner.Languages.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceAtIndex(this string targetString, int index, string insertString)
            => targetString.Remove(index, insertString.Length)
                .Insert(index, insertString);
        public static string[] Split(this string source, string seperator)
            => source.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
    }
}
