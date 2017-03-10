using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);

                if (split[0].Equals("gcd"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    Console.WriteLine(gcd(n, m));
                }
                else if (split[0].Equals("exp"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    long N = long.Parse(split[3]);
                    Console.WriteLine(exp(n, m, N));
                }
                else if (split[0].Equals("inverse"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    Console.WriteLine(inverse(n, m));
                }
                else if (split[0].Equals("isprime"))
                {
                    long n = long.Parse(split[1]);
                    if(isprime(n) == false)
                        Console.WriteLine("no");
                    else Console.WriteLine("yes");

                }
                else if (split[0].Equals("key"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    Console.WriteLine(key(n, m));
                }
            }
            Console.Read();
        }

        private static bool key(long p, long q)
        {
            long N = p * q;
            long phi = (p - 1) * (q - 1);
            return false;
        }

        private static bool isprime(long n)
        {
            int[] test = new int[3] { 2, 3, 5 };
            for (int j = 0; j < 3; j++)
            {
                int a = test[j];
                if (exp(a, n - 1, n) != 1)
                    return false;
            }
            return true;
        }
        private static long[] ee(long a, long b)
        {
            if (b == 0)
                return new long[3] { 1, 0, a };
            else
            {
                long[] r = ee(b, a % b);
                return new long[3] { r[1], r[0] - (a / b) * r[1], r[2] };
            }
        }
        private static string inverse(long a, long N)
        {
            long[] r = ee(a, N);
            if (r[2] == 1)
                return (r[0] % N).ToString();
            else
                return "none";
        }

        private static long exp(long n, long m, long N)
        {
            if (m == 0)
                return 1;
            else
            {
                long z = exp(n, m / 2, N);
                if (m % 2 == 0)
                    return (z * z) % N;
                else
                    return (n * z * z) % N;
            }
        }

        private static long gcd(long n, long m)
        {
            while (m > 0)
            {
                long nModm = n % m;
                n = m;
                m = nModm;
            }
            return n;
        }
    }
}
