using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApplication1
{
    class star
    {
        public long x;
        public long y;
    }
    class galaxy
    {
        static long diameter;
        static long starCount;
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] arr = s.Split(new char[0]); // split with whitespace 
            long.TryParse(arr[0], out diameter);
            long.TryParse(arr[1], out starCount);
            star[] stars = new star[starCount];
            for (long line = 0; line < starCount; line++) // read in all words 
            {
                string input = Console.ReadLine();
                string[] coordinates = input.Split(new char[0]);
                star aStar = new star();
                long.TryParse(coordinates[0], out aStar.x);
                long.TryParse(coordinates[1], out aStar.y);
                stars[line] = aStar;
            }
            string result = findMajorityWrapper(stars);
            Console.WriteLine(result);
            Console.Read();
        }
        private static string findMajorityWrapper(star[] stars)
        {
            int majoirtyNumber = 0;
            star majorityGalaxy = findMajority(stars);
            if (majorityGalaxy == null)
            {
                return "NO";
            }
            else
            {
                foreach (star astar in stars)
                {
                    if (distance(majorityGalaxy, astar))
                    {
                        majoirtyNumber++;
                    }
                }
                if (majoirtyNumber > starCount / 2)
                {
                    return "" + majoirtyNumber;
                }
                else
                {
                    return "NO";
                }
            }
        }
        static public star findMajority(star[] A)
        {
            List<star> B = new List<star>();
            int count = A.Count();
            star leftover = new star();
            star majority;
            if (count == 0)
            {
                return null;
            }
            else if (count == 1)
            {
                return A[0];
            }
            else
            {
                // find matching pairs put in List B 
                int i = 0;
                while (i + 1 < count)
                {
                    if (distance(A[i], A[i + 1]))
                    {
                        B.Add(A[i]);
                    }
                    i += 2;
                }
                // leftover single element put in List B 
                if (count % 2 != 0)
                {
                    leftover = A[count - 1];
                }
                majority = findMajority(B.ToArray());
                // if x is NO 
                if (majority == null)
                {
                    // if |A| is odd 
                    if (count % 2 != 0)
                    {