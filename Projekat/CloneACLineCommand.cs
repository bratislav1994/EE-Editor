using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneACLineCommand : Command
    {
        private ACLineSegment line = new ACLineSegment();
        private ACLineSegment acls = null;
        private Terminal terminal;
        private Terminal terminal2;
        private string ID = string.Empty;

        public CloneACLineCommand(string id)
        {
            line.mRID = id;
            acls = new ACLineSegment(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
            int counter = 0;

            foreach (ACLineSegment l in Singleton.Instance().Lines)
            {
                if (id.Equals(l.mRID))
                {
                    foreach (Terminal t in Singleton.Instance().Terminals)
                    {
                        if (t.mRID.Contains(id))
                        {
                            if (counter == 0)
                            {
                                counter++;
                                foreach (ConnectivityNode cn in Singleton.Instance().Nodes)
                                {
                                    if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                    {
                                        terminal = new Terminal()
                                        {
                                            aliasName = "Terminal",
                                            ConductingEquipment = new ConductingEquipment(),
                                            connected = false,
                                            ConnectivityNode = cn,
                                            description = "something",
                                            mRID = ID + "1",
                                            name = "Terminal",
                                            phases = PhaseCode.A,
                                            sequenceNumber = 1
                                        };
                                    }
                                }
                            }
                            else
                            {
                                foreach (ConnectivityNode cn in Singleton.Instance().Nodes)
                                {
                                    if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                    {
                                        terminal2 = new Terminal()
                                        {
                                            aliasName = "Terminal",
                                            ConductingEquipment = new ConductingEquipment(),
                                            connected = false,
                                            ConnectivityNode = cn,
                                            description = "something",
                                            mRID = ID + "1",
                                            name = "Terminal",
                                            phases = PhaseCode.A,
                                            sequenceNumber = 1
                                        };
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Execute()
        {
            line = (ACLineSegment)acls.Clone();
            line.mRID = ID;
            Singleton.Instance().Lines.Add(line);
            Singleton.Instance().Terminals.Add(terminal);
            Singleton.Instance().Terminals.Add(terminal2);

            for (int i = 0; i < Singleton.Instance().Terminals.Count; i++)
            {
                Terminal a = Singleton.Instance().Terminals[i];
            }
        }

        public override void UnExecute()
        {
            for (int i = 0; i < Singleton.Instance().Lines.Count; i++)
            {
                if (Singleton.Instance().Lines[i].mRID.Equals(line.mRID))
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

                    Singleton.Instance().Lines.Remove(Singleton.Instance().Lines[i]);
                    break;
                }

            }
        }
    }
}
