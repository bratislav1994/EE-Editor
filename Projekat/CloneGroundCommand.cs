using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneGroundCommand : Command
    {
        private Ground ground = new Ground();
        private Ground g = null;
        private string ID = string.Empty;

        public CloneGroundCommand(string id)
        {
            ground.mRID = id;
            g = new Ground(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            ground = (Ground)g.Clone();
            ground.mRID = ID;
            Singleton.Instance().Ground.Add(ground);
        }

        public override void UnExecute()
        {
            foreach (Ground g in Singleton.Instance().Ground)
            {
                if (g.mRID.Equals(ground.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(g.substationID))
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
                    }

                    Singleton.Instance().Ground.Remove(g);
                    break;
                }
            }
        }
    }
}
