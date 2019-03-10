using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddDisconnectorCommand : Command
    {
        private Disconnector d = new Disconnector();

        public AddDisconnectorCommand(String alias, String desc, String name, bool normal, bool open, float ratedCurrent, bool retained, string subID)
        {
            d.aliasName = alias;
            d.description = desc;
            d.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            d.name = name;
            d.normalOpen = normal;
            d.open = open;
            d.ratedCurrent = ratedCurrent;
            d.retained = retained;
            d.substationID = subID;
        }

        public override void Execute()
        {
            Singleton.Instance().Disconnectors.Add(d);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(d.substationID))
                {
                    s.VoltageLevels[0].Equipments.Add(d);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            foreach (Disconnector dis in Singleton.Instance().Disconnectors)
            {
                if (dis.mRID.Equals(d.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (d.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (dis.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().Disconnectors.Remove(dis);
                    break;
                }
            }
        }
    }
}
