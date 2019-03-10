using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveDisconnectorCommand : Command
    {
        private Disconnector d = new Disconnector();

        public RemoveDisconnectorCommand(string id)
        {
            d.mRID = id;
        }

        public override void Execute()
        {
            foreach (Disconnector dis in Singleton.Instance().Disconnectors)
            {
                if (dis.mRID.Equals(d.mRID))
                {
                    d = dis;

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.VoltageLevels[0].Equipments)
                        {
                            if (e.mRID.Equals(d.mRID))
                            {
                                s.VoltageLevels[0].Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().Disconnectors.Remove(dis);
                    break;
                }
            }
        }

        public override void UnExecute()
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
    }
}
