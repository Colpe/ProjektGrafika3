using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    public static class ColorExtenders
    {

        public static (float, float, float) Subtract(Color x, Color y)
        {
            return ((x.R - y.R), (x.G - y.G), (x.B - y.B));
        }
        public static Color Add(Color c, (float, float, float) error)
        {
            int r = c.R + (int)error.Item1 > 255 ? 255 : c.R + (int)error.Item1 < 0 ? 0 : c.R + (int)error.Item1;
            int g = c.G + (int)error.Item2 > 255 ? 255 : c.G + (int)error.Item2 < 0 ? 0 : c.G + (int)error.Item2;
            int b = c.B + (int)error.Item3 > 255 ? 255 : c.B + (int)error.Item3 < 0 ? 0 : c.B + (int)error.Item3;

            return Color.FromArgb(r, g, b);
        }
        class ColorComparer : Comparer<Color>
        {
            public override int Compare(Color x, Color y)
            {
                return (x.R << 16 + x.G << 8 + x.B).CompareTo(y.R << 16 + y.G << 8 + y.B);
            }
        };

        public static Comparer<Color> GetColorComparer()
        {
            return new ColorComparer();
        }
    }
}
