using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddBreakerCommand : Command
    {
        private Breaker breaker = new Breaker();
        private string substationID = string.Empty;
        private float baseVoltage = 0;

        public AddBreakerCommand(String alias, String desc, String name, float inTransitTime, bool normal, bool open, float ratedCurrent, bool retained, String subID, float baseVoltage)
        {
            breaker.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            breaker.aliasName = alias;
            breaker.description = desc;
            breaker.name = name;
            breaker.inTransitTime = inTransitTime;
            breaker.normalOpen = normal;
            breaker.open = open;
            breaker.ratedCurrent = ratedCurrent;
            breaker.retained = retained;
            substationID = subID;
            breaker.substationID = subID;
            this.baseVoltage = baseVoltage;
        }

        public override void Execute()
        {
            Singleton.Instance().Breakers.Add(breaker);

            foreach (Substation sub in Singleton.Instance().Substations)
            {
                if (sub.mRID.Equals(breaker.substationID))
                {
                    sub.VoltageLevels[0].Equipments.Add(breaker);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            foreach (Breaker b in Singleton.Instance().Breakers)
            {
                if (b.mRID.Equals(breaker.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (breaker.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (b.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().Breakers.Remove(b);
                    break;
                }
            }
        }
    }
}
