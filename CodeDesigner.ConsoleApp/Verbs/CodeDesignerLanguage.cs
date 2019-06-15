using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.ConsoleApp.Verbs
{
    [Verb("CDL", HelpText = "Compile/De-compile code designer source code.")]
    public class CodeDesignerLanguage
    {
        [Option('c', "Compile", HelpText = "Compile code designer source code.")]
        public bool Compile { get; set; }
        [Option('d', "Decompile", HelpText = "Compile code designer source code.")]
        public bool Decompile { get; set; }
        [Option('s', "Source", HelpText = "A string of the code designer source code.")]
        public string Source { get; set; }

        public static int Run(CodeDesignerLanguage cdlOptions)
        {

            if (cdlOptions.Compile)
            {
                var compiler = new Languages.CDL.Compiler();
                compiler.Lexer(System.IO.File.ReadAllText(@"Resources\test.cds"));
            }

            if (cdlOptions.Decompile)
            {

            }

            return 0;
        }
    }
}
