using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.CheatEngine.Exceptions
{
    public class OpenProcessException : Exception
    {
        public OpenProcessException(string message)
            : base(message)
        {
        }
    }
}
