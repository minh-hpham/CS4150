using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class Program
    {
        static void Main(string[] args)
        {
            // number of cities
            int n = Int32.Parse(Console.ReadLine());

            var cities = new Dictionary<String, int>();

            for (int i = 0; i < n; i++)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // (Key,value) is (city_name, toll)
                cities.Add(split[0], Int32.Parse(split[1]));
            }
            // number of highway
            int h = Int32.Parse(Console.ReadLine());

            List<String> highways = new List<string>();
            for (int i = 0; i < h; i++)
            {
                // each string is in format: "city1 city2". Unique
                highways.Add(Console.ReadLine());
            }

            // number of trips
            int t = Int32.Parse(Console.ReadLine());


            for (int i = 0; i < t; i++)
            {
                // each string is in format: "city1 city2". Unique

                Console.WriteLine(minimumToll(Console.ReadLine()));
            }
            Console.Read();


        }

        /// Return the minimum toll to travel between 2 cities 
        ///Or NO if the path is unreachable
        private static bool minimumToll(string v)
        {
            throw new NotImplementedException();
        }
    }
}
