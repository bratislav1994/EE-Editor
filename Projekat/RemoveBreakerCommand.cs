using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveBreakerCommand : Command
    {
        private Breaker b = new Breaker();

        public RemoveBreakerCommand(string id)
        {
            b.mRID = id;
        }

        public override void Execute()
        {
            foreach (Breaker breaker in Singleton.Instance().Breakers)
            {
                if (breaker.mRID.Equals(b.mRID))
                {
                    b = breaker;

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.VoltageLevels[0].Equipments)
                        {
                            if (e.mRID.Equals(b.mRID))
                            {
                                s.VoltageLevels[0].Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().Breakers.Remove(breaker);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Breakers.Add(b);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(b.substationID))
                {
                    s.VoltageLevels[0].Equipments.Add(b);
                    break;
                }
            }
        }
    }
}
