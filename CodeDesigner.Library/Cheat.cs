using CodeDesigner.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library
{
    public class Cheat
    {
        public int Position { get; set; }
        public CheatType CheatType { get; set; }
        public int Address { get; set; }
        public byte[] Data { get; set; }
    }
}
