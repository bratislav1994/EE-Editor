using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveSynMachineCommand : Command
    {
        private SynchronousMachine sm = new SynchronousMachine();

        public RemoveSynMachineCommand(String id)
        {
            sm.mRID = id;
        }

        public override void Execute()
        {
            foreach (SynchronousMachine synMachine in Singleton.Instance().SynMachines)
            {
                if (synMachine.mRID.Equals(sm.mRID))
                {
                    sm = synMachine;

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.VoltageLevels[0].Equipments)
                        {
                            if (e.mRID.Equals(sm.mRID))
                            {
                                s.VoltageLevels[0].Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().SynMachines.Remove(synMachine);
                    break;
                }

            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().SynMachines.Add(sm);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if(s.mRID.Equals(sm.substationID))
                {
                    for (int i = 0; i < s.VoltageLevels.Count; i++)
                    {
                        if (s.VoltageLevels[i].BaseVoltage.nominalVoltage >= sm.minU)
                        {
                            s.VoltageLevels[i].Equipments.Add(sm);
                            break;
                        }
                    }
                }
            }

        }
    }
}
