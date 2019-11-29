using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class AverageDithering
    {

        static public Bitmap averageDitheringAlgorithm(Color[,] pixels, List<int> RColors, List<int> GColors, List<int> BColors)
        {

            Color[,] rsl = new Color[pixels.GetLength(0), pixels.GetLength(1)];
            Parallel.For(0, pixels.GetLength(0), i =>
             Parallel.For(0, pixels.GetLength(1), j =>
                   rsl[i, j] = SetColor(pixels[i, j], RColors, GColors, BColors)
             ));
            Bitmap btm = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));
            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                    btm.SetPixel(i, j, rsl[i, j]);
            return btm;
        }

        private static Color SetColor(Color c, List<int> RColors, List<int> GColors, List<int> BColors)
        {
            int suma = (c.R + c.G + c.B) / 3;
            return AproximateColor.Approximate(RColors, GColors, BColors, c);
        }
    }
}
