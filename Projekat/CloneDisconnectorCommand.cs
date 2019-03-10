using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneDisconnectorCommand : Command
    {
        private Disconnector disconnector = new Disconnector();
        private Disconnector d = null;
        private string ID = string.Empty;

        public CloneDisconnectorCommand(string id)
        {
            disconnector.mRID = id;
            d = new Disconnector(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            disconnector = (Disconnector)d.Clone();
            disconnector.mRID = ID;
            Singleton.Instance().Disconnectors.Add(disconnector);
        }

        public override void UnExecute()
        {
            foreach (Disconnector dis in Singleton.Instance().Disconnectors)
            {
                if (dis.mRID.Equals(disconnector.mRID))
                {
                    foreach (Substation s in Singleton.Instance().Substations)
                    {
                        if (s.mRID.Equals(dis.substationID))
                        {
                            foreach (Equipment e in s.VoltageLevels[0].Equipments)
                            {
                                if (e.mRID.Equals(dis.mRID))
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
