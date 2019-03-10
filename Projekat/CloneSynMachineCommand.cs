using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneSynMachineCommand : Command
    {
        private SynchronousMachine synMachine = new SynchronousMachine();
        private SynchronousMachine sm = null;
        private string ID = string.Empty;

        public CloneSynMachineCommand(String id)
        {
            synMachine.mRID = id;
            sm = new SynchronousMachine(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            synMachine = (SynchronousMachine)sm.Clone();
            synMachine.mRID = ID;
            Singleton.Instance().SynMachines.Add(synMachine);
        }

        public override void UnExecute()
        {
            foreach (SynchronousMachine sm2 in Singleton.Instance().SynMachines)
            {
                if (sm2.mRID.Equals(synMachine.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(sm2.substationID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (e.mRID.Equals(sm2.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().SynMachines.Remove(sm2);
                    break;
                }

            }
        }
    }
}
