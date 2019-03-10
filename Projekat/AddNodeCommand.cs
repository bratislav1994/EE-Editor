using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddNodeCommand : Command
    {
        private ConnectivityNode cn = new ConnectivityNode();
        private Substation sub = new Substation();

        public AddNodeCommand(String alias, String desc, String name, Substation s, float nominal)
        {
            cn.aliasName = alias;
            cn.description = desc;
            do
            {
                cn.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            }
            while (cn.mRID[0] < 'a' || cn.mRID[0] > 'z');
            cn.name = name;
            cn.x = -1;
            cn.y = -1;
            //cn.ConnectivityNodeContainer = s;
            
            cn.m_BaseVoltage = new BaseVoltage();
            cn.m_BaseVoltage.aliasName = "node_" + cn.mRID + "b:" + cn.aliasName;
            cn.m_BaseVoltage.ConductingEquipment = new List<ConductingEquipment>();
            cn.m_BaseVoltage.description = "node_" + cn.mRID + "b:" + cn.description;
            cn.m_BaseVoltage.mRID = Guid.NewGuid().ToString().Substring(0, 10);
            cn.m_BaseVoltage.name = "node_" + cn.mRID + "b:" + cn.name;
            cn.m_BaseVoltage.nominalVoltage = nominal;
            sub = s;
            //s.connectivityNodes.Add(cn);
        }

        public override void Execute()
        {
            Singleton.Instance().Nodes.Add(cn);
            sub.connectivityNodes.Add(cn);
            //MainWindow.nodes.Add(cn);
        }

        public override void UnExecute()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(cn.mRID))
                {
                    //foreach (Substation s in Singleton.Instance().Substations)
                    //{
                    //    foreach (ConnectivityNode conNode in s.connectivityNodes)
                    //    {
                    //        if (conNode.mRID.Equals(cn.mRID))
                    //        {

                    //        }
                    //    }
                    //}

                    sub.connectivityNodes.Remove(Singleton.Instance().Nodes[i]);
                    Singleton.Instance().Nodes.Remove(cn);
                    
                    break;
                }
            }
        }
    }
}
