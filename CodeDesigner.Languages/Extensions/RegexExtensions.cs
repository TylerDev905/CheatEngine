using CodeDesigner.Languages.CDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeDesigner.Languages.Extensions
{
    public static class RegexExtensions
    {
        public static List<T> ToToken<T>(this MatchCollection matches, Func<Match, T> conversionFnc)
            => matches.OfType<Match>().Select(conversionFnc).ToList();
    }
}
