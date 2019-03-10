using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class RemovePowerTransformerCommand : Command
    {
        private PowerTransformer pt = new PowerTransformer();

        public RemovePowerTransformerCommand(string id)
        {
            pt.mRID = id;
        }

        public override void Execute()
        {
            foreach (PowerTransformer l in Singleton.Instance().PowerTransformers)
            {
                if (l.mRID.Equals(pt.mRID))
                {
                    pt = l;
                    Singleton.Instance().PowerTransformers.Remove(l);

                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        foreach (Equipment e in s.Equipments)
                        {
                            if (e.mRID.Equals(pt.mRID))
                            {
                                s.Equipments.Remove(e);
                                break;
                            }
                        }
                    }

                    break;
                }
            }
        }

        public override void UnExecute()
        {
            Singleton.Instance().PowerTransformers.Add(pt);

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(pt.substationID))
                {
                    s.Equipments.Add(pt);
                    break;
                }
            }
        }
    }
}
