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
            double middleR = 0;
            double middleG = 0;
            double middleB = 0;

            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    middleR += pixels[i, j].R;
                    middleG += pixels[i, j].G;
                    middleB += pixels[i, j].B;

                }
            }

            middleR = (middleR / (pixels.GetLength(0)* pixels.GetLength(1)));
            middleG = (middleG / (pixels.GetLength(0)* pixels.GetLength(1)));
            middleB = (middleB / (pixels.GetLength(0)* pixels.GetLength(1)));


            middleR = (middleR / 255);
            middleG = (middleG / 255);
            middleB = (middleB / 255);

            Parallel.For(0, pixels.GetLength(0), i =>
                 Parallel.For(0, pixels.GetLength(1), j =>
                       rsl[i, j] = Color.FromArgb(
                           //   SetColor(pixels[i, j], RColors, GColors, BColors)
                           ((float)(pixels[i, j].R % (255 / (RColors.Count - 1)))) / ((float)255 / (RColors.Count - 1)) > middleR ? RColors.First(x => x >= pixels[i, j].R) : RColors.Last(x => x <= pixels[i, j].R),
                           ((float)(pixels[i, j].G % (255 / (GColors.Count - 1)))) / ((float)255 / (GColors.Count - 1)) > middleG ? GColors.First(x => x >= pixels[i, j].G) : GColors.Last(x => x <= pixels[i, j].G),
                           ((float)(pixels[i, j].B % (255 / (BColors.Count - 1)))) / ((float)255 / (BColors.Count - 1)) > middleB ? BColors.First(x => x >= pixels[i, j].B) : BColors.Last(x => x <= pixels[i, j].B)
                 )
                 ));


            Bitmap btm = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));
            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                    btm.SetPixel(i, j, rsl[i, j]);
            return btm;
        }

        private static Color SetColor(Color c, List<int> RColors, List<int> GColors, List<int> BColors)
        {
            int suma = (c.R + c.G + c.B + 150) / 3;
            return AproximateColor.Approximate(RColors, GColors, BColors, c);
        }
    }
}
