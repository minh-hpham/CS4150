using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            Console.ReadLine();
            HashSet<String> solution = new HashSet<string>();
            HashSet<String> rejected = new HashSet<string>();
            while ((line = Console.ReadLine()) != null)
            {
                String sort = alphabetized(line);
                if (solution.Contains(sort))
                {
                    solution.Remove(sort);
                    rejected.Add(sort);
                }
                else if (rejected.Contains(sort) == false)
                {
                    solution.Add(sort);
                }
            }
            Console.WriteLine(solution.Count);
        }

        private static string alphabetized(string s)
        {
            // 1.
            // Convert to char array.
            char[] a = s.ToCharArray();

            // 2.
            // Sort letters.
            Array.Sort(a);

            // 3.
            // Return modified string.
            return new string(a);
        }
    }
}
