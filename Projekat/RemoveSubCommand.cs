using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveSubCommand : Command
    {
        private Substation s = new Substation();
        private List<ACLineSegment> lines = new List<ACLineSegment>();
        private List<Terminal> terminals = new List<Terminal>();
        private List<int> terminalsID = new List<int>();

        public RemoveSubCommand(String id)
        {
            s.mRID = id;
        }

        public override void Execute()
        {
            
            bool checkAgain = false;

            for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
            {
                if (Singleton.Instance().Substations[i].mRID.Equals(s.mRID))
                {
                    s = Singleton.Instance().Substations[i];

                    foreach (ConnectivityNode cn2 in s.connectivityNodes)
                    {
                        foreach (ConnectivityNode cn in Singleton.Instance().Nodes)
                        {
                            if (cn.mRID.Equals(cn2.mRID))
                            {
                              while (!checkAgain)
                              { 
                                foreach (Terminal t in Singleton.Instance().Terminals)
                                {
                                    bool isDeleted = false;
                                    string ID = string.Empty;

                                    if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                    {
                                        for (int j = 0; j < Singleton.Instance().Terminals.Count; j++)
                                        {
                                            if (t.mRID.Equals(Singleton.Instance().Terminals[j].mRID))
                                            {
                                                isDeleted = true;
                                                
                                                ID = Singleton.Instance().Terminals[j].mRID;

                                                terminals.Add(Singleton.Instance().Terminals[j]);
                                                Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[j]);
                                                if (j % 2 == 0)
                                                {
                                                    terminals.Add(Singleton.Instance().Terminals[j]);
                                                    Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[j]);
                                                }
                                                else
                                                {
                                                    terminals.Add(Singleton.Instance().Terminals[j - 1]);
                                                    Singleton.Instance().Terminals.Remove(Singleton.Instance().Terminals[j - 1]);
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
                                                    checkAgain = true;
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (checkAgain)
                                    {
                                        checkAgain = false;
                                    }
                                else
                                    {
                                        break;
                                    }
                              }

                                Singleton.Instance().Nodes.Remove(cn);
                                break;
                            }
                        }
                    }
                    
                    foreach (PowerTransformer pt in s.Equipments)
                    {
                        Singleton.Instance().PowerTransformers.Remove(pt);
                    }

                    foreach (Equipment b in s.VoltageLevels[0].Equipments)
                    {
                        if (b is Breaker)
                        {
                            Singleton.Instance().Breakers.Remove((Breaker)b);
                        }
                    }

                    foreach (Equipment lbs in s.VoltageLevels[0].Equipments)
                    {
                        if (lbs is LoadBreakSwitch)
                        {
                            Singleton.Instance().LoadBreakers.Remove((LoadBreakSwitch)lbs);
                        }
                    }

                    foreach (Equipment dis in s.VoltageLevels[0].Equipments)
                    {
                        if (dis is Disconnector)
                        {
                            Singleton.Instance().Disconnectors.Remove((Disconnector)dis);
                        }
                    }

                    foreach (Equipment g in s.VoltageLevels[0].Equipments)
                    {
                        if (g is Ground)
                        {
                            Singleton.Instance().Ground.Remove((Ground)g);
                        }
                    }

                    foreach (Equipment synM in s.VoltageLevels[0].Equipments)
                    {
                        if (synM is SynchronousMachine)
                        {
                            Singleton.Instance().SynMachines.Remove((SynchronousMachine)synM);
                        }
                    }

                    foreach (Equipment consumer in s.VoltageLevels[0].Equipments)
                    {
                        if (consumer is EnergyConsumer)
                        {
                            Singleton.Instance().Consumers.Remove((EnergyConsumer)consumer);
                        }
                    }

                    Singleton.Instance().Substations.Remove(Singleton.Instance().Substations[i]);
                    break;
                }

            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Substations.Add(s);

            foreach (PowerTransformer pt in s.Equipments)
            {
                Singleton.Instance().PowerTransformers.Add(pt);
            }

            foreach (Equipment b in s.VoltageLevels[0].Equipments)
            {
                if (b is Breaker)
                {
                    Singleton.Instance().Breakers.Add((Breaker)b);
                }
            }

            foreach (Equipment lbs in s.VoltageLevels[0].Equipments)
            {
                if (lbs is LoadBreakSwitch)
                {
                    Singleton.Instance().LoadBreakers.Add((LoadBreakSwitch)lbs);
                }
            }

            foreach (Equipment dis in s.VoltageLevels[0].Equipments)
            {
                if (dis is Disconnector)
                {
                    Singleton.Instance().Disconnectors.Add((Disconnector)dis);
                }
            }

            foreach (Equipment g in s.VoltageLevels[0].Equipments)
            {
                if (g is Ground)
                {
                    Singleton.Instance().Ground.Add((Ground)g);
                }
            }

            foreach (Equipment synM in s.VoltageLevels[0].Equipments)
            {
                if (synM is SynchronousMachine)
                {
                    Singleton.Instance().SynMachines.Add((SynchronousMachine)synM);
                }
            }

            foreach (Equipment consumer in s.VoltageLevels[0].Equipments)
            {
                if (consumer is EnergyConsumer)
                {
                    Singleton.Instance().Consumers.Add((EnergyConsumer)consumer);
                }
            }

            foreach (ACLineSegment line in lines)
            {
                Singleton.Instance().Lines.Add(line);
            }
            lines.Clear();

            foreach (Terminal t in terminals)
            {
                Singleton.Instance().Terminals.Add(t);
            }
            terminals.Clear();

            foreach (ConnectivityNode cn in s.connectivityNodes)
            {
                Singleton.Instance().Nodes.Add(cn);
            }

        }
    }
}
