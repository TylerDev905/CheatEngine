using CodeDesigner.Library.Extensions;
using CodeDesigner.Languages.MipsR5900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CodeDesigner.ConsoleApp.Verbs;
using CodeDesigner.Library.Editors;

namespace CodeDesigner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var processMemory = new ProcessMemoryEditor("PCSX2dis");


            //Console.Write($"{System.IO.File.ReadAllText(@"Resources\WelcomeText.txt")}\n\n");

            _ = Parser.Default.ParseArguments<MipsR9000, CodeDesignerLanguage, Pcsx2, CheatEngine>(args)
              .MapResult(
                (MipsR9000 opts) => MipsR9000.Run(opts),
                (CodeDesignerLanguage opts) => CodeDesignerLanguage.Run(opts),
                (Pcsx2 opts) => Pcsx2.Run(opts),
                (CheatEngine opts) => CheatEngine.Run(opts),
                errs => 1);
        }
    }
}
