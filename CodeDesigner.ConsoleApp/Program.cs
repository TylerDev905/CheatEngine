using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CodeDesigner.ConsoleApp.Verbs;
using System.Net.Sockets;

namespace CodeDesigner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.File.ReadAllText("Resources/WelcomeMsg.txt"));

            var commandArgs = args;

            if (!commandArgs.Any())
            {
                commandArgs = new string[] { "help" };
            }

            var endMainLoop = false;

            while (!endMainLoop)
            {
                _ = Parser.Default.ParseArguments<MipsR9500, CodeDesignerLanguage, Pcsx2, Verbs.CheatEngine>(commandArgs)
                  .MapResult(
                    (MipsR9500 opts) => MipsR9500.Run(opts),
                    (CodeDesignerLanguage opts) => CodeDesignerLanguage.Run(opts),
                    (Pcsx2 opts) => Pcsx2.Run(opts),
                    (Verbs.CheatEngine opts) => Verbs.CheatEngine.Run(opts),
                    errs => 1);

                Console.WriteLine("_________________________________________________________________________________________");
                Console.WriteLine("Input a command: ");
                commandArgs = Console.ReadLine().Split(' ');
            }
        }
    }
}
