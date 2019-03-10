using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddConsumerCommand : Command
    {
        private EnergyConsumer ec = new EnergyConsumer();

        public AddConsumerCommand(string alias, string desc, string name, int customerCount, bool grounded, float p, float pfixed, float pfixedPct, float q, float qfixed, float qfixedPct, string subID)
        {
            ec.aliasName = alias;
            ec.customerCount = customerCount;
            ec.description = desc;
            ec.grounded = grounded;
            ec.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            ec.name = name;
            ec.p = p;
            ec.pfixed = pfixed;
            ec.pfixedPct = pfixedPct;
            ec.q = q;
            ec.qfixed = qfixed;
            ec.qfixedPct = qfixedPct;
            ec.substationID = subID;
        }

        public override void Execute()
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

        public override void UnExecute()
        {
            foreach (EnergyConsumer enerCon in Singleton.Instance().Consumers)
            {
                if (enerCon.mRID.Equals(ec.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (enerCon.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (enerCon.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().Consumers.Remove(enerCon);
                    break;
                }
            }
        }
    }
}
