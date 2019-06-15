using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Exceptions
{
    public class ProcessAttachException : Exception
    {
        public ProcessAttachException(string message)
            : base(message)
        {
        }
    }
}
