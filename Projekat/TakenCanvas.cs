using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public abstract class TakenCanvas
    {
        public static bool Check(double countWidth, double countTop, double x, double y, int size)
        {
            return (countWidth + size >= x &&
                    countWidth + size <= x + size &&
                    countTop >= y &&
                    countTop <= y + size) ||

                   (countWidth + size >= x &&
                    countWidth + size <= x + size &&
                    countTop + size >= y &&
                    countTop + size <= y + size) ||

                   (countWidth >= x &&
                    countWidth <= x + size &&
                    countTop + size >= y &&
                    countTop + size <= y + size) ||

                   (countWidth >= x &&
                    countWidth <= x + size &&
                    countTop >= y &&
                    countTop <= y + size);
        }
    }
}
