using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveConsumerCommand : Command
    {
        private EnergyConsumer ec = new EnergyConsumer();

        public RemoveConsumerCommand(string id)
        {
            ec.mRID = id;
        }

        public override void Execute()
        {
            foreach (EnergyConsumer l in Singleton.Instance().Consumers)
            {
                if (l.mRID.Equals(ec.mRID))
                {
                    ec = l;

                    foreach (Substation s in Singleton.Instance().Substations)
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

                    Singleton.Instance().Consumers.Remove(l);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Consumers.Add(ec);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(ec.substationID))
                {
                    s.VoltageLevels[0].Equipments.Add(ec);
                    break;
                }
            }
        }
    }
}
