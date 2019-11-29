using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class PopularityAlgorithm
    {
        static public Bitmap Popularity(Color[,] pixels, int k)

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DirectBitmap dbtm = new DirectBitmap(pixels.GetLength(0), pixels.GetLength(1));
            Color[] palette = GetPalette(pixels, k);
            Func<Color, Color, double> GetEuclidianDistance = (c1, c2) =>
              {
                  return (c1.R - c2.R) * (c1.R - c2.R) + (c1.G - c2.G) * (c1.G - c2.G) + (c1.B - c2.B) * (c1.B - c2.B);
              };
            //   var elapsedMs = watch.ElapsedMilliseconds;
            //  Console.WriteLine("Initialization:" + elapsedMs);
            //  Color[,] colors = new Color[pixels.GetLength(0), pixels.GetLength(1)];
            Parallel.For(0, pixels.GetLength(0), (i) =>
            {
                Parallel.For(0, pixels.GetLength(1), (j) =>
                {
                    Color best = palette.First();
                    double d = double.MaxValue;
                    for (int kk = 0; kk < palette.Length; kk++)
                        if (d > GetEuclidianDistance(pixels[i, j], palette[kk]))
                        {
                            best = palette[kk];
                            d = GetEuclidianDistance(pixels[i, j], palette[kk]);
                        }
                    dbtm.SetPixel(i, j, best);
                    //  colors[i, j] = best;
                });
            });
            //    Console.WriteLine("time");
            //   elapsedMs = watch.ElapsedMilliseconds;
            //  Console.WriteLine("Parallel part:" + elapsedMs);

            //for (int i = 0; i < pixels.GetLength(0); i++)
            //    for (int j = 0; j < pixels.GetLength(1); j++)
            //        btm.SetPixel(i, j, colors[i, j]);
            //   elapsedMs = watch.ElapsedMilliseconds;
            //   watch.Stop();
            //   Console.WriteLine("btm assignment:" + elapsedMs);
            return dbtm.Bitmap;
        }

        static Color[] GetPalette(Color[,] pixels, int k)
        {
            Dictionary<Color, int> dict = new Dictionary<Color, int>();
            foreach (var v in pixels)
            {
                if (dict.TryGetValue(v, out int value))
                    dict[v] += 1;
                else
                    dict.Add(v, 1);
            }
            ;
            return dict.OrderBy(x => x.Value).Take(k).Select(x => x.Key).ToArray();
        }

    }
}
