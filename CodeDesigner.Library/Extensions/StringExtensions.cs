using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Extensions
{
    public static class StringExtensions
    {
        public static string[] Split(this string source, string seperator)
            => source.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        public static Match Match(this Regex regex, string input, string pattern)
            => Regex.Match(input, pattern, RegexOptions.IgnoreCase);

        public static MatchCollection Matches(this Regex regex, string input, string pattern)
            => Regex.Matches(input, pattern, RegexOptions.IgnoreCase);

        public static bool IsMatch(this Regex regex, string input, string pattern)
            => Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        
        public static string ReplaceAtIndex(this string targetString, int index, string insertString)
        {
            var replacementSequence = targetString.Skip(index).Take(insertString.Length).ToArray();
            return targetString.Replace(new string(replacementSequence), insertString);
        }
        public static byte[] StringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray()
                .Reverse()
                .ToArray();
        }
    }
}
