using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneSubCommand : Command
    {
        private Substation s = new Substation();
        private Substation s2 = null;
        private string ID = string.Empty;

        public CloneSubCommand(String id)
        {
            s.mRID = id;
            s2 = new Substation(id);
            do
            {
                ID = Guid.NewGuid().ToString().Substring(0, 8);
            }
            while (ID[0] < 'a' || ID[0] > 'z');
        }

        public override void Execute()
        {
            s = (Substation)s2.Clone();
            s.mRID = ID;
            Singleton.Instance().Substations.Add(s);
        }

        public override void UnExecute()
        {
            for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
            {
                if (Singleton.Instance().Substations[i].mRID.Equals(s.mRID))
                {
                    Singleton.Instance().Substations.Remove(Singleton.Instance().Substations[i]);
                    break;
                }

            }
        }
    }
}
