using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveACLineCommand : Command
    {
        private ACLineSegment line = new ACLineSegment();
        private Terminal terminal = new Terminal();
        private Terminal terminal2 = new Terminal();

        public RemoveACLineCommand(String id)
        {
            line.mRID = id;
        }

        public override void Execute()
        {
            foreach (ACLineSegment l in Singleton.Instance().Lines)
            {
                if (l.mRID.Equals(line.mRID))
                {
                    line = l;

                    foreach (Terminal t in Singleton.Instance().Terminals)
                    {
                        if (t.mRID.Contains(line.mRID))
                        {
                            terminal = t;
                            Singleton.Instance().Terminals.Remove(t);
                            break;
                        }
                    }

                    foreach (Terminal t in Singleton.Instance().Terminals)
                    {
                        if (t.mRID.Contains(line.mRID))
                        {
                            terminal2 = t;
                            Singleton.Instance().Terminals.Remove(t);
                            break;
                        }
                    }

                    Singleton.Instance().Lines.Remove(l);
                    break;
                }

            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Lines.Add(line);
            Singleton.Instance().Terminals.Add(terminal);
            Singleton.Instance().Terminals.Add(terminal2);
        }
    }
}
