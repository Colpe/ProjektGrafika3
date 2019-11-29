using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class OrderDitheringAlgorithm
    {

        static public Bitmap OrderDithering(Color[,] pixels, int kr, int kg, int kb, List<int> RColors, List<int> GColors, List<int> BColors)
        {
            int nR = SelectN(kr);
            int nG = SelectN(kg);
            int nB = SelectN(kb);

            int[,] mR = GenerateMatrix2(nR);
            int[,] mG = GenerateMatrix2(nG);
            int[,] mB = GenerateMatrix2(nB);

            Bitmap btm = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));
            (byte, byte, byte)[,] colors = new (byte, byte, byte)[pixels.GetLength(0), pixels.GetLength(1)];

            for (int x = 0; x < pixels.GetLength(0); x += 1)
            {
                for (int y = 0; y < pixels.GetLength(1); y += 1)
                {
                    int r, g, b;
                    {
                        int col = pixels[x, y].R;//AproximateColor.Approximate(RColors, GColors, BColors, pixels[x + i, y + j]);
                        double re = ((double)(kr - 1) * nR * nR / 255) * pixels[x, y].R % (nR * nR);
                        if (re <= mR[x % nR, y % nR])
                            r = RColors.Last(k => k <= col);// (byte)(pixels[x + i, y + j].R + 1);
                        else
                            r = RColors.First(k => k >= col);// pixels[x + i, y + j].R;
                    }
                    {
                        int col = pixels[x, y].G;//AproximateColor.Approximate(RColors, GColors, BColors, pixels[x + i, y + j]);
                        double re = ((double)(kg - 1) * nG * nG / 255) * pixels[x, y].G % (nG * nG);
                        if (re <= mG[x % nG, y % nG])
                            g = GColors.Last(k => k <= col);// (byte)(pixels[x + i, y + j].R + 1);
                        else
                            g = GColors.First(k => k >= col);// pixels[x + i, y + j].R;
                    }
                    {
                        int col = pixels[x, y].B;
                        double re = ((double)(kb - 1) * nB * nB / 255) * pixels[x, y].B % (nB * nB);
                        if (re <= mB[x % nB, y % nB])
                            b = BColors.Last(k => k <= col);// (byte)(pixels[x + i, y + j].R + 1);
                        else
                            b = BColors.First(k => k >= col);// pixels[x + i, y + j].R;
                    }
                    btm.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return btm;
        }

        private static int SelectN(int k)
        {
            List<int> n = new List<int>() { 2, 3, 4, 6, 8, 12, 16 };
            int i = 0;
            while (n[i] * n[i] * (k - 1) < 255)
                i++;
            return n[i];
            //   return 8;
        }


        static int[,] GenerateMatrix2(int n)
        {
            int[,] D2 = new int[,]
            {
                { 0,2},
                {3,1 }
            };

            int[,] D3 = new int[,]
            {
                { 6,8,4},
                {1,0,3 },
                {5,2,7 }
            };
            if (n == 2)
                return D2;
            if (n == 3)
                return D3;

            int[,] D2n = new int[n, n];
            int[,] Dn = GenerateMatrix2(n / 2);

            for (int i = 0; i < n / 2; i++)
                for (int j = 0; j < n / 2; j++)
                    D2n[i, j] = 4 * Dn[i, j];
            for (int i = 0; i < n / 2; i++)
                for (int j = n / 2; j < n; j++)
                    D2n[i, j] = 4 * Dn[i, j % (n / 2)] + 2;

            for (int i = n / 2; i < n; i++)
                for (int j = 0; j < n / 2; j++)
                    D2n[i, j] = 4 * Dn[i % (n / 2), j] + 3;

            for (int i = n / 2; i < n; i++)
                for (int j = n / 2; j < n; j++)
                    D2n[i, j] = 4 * Dn[i % (n / 2), j % (n / 2)] + 1;


            return D2n;
        }

    }
}
