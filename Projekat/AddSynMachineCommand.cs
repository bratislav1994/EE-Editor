using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddSynMachineCommand : Command
    {
        private SynchronousMachine sm = new SynchronousMachine();

        public AddSynMachineCommand(string alias, string desc, string name, float baseQ, float maxQ, float maxU, float minQ, float minU, SynchronousMachineKind type, float voltRegRange, float ratedPowFactor, float ratedS, float ratedU, string subID)
        {
            sm.aliasName = alias;
            sm.baseQ = baseQ;
            sm.description = desc;
            sm.maxQ = maxQ;
            sm.maxU = maxU;
            sm.minQ = minQ;
            sm.minU = minU;
            sm.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            sm.name = name;
            sm.ratedPowerFactor = ratedPowFactor;
            sm.ratedS = ratedS;
            sm.ratedU = ratedU;
            sm.type = type;
            sm.voltageRegulationRange = voltRegRange;
            sm.substationID = subID;
        }

        public override void Execute()
        {
            Singleton.Instance().SynMachines.Add(sm);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(sm.substationID))
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

        public override void UnExecute()
        {
            foreach (SynchronousMachine synM in Singleton.Instance().SynMachines)
            {
                if (synM.mRID.Equals(sm.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (synM.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (synM.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().SynMachines.Remove(synM);
                    break;
                }
            }
        }
    }
}
