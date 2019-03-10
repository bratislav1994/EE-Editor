using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class AddSubCommand : Command
    {
        private Substation s = new Substation();
        public AddSubCommand(String alias, String desc, String name, float voltage)
        {
            s.aliasName = alias;
            s.description = desc;

            do
            {
                s.mRID = Guid.NewGuid().ToString().Substring(0, 8);
            }
            while (s.mRID[0] < 'a' || s.mRID[0] > 'z');
            
            s.name = name;
            s.x = -1;
            s.y = -1;

            s.connectivityNodes = new List<ConnectivityNode>();

            s.Bays = new List<Bay>();
            s.Equipments = new List<Equipment>();
            s.VoltageLevels = new List<VoltageLevel>();

            BaseVoltage bv = new BaseVoltage()
            {
                aliasName = "sub_-" + s.mRID + "_bv_" + s.aliasName,
                mRID = "sub_-" + s.mRID + "-_bv_-" + s.mRID,
                name = "sub_-" + s.mRID + "-_bv_-" + s.name,
                description = name = "sub_-" + s.mRID + "-_bv_-" + s.description,
                nominalVoltage = voltage,
                ConductingEquipment = new List<ConductingEquipment>()
            };

            VoltageLevel vl = new VoltageLevel()
            {
                aliasName = "sub_" + s.mRID + "_vl_" + s.aliasName,
                mRID = "sub_-" + s.mRID + "-_vl_-" + s.mRID,
                name = "sub_-" + s.mRID + "-_vl_-" + s.name,
                description = name = "sub_-" + s.mRID + "-_vl_-" + s.description,
                BaseVoltage = bv,
                Bays = new List<Bay>(),
                Equipments = new List<Equipment>()
            };
            
            s.VoltageLevels.Add(vl);
        }

        public override void Execute()
        {
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
