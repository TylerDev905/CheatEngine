using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library
{
    public class MemoryViewer
    {
        public IByteEditor ByteEditor { get; set; }
        public int Address { get; set; } = 0x00;
    }
}
