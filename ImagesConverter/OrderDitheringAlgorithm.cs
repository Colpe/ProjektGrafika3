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

        static public Bitmap OrderDithering(Color[,] pixels, int kr, int kg, int kb)
        {
            int nR = SelectN(kr);
            int nG = SelectN(kg);
            int nB = SelectN(kb);

            int[,] mR = GenerateArray(nR);
            int[,] mG = GenerateArray(nG);
            int[,] mB = GenerateArray(nB);

            (byte, byte, byte)[,] colors = new (byte, byte, byte)[pixels.GetLength(0), pixels.GetLength(1)];
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    for (int i = 0; i < nR; i++)
                    {
                        for (int j = 0; j < nR; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].R % (nR * nR) > mR[i, j])
                                    colors[x + i, y + j].Item1 = (byte)(pixels[x + i, y + j].R + 1);
                                else
                                    colors[x + i, y + j].Item1 = pixels[x + i, y + j].R;
                        }
                    }
                    for (int i = 0; i < nG; i++)
                    {
                        for (int j = 0; j < nG; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].G % (nG * nG) > mG[i, j])
                                    colors[x + i, y + j].Item2 = (byte)(pixels[x + i, y + j].G + 1);
                                else
                                    colors[x + i, y + j].Item2 = (byte)(pixels[x + i, y + j].G);
                        }
                    }
                    for (int i = 0; i < nB; i++)
                    {
                        for (int j = 0; j < nB; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].B % (nB * nB) > mB[i, j])
                                    colors[x + i, y + j].Item3 = (byte)(pixels[x + i, y + j].B + 1);
                                else
                                    colors[x + i, y + j].Item3 = (byte)(pixels[x + i, y + j].B);
                        }
                    }
                }
            }
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    for (int i = 0; i < nR; i++)
                    {
                        for (int j = 0; j < nR; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].R % (nR * nR) > mR[i, j])
                                    colors[x + i, y + j].Item1 = (byte)(pixels[x + i, y + j].R + 1);
                                else
                                    colors[x + i, y + j].Item1 = pixels[x + i, y + j].R;
                        }
                    }
                    for (int i = 0; i < nG; i++)
                    {
                        for (int j = 0; j < nG; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].G % (nG * nG) > mG[i, j])
                                    colors[x + i, y + j].Item2 = (byte)(pixels[x + i, y + j].G + 1);
                                else
                                    colors[x + i, y + j].Item2 = (byte)(pixels[x + i, y + j].G);
                        }
                    }
                    for (int i = 0; i < nB; i++)
                    {
                        for (int j = 0; j < nB; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].B % (nB * nB) > mB[i, j])
                                    colors[x + i, y + j].Item3 = (byte)(pixels[x + i, y + j].B + 1);
                                else
                                    colors[x + i, y + j].Item3 = (byte)(pixels[x + i, y + j].B);
                        }
                    }
                }
            }
            for (int x = 0; x < pixels.GetLength(0); x += nR)
            {
                for (int y = 0; y < pixels.GetLength(1); y += nR)
                {
                    for (int i = 0; i < nR; i++)
                    {
                        for (int j = 0; j < nR; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].R > mR[i, j])
                                    colors[x + i, y + j].Item1 = 255;// (byte)(pixels[x + i, y + j].R + 1);
                                else
                                    colors[x + i, y + j].Item1 = 0;// pixels[x + i, y + j].R;
                        }
                    }
                }
            }
            for (int x = 0; x < pixels.GetLength(0); x += nG)
            {
                for (int y = 0; y < pixels.GetLength(1); y += nG)
                {
                    for (int i = 0; i < nG; i++)
                    {
                        for (int j = 0; j < nG; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                            {
                                Color c=Appro
                                if (pixels[x + i, y + j].G > mG[i, j])
                                    colors[x + i, y + j].Item2 = 255;// (byte)(pixels[x + i, y + j].G + 1);
                                else
                                    colors[x + i, y + j].Item2 = 0;// (byte)(pixels[x + i, y + j].G);
                            }
                        }
                    }

                }
            }

            for (int x = 0; x < pixels.GetLength(0); x += nB)
            {
                for (int y = 0; y < pixels.GetLength(1); y += nB)
                {

                    for (int i = 0; i < nB; i++)
                    {
                        for (int j = 0; j < nB; j++)
                        {
                            if (x + i < pixels.GetLength(0) && y + j < pixels.GetLength(1))
                                if (pixels[x + i, y + j].B > mB[i, j])
                                    colors[x + i, y + j].Item3 = 255;// (byte)(pixels[x + i, y + j].B + 1);
                                else
                                    colors[x + i, y + j].Item3 = 0;// (byte)(pixels[x + i, y + j].B);
                        }
                    }
                }
            }
            Bitmap btm = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    btm.SetPixel(i, j, Color.FromArgb(colors[i, j].Item1, colors[i, j].Item2, colors[i, j].Item3));
                }
            }



            return btm;
        }

        private static int SelectN(int k)
        {
            List<int> n = new List<int>() { 2, 3, 4, 8, 16 };
            int i = 0;
            while (n[i] * n[i] * (k - 1) < 255)
                i++;
            return n[i];
        }

        private static int[,] GenerateArray(int n)
        {
            if (n == 9)
            {
                return new int[,]
                {
                    {1,7,4 },
                    {5,8,3 },
                    {6,2,9 }
                };
            }

            int M = (int)Math.Log(n, 2);
            int L = (int)Math.Log(n, 2);
            Console.WriteLine(M + " " + L);

            int xdim = 1 << M;
            int ydim = 1 << L;
            int[,] result = new int[n, n];
            for (int y = 0; y < ydim; ++y)
            {
                for (int x = 0; x < xdim; ++x)
                {
                    int v = 0, offset = 0, xmask = M, ymask = L;
                    int xc = x, yc = y ^ ((x << L) >> M);

                    for (int bit = 0; bit < M + L;)
                    {
                        v |= ((xc >> --xmask) & 1) << bit++;
                        for (offset += L; offset >= M; offset -= M)
                            v |= ((yc >> --ymask) & 1) << bit++;
                    }
                    result[y, x] = 255 * v / (n * n);
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(result[i, j] + "  ");
                }
                Console.WriteLine();
            }
            return result;
        }
    }
}
