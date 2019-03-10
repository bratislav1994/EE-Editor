using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneConsumerCommand : Command
    {
        private EnergyConsumer enerCon = new EnergyConsumer();
        private EnergyConsumer ec = null;
        private string ID = string.Empty;

        public CloneConsumerCommand(string id)
        {
            enerCon.mRID = id;
            ec = new EnergyConsumer(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            enerCon = (EnergyConsumer)ec.Clone();
            enerCon.mRID = ID;
            Singleton.Instance().Consumers.Add(enerCon);
        }

        public override void UnExecute()
        {
            foreach (EnergyConsumer ec in Singleton.Instance().Consumers)
            {
                if (ec.mRID.Equals(enerCon.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(ec.substationID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (e.mRID.Equals(ec.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }
                    
                    Singleton.Instance().Consumers.Remove(ec);
                    break;
                }
            }
        }
    }
}
