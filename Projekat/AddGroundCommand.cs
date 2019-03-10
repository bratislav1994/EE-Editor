using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddGroundCommand : Command
    {
        private Ground g = new Ground();

        public AddGroundCommand(String alias, String desc, String name, String id)
        {
            g.aliasName = alias;
            g.description = desc;
            g.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            g.name = name;
            g.substationID = id;
        }

        public override void Execute()
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

        public override void UnExecute()
        {
            foreach (Ground ground in Singleton.Instance().Ground)
            {
                if (ground.mRID.Equals(g.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (g.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (ground.mRID.Equals(e.mRID))
                                {
                                    s.VoltageLevels[0].Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().Ground.Remove(ground);
                    break;
                }
            }
        }
    }
}
