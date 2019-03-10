using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneLoadBreakerCommand : Command
    {
        private LoadBreakSwitch loadBr = new LoadBreakSwitch();
        private LoadBreakSwitch lbs = null;
        private string ID = string.Empty;

        public CloneLoadBreakerCommand(string id)
        {
            loadBr.mRID = id;
            lbs = new LoadBreakSwitch(id);
            ID = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public override void Execute()
        {
            loadBr = (LoadBreakSwitch)lbs.Clone();
            loadBr.mRID = ID;
            Singleton.Instance().LoadBreakers.Add(loadBr);
        }

        public override void UnExecute()
        {
            foreach (LoadBreakSwitch br in Singleton.Instance().LoadBreakers)
            {
                if (br.mRID.Equals(loadBr.mRID))
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

                    Singleton.Instance().LoadBreakers.Remove(br);
                    break;
                }
            }
        }
    }
}
