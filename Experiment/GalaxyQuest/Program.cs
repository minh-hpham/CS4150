using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
            int d = Int32.Parse(split[0]);
            int k = Int32.Parse(split[1]);

            // Array contains all stars with x and y
            List<Star> pu = new List<Star>();

            for (int i = 0; i < k; i++)
            {
                string[] line = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                pu.Add(new Star(Int32.Parse(line[0]), Int32.Parse(line[1])));
            }

            Star find = findMajority(pu,d);
            if(find == null)
                Console.WriteLine("NO");
            else
            {
                int count = 0;
                foreach (Star s in pu)
                {
                    if (find.inGalaxy(d, s)) count++;
                }
                Console.WriteLine(count);
            }
            Console.Read();
        }

        public static List<Star[]> BreakIntoChunks(List<Star> array, int size)
        {
            int i = 0;
            var query = from s in array
                        let num = i++
                        group s by num / size into g
                        select g.ToArray();
            var results = query.ToList();

            return results;
        }


        public static Star findMajority(List<Star> A, int d)
        {
           
            if (A.Count == 0)
                return null;
            else if (A.Count == 1)
                return A.First();
            else
            {
                // Find A' and y. A' is chunks
                List<Star[]> chunks = BreakIntoChunks(A, 2);
                //y
                Star y = null;
                // A'
                List<Star> recursiveA = new List<Star>();
                for (int i = 0; i < chunks.Count; i++)
                {
                    Star[] arr = chunks.ElementAt(i);
                    if (arr.Length == 1)
                    {
                        y = arr[0];
                    }
                    else
                    {
                        if (arr[0].inGalaxy(d, arr[1]))
                        {
                            recursiveA.Add(arr[0]);
                        }
                    }
                }

                // A' now is chunks
                Star x = findMajority(recursiveA,d);
                if(x == null)
                {
                    if(A.Count % 2 > 0)
                    {
                        int count = 0;
                        foreach(Star s in A)
                        {
                            if (y.inGalaxy(d, s)) count++;
                        }
                        return count > (A.Count / 2) ? y : null;
                    }
                    return null;
                }
                else
                {
                    int count = 0;
                    foreach (Star s in A)
                    {
                        if (x.inGalaxy(d, s)) count++;
                    }
                    return count > (A.Count / 2) ? x : null;
                }
            }
 
        }
#if false
        public static int findMajority(Star[] A, int d)
        {
            Star y = null;
            if (A.Length == 0)
                return 0;
            else if (A.Length == 1)
                return 1;
            else
            {
                // Find A' and y. A' is chunks
                Star[][] chunks = BreakIntoChunks(A, 2);

                List<Star> list = new List<Star>();
                for (int i = 0; i < chunks.Length; i++)
                {

                    if (chunks[i].Length == 1)
                    {
                        y = chunks[i][0];
                    }
                    else
                    {
                        Star[] arr = chunks[i];
                        if (arr[0].inGalaxy(d, arr[1]))
                        {
                            list.Add(new Star[] { arr[0] });
                        }
                    }

                    list.Remove(chunks[i]);

                }

                // A' now is chunks
                int x = findMajority(chunks);
            }
            return 0;
        } 
#endif

#if false
        public static Star[][] BreakIntoChunks(Star[] array, int size)
        {
            int i = 0;
            var query = from s in array
                        let num = i++
                        group s by num / size into g
                        select g.ToArray();
            var results = query.ToArray();

            return results;
        } 
#endif

#if false
        public static Star[][] BreakIntoChunks(Star[] list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize must be greater than 0.");
            }
            int size = list.Length;
            Star[,] retVal = new Star[size / 2, chunkSize];

            for (int i = 0; i < size / 2; i++)
            {
                int count = list.Length > chunkSize ? chunkSize : list.Length;
                retVal[i] = (list.GetRange(0, count));
                list.RemoveRange(0, count);
            }

            return retVal;
        } 
#endif




    }
    class Star
    {
        public int x, y;

        public Star(int x_, int y_)
        {
            x = x_;
            y = y_;
        }

        public bool inGalaxy(int d, Star other)
        {
            long result = (long)((x - other.x) * (x - other.x) + (y - other.y) * (y - other.y));
            return result <= (long)(d * d);
        }

    }



}
