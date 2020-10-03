using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CodeDesigner.ConsoleApp.Verbs;

namespace CodeDesigner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = Parser.Default.ParseArguments<MipsR9000, CodeDesignerLanguage, Pcsx2, Verbs.CheatEngine>(args)
              .MapResult(
                (MipsR9000 opts) => MipsR9000.Run(opts),
                (CodeDesignerLanguage opts) => CodeDesignerLanguage.Run(opts),
                (Pcsx2 opts) => Pcsx2.Run(opts),
                (Verbs.CheatEngine opts) => Verbs.CheatEngine.Run(opts),
                errs => 1);
        }
    }
}
