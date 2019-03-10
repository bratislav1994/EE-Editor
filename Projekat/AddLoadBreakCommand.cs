using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddLoadBreakCommand : Command
    {
        private LoadBreakSwitch loadBreaker = new LoadBreakSwitch();
        private string substationID = string.Empty;
        private float baseVoltage = 0;
       
        public AddLoadBreakCommand(String alias, String desc, String name, bool normal, bool open, float ratedCurrent, bool retained, String subID, float baseVoltage)
        {
            loadBreaker.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            loadBreaker.aliasName = alias;
            loadBreaker.description = desc;
            loadBreaker.name = name;
            loadBreaker.normalOpen = normal;
            loadBreaker.open = open;
            loadBreaker.ratedCurrent = ratedCurrent;
            loadBreaker.retained = retained;
            substationID = subID;
            loadBreaker.substationID = subID;
            this.baseVoltage = baseVoltage;
        }

        public override void Execute()
        {
            Singleton.Instance().LoadBreakers.Add(loadBreaker);

            foreach (Substation sub in Singleton.Instance().Substations)
            {
                if (sub.mRID.Equals(loadBreaker.substationID))
                {
                    sub.VoltageLevels[0].Equipments.Add(loadBreaker);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            foreach (LoadBreakSwitch breaker in Singleton.Instance().LoadBreakers)
            {
                if (loadBreaker.mRID.Equals(breaker.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (breaker.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (breaker.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().LoadBreakers.Remove(breaker);
                    break;
                }
            }
        }
    }
}
