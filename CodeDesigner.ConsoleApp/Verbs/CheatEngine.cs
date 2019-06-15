using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.ConsoleApp.Verbs
{
    [Verb("CheatEngine", HelpText = "A cheat engine that can work with any game.")]
    public class CheatEngine
    {
        [Option('c', "Cheats", HelpText = "A list of cheats to patch memory with.")]
        public bool Cheats { get; set; }

        [Option('f', "FilePath", HelpText = "The file path of the cheat list(extension .cdl).")]
        public string FilePath { get; set; }
        public static int Run(CheatEngine cheatEngine)
        {
            return 0;
        }
    }
}
