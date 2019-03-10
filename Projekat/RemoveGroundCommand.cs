using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemoveGroundCommand : Command
    {
        private Ground g = new Ground();

        public RemoveGroundCommand(string id)
        {
            g.mRID = id;
        }

        public override void Execute()
        {
            foreach (Ground ground in Singleton.Instance().Ground)
            {
                if (ground.mRID.Equals(g.mRID))
                {
                    g = ground;

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.VoltageLevels[0].Equipments)
                        {
                            if (e.mRID.Equals(g.mRID))
                            {
                                s.VoltageLevels[0].Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().Ground.Remove(ground);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().Ground.Add(g);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(g.substationID))
                {
                    s.VoltageLevels[0].Equipments.Add(g);
                    break;
                }
            }
        }
    }
}
