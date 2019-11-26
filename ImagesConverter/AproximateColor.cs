using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class AproximateColor
    {
        public static Color Approximate(List<int> RColors, List<int> GColors, List<int> BColors, Color realColor)
        {
            int dif = int.MaxValue;

            int R = 0;
            RColors.ForEach(x =>
            {
                if (dif > Math.Abs(x - realColor.R))
                {
                    R = x;
                    dif = Math.Abs(x - realColor.R);
                }
            }
                );
            int G = 0;
            dif = int.MaxValue;
            RColors.ForEach(x =>
            {
                if (dif > Math.Abs(x - realColor.G))
                {
                    G = x;
                    dif = Math.Abs(x - realColor.G);
                }
            }
    );
            int B = 0;
            dif = int.MaxValue;
            RColors.ForEach(x =>
            {
                if (dif > Math.Abs(x - realColor.B))
                {
                    B = x;
                    dif = Math.Abs(x - realColor.B);
                }
            }
    );
            return Color.FromArgb(R, G, B);

        }

    }
}
