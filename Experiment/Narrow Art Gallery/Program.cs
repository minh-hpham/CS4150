using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrow_Art_Gallery
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
            int N = Int32.Parse(split[0]);
            int k = Int32.Parse(split[1]);
            int[,] values = new int[N, 2];

            for (int i = 0; i < N; i++)
            {
                string[] line = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                values[i, 0] = Int32.Parse(line[0]);
                values[i, 1] = Int32.Parse(line[1]);
            }
            Console.ReadLine();
            Console.WriteLine(maxValue(0, k, values));
            Console.Read();
        }

        private static int maxValue(int r, int k, int[,] values)
        {
            return maxValueRecursive(r, -1, k, values, new Dictionary<String,int>());
        }
        /*
          Requires the existence of an N x 2 values array. 
          Requires that k <= N - r.
          Requires that 0 ≤ r ≤ N
          Requires that uncloseableRoom = -1, 0, or 1

          Returns the maximum value that can be obtained from rows r through N-1
          when k rooms are closed, subject to this restriction: 

           If uncloseableRoom is 0, the room in column 0 of row r cannot be closed;
           If uncloseableRoom is 1, the room in column 1 of row r cannot be closed;
           If uncloseableRoom is -1, either room of row i may be closed if desired.
        */
        private static int maxValueRecursive(int r, int uncloseableRoom, int k, int[,] values, Dictionary<String, int>cache )
        {
            int maxIndex = values.GetLength(0) - 1;
            int result;
            String key = "" + r + uncloseableRoom + k;
            if (cache.TryGetValue(key, out result))
            {
                return result;
            }
            if (k < values.GetLength(0) - r && r <= maxIndex)
            {
                int max = 0;
                if (uncloseableRoom == -1)
                {
                    int atboth = values[r, 0] + values[r, 1] + maxValueRecursive(r + 1, -1, k, values, cache);
                    int at0 = values[r, 0] + maxValueRecursive(r + 1, 0, k - 1, values, cache);
                    int at1 = values[r, 1] + maxValueRecursive(r + 1, 1, k - 1, values, cache);
                    max = Math.Max(Math.Max(at0, at1), atboth);
                    cache[key] = max;
                    return max ;
                }

                else if (uncloseableRoom == 0)
                {
                    int atboth = values[r, 0] + values[r, 1] + maxValueRecursive(r + 1, -1, k, values, cache);
                    max = Math.Max(values[r, 0] + maxValueRecursive(r + 1, 0, k - 1, values, cache), atboth);
                    cache[key] = max;
                    return max;
                }
                
                else
                {
                    int atboth = values[r, 0] + values[r, 1] + maxValueRecursive(r + 1, -1, k, values, cache);
                    max= Math.Max(values[r, 1] + maxValueRecursive(r + 1, 1, k - 1, values, cache), atboth);
                    cache[key] = max;
                    return max;
                }

            }
            
            
            else if (k == values.GetLength(0) - r && r <= maxIndex)
            {
                int max = 0;
                if (uncloseableRoom == -1)
                {
                    int at0 = values[r, 0] + maxValueRecursive(r + 1, 0, k - 1, values, cache);
                    int at1 = values[r, 1] + maxValueRecursive(r + 1, 1, k - 1, values, cache);
                    max = Math.Max(at0, at1);
                    cache[key] = max;
                    return max;
                }

                else if (uncloseableRoom == 0)
                {
                    max = values[r, 0] + maxValueRecursive(r + 1, 0, k - 1, values, cache);
                    cache[key] = max;
                    return max;
                }
                   
                else
                {
                    max = values[r, 1] + maxValueRecursive(r + 1, 1, k - 1, values, cache);
                    cache[key] = max;
                    return max;
                }
                    
            }

            else if (k == 0 && r <= maxIndex)
            {
                int sum = 0;
                for (int i = r; i <= maxIndex; i++)
                {
                    sum += values[r, 0] + values[r, 1];
                }
                cache[key] = sum;
                return sum;
            }
            else //(r > maxIndex)
            {
                cache[key] = 0;
                return 0;
            }
                
        }
    }
}
