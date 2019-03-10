using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveLoadBreakerCommand : Command
    {
        private LoadBreakSwitch lbs = new LoadBreakSwitch();

        public RemoveLoadBreakerCommand(string id)
        {
            lbs.mRID = id;
        }

        public override void Execute()
        {
            foreach (LoadBreakSwitch loadBr in Singleton.Instance().LoadBreakers)
            {
                if (loadBr.mRID.Equals(lbs.mRID))
                {
                    lbs = loadBr;

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.VoltageLevels[0].Equipments)
                        {
                            if (e.mRID.Equals(lbs.mRID))
                            {
                                s.VoltageLevels[0].Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().LoadBreakers.Remove(loadBr);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().LoadBreakers.Add(lbs);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(lbs.substationID))
                {
                    s.VoltageLevels[0].Equipments.Add(lbs);
                    break;
                }
            }
        }
    }
}
