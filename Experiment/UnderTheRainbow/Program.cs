using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Int32.Parse(Console.ReadLine());
            int[] dist = new int[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                dist[i] = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine(DynamicPenalty(dist));
            Console.Read();
        }
        static int DynamicPenalty(int[] dist)
        {
            return DynamicPenalty(dist, 0, new int[dist.Length]);
        }
        static int DynamicPenalty(int[] dist, int n, int[] cache)
        {
            if (n == (dist.Length - 1))
                return 0;
            if (cache[n] != 0)
            {
                return cache[n];  
            }
            int minPenalty = int.MaxValue;
            for (int k = dist.Length-1; k > n; k--)
            {
                int penalty = (400 - (dist[k]-dist[n]))* (400 - (dist[k] - dist[n])) + DynamicPenalty(dist, k, cache);
                minPenalty = Math.Min(penalty, minPenalty);
            }
            cache[n] = minPenalty;
            return minPenalty;
        }
    }

}
