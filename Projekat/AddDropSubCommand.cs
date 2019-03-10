using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Projekat
{
    public class AddDropSubCommand : Command
    {
        private Substation s = new Substation();
        private double x2 = 0;//nove vrednosti
        private double y2 = 0;
        private double x = 0;//da pamtim stare vrednosti
        private double y = 0;

        public AddDropSubCommand(Substation draggedSubstation, double x, double y)
        {
            s = draggedSubstation;
            this.x = x;
            this.y = y;
            this.x2 = s.x;
            this.y2 = s.y;
        }

        public override void Execute()
        {
            s.x = x2;
            s.y = y2;
        }

        public override void UnExecute()
        {
            s.x = x;
            s.y = y;

            foreach (Substation sub in Singleton.Instance().Substations)
            {
                if (!s.mRID.Equals(sub.mRID))
                {
                    if (TakenCanvas.Check(s.x, s.y, sub.x, sub.y, 180))
                    {
                        sub.x = -1;
                        sub.y = -1;
                    }
                }
            }
        }
    }
}
