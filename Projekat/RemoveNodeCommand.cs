using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveNodeCommand : Command
    {
        private ConnectivityNode cn = new ConnectivityNode();
        private List<ACLineSegment> lines = new List<ACLineSegment>();
        private List<Terminal> terminals = new List<Terminal>();
        int index;

        public RemoveNodeCommand(String id)
        {
            cn.mRID = id;
        }

        public override void Execute()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(cn.mRID))
                {
                    cn = Singleton.Instance().Nodes[i];

                    for (int j = 0; j < Singleton.Instance().Substations.Count; j++)
                    {
                        for (int k = 0; k < Singleton.Instance().Substations[j].connectivityNodes.Count; k++)
                        {
                            if (Singleton.Instance().Substations[j].connectivityNodes[k].mRID.Equals(Singleton.Instance().Nodes[i].mRID))
                            {
                                foreach (Terminal t in Singleton.Instance().Terminals)
                                {
                                    bool isDeleted = false;
                                    string ID = string.Empty;

                                    if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                    {
                                        for (int m = 0; m < Singleton.Instance().Terminals.Count; m++)
                                        {
                                            if (t.mRID.Equals(Singleton.Instance().Terminals[m].mRID))
                                            {
                                                isDeleted = true;
                                                ID = Singleton.Instance().Terminals[m].mRID;

                                                terminals.Add(Singleton.Instance().Terminals[m]);
                                                Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[m]);
                                                if (m % 2 == 0)
                                                {
                                                    terminals.Add(Singleton.Instance().Terminals[m]);
                                                    Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[m]);
                                                }
                                                else
                                                {
                                                    terminals.Add(Singleton.Instance().Terminals[m - 1]);
                                                    Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[m - 1]);
                                                }

                                                break;
                                            }
                                        }
                                        if (isDeleted)
                                        {
                                            foreach (ACLineSegment line in Singleton.Instance().Lines)
                                            {
                                                if (ID.Contains(line.mRID))
                                                {
                                                    lines.Add(line);
                                                    Singleton.Instance().Lines.Remove(line);
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }

                                Singleton.Instance().Substations[j].connectivityNodes.RemoveAt(k);
                                index = j;
                                break;
                            }
                        }
                    }

                    Singleton.Instance().Nodes.Remove(Singleton.Instance().Nodes[i]);
                    break;
                }

            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Nodes.Add(cn);
            Singleton.Instance().Substations[index].connectivityNodes.Add(cn);

            foreach (ACLineSegment line in lines)
            {
                Singleton.Instance().Lines.Add(line);
            }

            foreach (Terminal t in terminals)
            {
                Singleton.Instance().Terminals.Add(t);
            }
        }
    }
}
