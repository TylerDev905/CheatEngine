using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Enums
{
    public enum CheatType
    {
        _8BitWrite = 0x00,
        _16BitWrite = 0x10,
        _32BitWrite = 0x20,
        _copyBytes = 0x50,
        _pointerWrite = 0x70,
        _timer = 0xB0,
        _32BitCondition = 0xC0,
        _16BitCondition = 0xD0
    }
}
