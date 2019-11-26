using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesConverter
{
    class PreparePallette
    {
        public static List<int> DividePalette(int k)
        {
            float p = 255 / (k-1);
            List<int> rsl = new List<int>();
            for (int i = 0; i < k; i++)
                rsl.Add((int)(i*p));
            return rsl;
        }
    }
}
