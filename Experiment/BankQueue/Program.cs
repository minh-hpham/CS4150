using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
            int N = int.Parse(split[0]);
            int T = int.Parse(split[1]);

            List<int> cash = new List<int>();
            List<int> time = new List<int>();

            for (int i = 0; i < N; i++)
            {
                string[] line = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                cash.Add(int.Parse(line[0]));
                time.Add(int.Parse(line[1]));
            }
            int sum = 0;
            int maxTime = T;//> time.Max() ? time.Max() : T;
            int index = -1;

            for (int t = maxTime; t >= 0; t--)
            {
                int max = 0;
                for (int j = 0; j < time.Count; j++)
                {
                    if (time[j] >= t && max < cash[j])
                    {
                        max = cash[j];
                        index = j;
                    }
                }
                if (max > 0)
                {
                    cash.RemoveAt(index);
                    time.RemoveAt(index);
                }
                sum += max;
            }

            Console.WriteLine(sum);
            Console.Read();
        }
    }
}
