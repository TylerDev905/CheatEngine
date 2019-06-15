using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Languages.Interfaces
{
    public interface IAssemblable
    {
        string Assemble(string hexString);
    }
}
