using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiderman_s_Workout
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                int m = Int32.Parse(Console.ReadLine());
                int[] line = new int[m];
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                for (int j = 0; j < m; j++)
                {
                    line[j] = Int32.Parse(split[j]);

                }
                Console.WriteLine(minheight(line, 0, 0));
            }
            Console.Read();
        }
        private static int iterate(int[] array, int index)
        {
            int size = (int)Math.Pow(2, array.Length) - 1;
            int[] cache = new int[size];
            int count = 0;
            int arrayIndex = 0;
            int power = (int)Math.Pow(2, arrayIndex);
            for (int i = 0; i < cache.Length; i++)
            {
                count++;
                if (power < count)
                {
                    arrayIndex++;
                    power = (int)Math.Pow(2, arrayIndex);
                    count = 1;
                }
                if (i == 0)
                    cache[i] = array[i];
                else if (i % 2 == 0)
                {
                    int value = cache[(i - 2) % 2] - array[arrayIndex];
                    if (value >= 0)
                        cache[i] = value;
                    else
                        cache[i] = int.MaxValue;
                }
                else
                {
                    cache[i] = cache[(i - 1) % 2] + array[arrayIndex];
                }
            }


            return cache[0];
        }
        private static int minheight(int[] array, int steps, int height)
        {
            int up, down;
            if (steps == 0)
            {
                return minheight(array, steps + 1, array[steps]);
            }
            else if (steps == array.Length - 1)
            {
                return height;
            }
            else
            {
                up = minheight(array, steps + 1, height + array[steps]);
                if (height - array[steps] >= 0)
                    down = minheight(array, steps + 1, height - array[steps]);
                else
                    down = int.MaxValue;
                return Math.Max(height, Math.Min(up, down));
            }
        }

    }
}
