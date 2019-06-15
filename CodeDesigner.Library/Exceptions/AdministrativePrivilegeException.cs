using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Exceptions
{
    public class AdministrativePrivilegeException :  Exception
    {
        public AdministrativePrivilegeException(string message)
            : base(message)
        {
        }
    }
}
