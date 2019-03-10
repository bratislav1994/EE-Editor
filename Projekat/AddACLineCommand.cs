using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddACLineCommand : Command
    {
        private ACLineSegment line = new ACLineSegment();
        private Terminal terminal;
        private Terminal terminal2;

        public AddACLineCommand(string[] values, ConnectivityNode cn, ConnectivityNode cn2)
        {
            line.aliasName = values[0];
            line.description = values[1];
            line.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            line.name = values[2];
            line.length = float.Parse(values[3]);
            line.bch = float.Parse(values[4]);
            line.gch = float.Parse(values[5]);
            line.r = float.Parse(values[6]);
            line.x = float.Parse(values[7]);

            terminal = new Terminal()
            {
                aliasName = "Terminal",
                ConductingEquipment = new ConductingEquipment(),
                connected = false,
                ConnectivityNode = cn,
                description = "something",
                mRID = line.mRID + "1",
                name = "Terminal",
                phases = PhaseCode.A,
                sequenceNumber = 1
            };

            terminal2 = new Terminal()
            {
                aliasName = "Terminal",
                ConductingEquipment = new ConductingEquipment(),
                connected = false,
                ConnectivityNode = cn2,
                description = "something",
                mRID = line.mRID + "2",
                name = "Terminal",
                phases = PhaseCode.A,
                sequenceNumber = 1
            };
        }

        public override void Execute()
        {
            Singleton.Instance().Lines.Add(line);
            Singleton.Instance().Terminals.Add(terminal);
            Singleton.Instance().Terminals.Add(terminal2);
        }

        public override void UnExecute()
        {
            foreach (ACLineSegment l in Singleton.Instance().Lines)
            {
                if (line.mRID.Equals(l.mRID))
                {
                    foreach (Terminal t in Singleton.Instance().Terminals)
                    {
                        if (t.mRID.Contains(line.mRID))
                        {
                            Singleton.Instance().Terminals.Remove(t);
                            break;
                        }
                    }

                    foreach (Terminal t in Singleton.Instance().Terminals)
                    {
                        if (t.mRID.Contains(line.mRID))
                        {
                            Singleton.Instance().Terminals.Remove(t);
                            break;
                        }
                    }

                    Singleton.Instance().Lines.Remove(l);
                    break;
                }
            }
        }
    }
}
