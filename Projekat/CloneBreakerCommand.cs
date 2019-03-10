using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneBreakerCommand : Command
    {
        private Breaker breaker = new Breaker();
        private Breaker b = null;
        private string ID = string.Empty;

        public CloneBreakerCommand(string id)
        {
            breaker.mRID = id;
            b = new Breaker(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            breaker = (Breaker)b.Clone();
            breaker.mRID = ID;
            Singleton.Instance().Breakers.Add(breaker);
        }

        public override void UnExecute()
        {
            foreach (Breaker br in Singleton.Instance().Breakers)
            {
                if (br.mRID.Equals(breaker.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(br.substationID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (e.mRID.Equals(br.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().Breakers.Remove(br);
                    break;
                }
            }
        }
    }
}
