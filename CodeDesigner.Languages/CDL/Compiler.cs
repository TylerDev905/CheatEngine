using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using CodeDesigner.Languages.Extensions;

namespace CodeDesigner.Languages.CDL
{
    public class Compiler
    {
        public abstract class Token
        {
            public abstract Match Match { get; set; }
        }

        public class Comment : Token
        {
            public static string SingleLineRegex { get; } = @"^[/]{2}(.*)$";
            public static string MultiLineRegex { get; } = @"^\/([\s\S]*)\*\/$";
            public bool IsSingleLine { get; set; }
            public override Match Match { get; set; }        
        }

        public class Label : Token
        {
            public static string SetRegex { get; } = @"^(.{3,})\:$";
            public static string GetRegex { get; } = @" \:(.{3,})$";
            public bool IsSetLabel { get; set; }
            public override Match Match { get; set; }
        }

        public class Operation : Token
        {
            public static string Regex
            {
                get
                {
                    var instructionNames = MipsR5900.LanguageDefinition.Instructions
                        .Select(i => i.TextDisplay);

                    return $@"^\b({string.Join("|", instructionNames)})\b(.*)$";
                }
            }

            public override Match Match { get; set; }
        }

        public void Lexer(string cdsString)
        {
            cdsString = cdsString
                .Replace("\r", "");

            var tokens = new List<Token>();

            var sharedRegexOptions =
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.Compiled;

            tokens.AddRange(Regex.Matches(cdsString, Comment.SingleLineRegex, sharedRegexOptions)
                .ToToken(m => new Comment()
                {
                    Match = m,
                    IsSingleLine = true
                }));

            tokens.AddRange(Regex.Matches(cdsString, Comment.MultiLineRegex, sharedRegexOptions)
                .ToToken(m => new Comment()
                {
                    Match = m,
                    IsSingleLine = false
                }));

            tokens.AddRange(Regex.Matches(cdsString, Label.SetRegex, sharedRegexOptions)
                .ToToken(m => new Label()
                {
                    Match = m
                }));

            tokens.AddRange(Regex.Matches(cdsString, Operation.Regex, sharedRegexOptions)
                .ToToken(m => new Operation()
                {
                    Match = m
                })); 

            tokens = tokens.OrderBy(t => t.Match.Index).ToList();

        }  
    }
}
