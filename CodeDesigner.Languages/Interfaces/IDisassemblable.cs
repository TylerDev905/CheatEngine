using CodeDesigner.Languages.MipsR5900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Languages.Interfaces
{
    public interface IDisassemblable
    {
        string Disassemble(string operationString);
    }
}
