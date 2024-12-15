using CodeDesigner.Library.Editors;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.ConsoleApp.Verbs
{
    [Verb("Pcsx2", HelpText = "Manipulate pcsx2 game memory.")]
    public class Pcsx2
    {
        [Option('w', "WriteOperation", HelpText = "Write an operation at the specified address.")]
        public bool WriteOperation { get; set; }
        [Option('r', "ReadOperation", HelpText = "Read an operation at the specified address.")]
        public bool ReadOperation { get; set; }
        [Option('a', "Address", HelpText = "The address to read or write to.")]
        public string Address { get; set; }
        [Option('o', "Operation", HelpText = "The operation that will be written or read from the specified address.")]
        public string Operation { get; set; }
        [Option('s', "StartProcess", HelpText = "Start the Pcsx2 process.")]
        public bool StartProcess { get; set; }
        [Option('i', "InstallAndConfig", HelpText = "Install and configure the pcsx2 emulator(Installs all software needed to run the pcsx2 while updating the pcsx2 config files).")]
        public bool InstallAndConfig { get; set; }
        [Option('p', "Pcsx2Version", HelpText = "The version the installAndConfig will install/upgrade/downgrade to.")]
        public string Version { get; set; }
        private static Pcsx2Editor _pcsx2Editor { get; set; } = new Pcsx2Editor();

        public static int Run(Pcsx2 pcsx2Options)
        {

            if (pcsx2Options.ReadOperation)
            {
                Console.WriteLine(_pcsx2Editor.ReadOperation(pcsx2Options.Address));
            }

            if (pcsx2Options.WriteOperation)
            {
                _pcsx2Editor.WriteOperation(pcsx2Options.Address, pcsx2Options.Operation);
                Console.WriteLine($"Wrote operation {pcsx2Options.Operation} to address {pcsx2Options.Address}.");
            }

            if (pcsx2Options.StartProcess)
            {
                _pcsx2Editor.OpenEmulator();
                Console.WriteLine("The pcsx2 process was started.");
            }

            if (pcsx2Options.InstallAndConfig)
            {
                _pcsx2Editor.InstallAndConfig(pcsx2Options.Version);
                Console.WriteLine("The pcsx2 process was started.");
            }

            return 0;
        }
    }
}