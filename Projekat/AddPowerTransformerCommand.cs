using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddPowerTransformerCommand : Command
    {
        private PowerTransformer pt = new PowerTransformer();

        public AddPowerTransformerCommand(String alias, String desc, String name, float high, String subID)
        {
            pt.aliasName = alias;
            pt.description = desc;
            pt.highSideMinOperatingU = high;
            pt.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            pt.name = name;
            pt.substationID = subID;
            pt.PowerTransformerEnd = new List<PowerTransformerEnd>();
        }

        public override void Execute()
        {
            Singleton.Instance().PowerTransformers.Add(pt);

            foreach (Substation sub in Singleton.Instance().Substations)
            {
                if (sub.mRID.Equals(pt.substationID))
                {
                    sub.Equipments.Add(pt);
                    break;
                }
            }
        }

        public override void UnExecute()
        {
            foreach (PowerTransformer transformer in Singleton.Instance().PowerTransformers)
            {
                if (transformer.mRID.Equals(pt.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (pt.substationID.Equals(s.mRID))
                        {
                            foreach (Equipment e in s.Equipments)
                            {
                                if (pt.mRID.Equals(e.mRID))
                                {
                                    s.Equipments.Remove(e);
                                    break;
                                }
                            }
                        }
                    }

                    Singleton.Instance().PowerTransformers.Remove(transformer);
                    break;
                }

            }
        }
    }
}
