using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }
}
