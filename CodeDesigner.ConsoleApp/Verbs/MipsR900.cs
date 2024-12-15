using CodeDesigner.Languages.MipsR5900;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.ConsoleApp.Verbs
{
    [Verb("MipsR9500", HelpText = "A MipsR9000 assembler/disassembler.")]
    public class MipsR9500
    {
        [Option('a', "Assemble", HelpText = "Assemble MIPsR9500 assembly code.")]
        public bool Assemble { get; set; }
        [Option('d', "Disassemble", HelpText = "Disassemble a MIPsR9500 operation.")]
        public bool Disassemble { get; set; }
        [Option('o', "Operation", HelpText = "The operation that will be Disassembled.")]
        public string Operation { get; set; }
        [Option('h', "OperationHex", HelpText = "The operation that will be Assembled.")]
        public string OperationHex { get; set; }
        private static Assembler _assembler { get; } = new Assembler();
        private static Disassembler _disassembler { get; } = new Disassembler();
        public static int Run(MipsR9500 mipsR9500)
        {

            if (mipsR9500.Assemble)
            {
                Console.WriteLine(_assembler.Assemble(mipsR9500.OperationHex));
            }

            if (mipsR9500.Disassemble)
            {
                Console.WriteLine(_disassembler.Disassemble(mipsR9500.Operation));
            }

            return 0;
        }
    }
}
