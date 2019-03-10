using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class ClonePowerTransformerCommand : Command
    {
        private PowerTransformer transformer = new PowerTransformer();
        private PowerTransformer pt = null;
        private string ID = string.Empty;

        public ClonePowerTransformerCommand(string id)
        {
            transformer.mRID = id;
            pt = new PowerTransformer(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            transformer = (PowerTransformer)pt.Clone();
            transformer.mRID = ID;
            Singleton.Instance().PowerTransformers.Add(transformer);
        }

        public override void UnExecute()
        {
            foreach (PowerTransformer powerT in Singleton.Instance().PowerTransformers)
            {
                if (powerT.mRID.Equals(transformer.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(powerT.substationID))
                        {
                            s.Equipments.Remove((Equipment)powerT);
                            break;
                        }
                    }

                    Singleton.Instance().PowerTransformers.Remove(powerT);
                    break;
                }
            }
        }
    }
}
