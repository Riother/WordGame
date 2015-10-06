using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.ListTools
{
    public static class ListExtensionFunctions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
            Random rand = new Random(seed);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
