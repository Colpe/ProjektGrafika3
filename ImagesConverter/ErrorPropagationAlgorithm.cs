using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class ErrorPropagationAlgorithm
    {
        //static float[,] FloydAndSteinbergFilter = new float[,] {
        //    {0, 0, 0},
        //    {0,0,7/16},
        //    {3/16,5/16,1/16},
        //    };
        static void SubstractMatrixFloydAnsSteinberg(Color[,] pixels, int i, int j, (float, float, float) Error)
        {
            if (i + 1 < pixels.GetLength(0))
            {
                pixels[i + 1, j] = ColorExtenders.Add(pixels[i + 1, j], (Error.Item1 * 7 / 16, Error.Item3 * 7 / 16, Error.Item3 * 7 / 16));
                if (j + 1 < pixels.GetLength(1))
                    pixels[i + 1, j + 1] = ColorExtenders.Add(pixels[i + 1, j + 1], (Error.Item1 * 1 / 16, Error.Item3 * 1 / 16, Error.Item3 * 1 / 16));
            }
            if (j + 1 < pixels.GetLength(1))
            {
                if (i > 0)
                    pixels[i - 1, j + 1] = ColorExtenders.Add(pixels[i - 1, j + 1], (Error.Item1 * 3 / 16, Error.Item3 * 3 / 16, Error.Item3 * 3 / 16));
                pixels[i, j + 1] = ColorExtenders.Add(pixels[i, j + 1], (Error.Item1 * 5 / 16, Error.Item3 * 5 / 16, Error.Item3 * 5 / 16));
            }
        }

        public static Bitmap ErrorPropagation(List<int> RColors, List<int> GColors, List<int> BColors, Color[,] realColors)
        {
            Color[,] pixels = (Color[,])realColors.Clone();

            Bitmap rsl = new Bitmap(realColors.GetLength(0), realColors.GetLength(1));
            Color K;
            (float, float, float) Error = (0, 0, 0);

            for (int i = 0; i < realColors.GetLength(0); i++)
                for (int j = 0; j < realColors.GetLength(1); j++)
                {
                    K = AproximateColor.Approximate(RColors, GColors, BColors, pixels[i, j]);
                    Error = ColorExtenders.Subtract(pixels[i, j], K);
                    rsl.SetPixel(i, j, K);
                    SubstractMatrixFloydAnsSteinberg(pixels, i, j, Error);
                }

            return rsl;

        }
    }
}
