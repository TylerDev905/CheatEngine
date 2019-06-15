using CodeDesigner.Languages.MipsR5900;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.ConsoleApp.Verbs
{
    [Verb("MipsR9000", HelpText = "A MipsR9000 assembler/disassembler.")]
    public class MipsR9000
    {
        [Option('a', "Assemble", HelpText = "Assemble MIPsR9000 assembly code.")]
        public bool Assemble { get; set; }
        [Option('d', "Disassemble", HelpText = "Disassemble a MIPsR9000 operation.")]
        public bool Disassemble { get; set; }
        [Option('o', "Operation", HelpText = "The operation that will be Disassembled.")]
        public string Operation { get; set; }
        [Option('h', "OperationHex", HelpText = "The operation that will be Assembled.")]
        public string OperationHex { get; set; }
        private static Assembler _assembler { get; } = new Assembler();
        private static Disassembler _disassembler { get; } = new Disassembler();
        public static int Run(MipsR9000 mipsR9000)
        {

            if (mipsR9000.Assemble)
            {
               Console.WriteLine(_assembler.Assemble(mipsR9000.OperationHex));
            }

            if (mipsR9000.Disassemble)
            {
                Console.WriteLine(_disassembler.Disassemble(mipsR9000.Operation));
            }

            return 0;
        }
    }
}
