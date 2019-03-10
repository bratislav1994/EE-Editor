using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CloneNodeCommand : Command
    {
        private ConnectivityNode cn = new ConnectivityNode();
        private ConnectivityNode cn2 = null;
        private string ID = string.Empty;

        public CloneNodeCommand(String id)
        {
            cn.mRID = id;
            cn2 = new ConnectivityNode(id);
            do
            {
                ID = Guid.NewGuid().ToString().Substring(0, 8);
            }
            while (ID[0] < 'a' || ID[0] > 'z');
        }

        public override void Execute()
        {
            cn = (ConnectivityNode)cn2.Clone();
            cn.mRID = ID;
            Singleton.Instance().Nodes.Add(cn);
        }

        public override void UnExecute()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(cn.mRID))
                {
                    for (int j = 0; j < Singleton.Instance().Substations.Count; j++)
                    {
                        for (int k = 0; k < Singleton.Instance().Substations[j].connectivityNodes.Count; k++)
                        {
                            if (Singleton.Instance().Substations[j].connectivityNodes[k].mRID.Equals(Singleton.Instance().Nodes[i].mRID))
                            {
                                Singleton.Instance().Substations[j].connectivityNodes.Remove(cn);
                                break;
                            }
                        }
                    }

                    Singleton.Instance().Nodes.Remove(Singleton.Instance().Nodes[i]);

                    break;
                }
            }
        }
    }
}
