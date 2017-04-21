using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiderman_s_Workout
{
    class Program
    {
        //static public string[] updown;
        static void Main(string[] args)
        {
            int N = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                int total = 0;
                int m = Int32.Parse(Console.ReadLine());
                int[] line = new int[m];
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                for (int j = 0; j < m; j++)
                {
                    line[j] = Int32.Parse(split[j]);
                    total += line[j];

                }
                String[,] updown = new String[m,total+1];
                //String updown = null;
                int maxheight = minheight(line, 0, 0, ref updown);
                Console.WriteLine(updown);
               // Console.WriteLine(step(line, updown));
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
        private static string step(int[] array, string[] updown)
        {
            StringBuilder sb = new StringBuilder();
            int currentheight = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (updown[i].Equals("DOWN"))
                {
                    sb.Append('D');
                    currentheight -= array[i];
                }
                else if (updown[i].Equals("UP"))
                {
                    sb.Append('U');
                    currentheight += array[i];
                }
            }

            if (currentheight != 0)
                return "IMPOSSIBLE";
            return sb.ToString();
        }
        //private static int minheight(int[] array, int steps, int height, ref String[] updown)
        //{
        //    int up, down;
        //    if (steps == 0)
        //    {
        //        updown[steps] = "UP";
        //        return minheight(array, steps + 1, array[steps], ref updown);
        //    }
        //    else if (steps == array.Length - 1)
        //    {
        //        updown[steps] = "DOWN";
        //        return height;
        //    }
        //    else
        //    {
        //        up = minheight(array, steps + 1, height + array[steps], ref updown);
        //        if (height - array[steps] >= 0)
        //            down = minheight(array, steps + 1, height - array[steps], ref updown);
        //        else
        //            down = int.MaxValue;

        //        int compare;
        //        if (up <= down)
        //        {
        //            updown[steps] = "UP";
        //            compare = up;
        //        }
        //        else
        //        {
        //            updown[steps] = "DOWN";
        //            compare = down;
        //        }
        //        return Math.Max(height, compare);
        //    }
        //}
        private static int minheight(int[] array, int steps, int height, ref string[,] updown)
        {
            int up, down;
            
            if (steps == 0)
            {
                updown[steps,array[steps]] = "UP";
                return minheight(array, steps + 1, array[steps], ref updown);
            }
            else if (steps == array.Length - 1)
            {
                updown[steps, array[steps]] = "DOWN";
                return height;
            }
            else
            {
                if (height - array[steps] >= 0)
                {
                    up = minheight(array, steps + 1, height + array[steps], ref updown);
                    down = minheight(array, steps + 1, height - array[steps], ref updown);
                    if (up < down)
                        updown[steps, array[steps]] = "UP";
                    else updown[steps, array[steps]] = "DOWN";
                    return Math.Max(height, Math.Min(up, down));
                }   
                else
                {
                    updown[index] = "UP";
                    return Math.Max(height, minheight(array, steps + 1, height + array[steps], ref updown));
                }
            }
        }
    }
}
