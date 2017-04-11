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
            for(int i = 0; i < N; i ++)
            {
                int m = Int32.Parse(Console.ReadLine());
                int[] line = new int[m];
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                for (int j = 0; j < m; j++)
                {
                    line[j] = Int32.Parse(split[j]);
                    Console.WriteLine(updown(line));
                }
            }
            Console.Read();
        }

        private static string updown(int[] line)
        {
            throw new NotImplementedException();
        }
    }
}
