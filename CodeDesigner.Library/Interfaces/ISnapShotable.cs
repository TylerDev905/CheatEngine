using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Interfaces
{
    interface ISnapShotable
    {
        byte[] SnapShot(int snapShotStartIndex, int snapShotLength);
    }
}
