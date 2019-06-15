using CodeDesigner.Library.Extensions;
using CodeDesigner.Languages.MipsR5900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Editors
{
    public class Pcsx2Editor : ProcessMemoryEditor
    {
        private Assembler _assembler { get; } = new Assembler();
        private Disassembler _disassembler { get; } = new Disassembler();
        public Pcsx2Editor(string processName = "pcsx2") : base(processName = "pcsx2") { }
        public void WriteOperation(int address, string operation)
        {
            var hexString = _disassembler.Disassemble(operation);
            Write(address, hexString.StringToByteArray());
        }
        public void WriteOperation(string address, string operationString)
        {
            WriteOperation(Convert.ToInt32(address, 16), operationString);
        }
        public string ReadOperation(int address)
        {
            var bytes = Read(address, 4);
            return _disassembler.Disassemble(BitConverter.ToString(bytes));
        }
        public string ReadOperation(string address)
        {
            return ReadOperation(Convert.ToString(int.Parse(address), 16));
        }
        public void OpenEmulator()
        {

        }
        public void InstallAndConfig(string version)
        {

        }

    }
}
