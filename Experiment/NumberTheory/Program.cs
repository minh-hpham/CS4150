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
            List<string> print = new List<string>();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = line.Split(new char[] { ' ' }, StringSplitOptions.None);

                if (split[0].Equals("gcd"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                   
                   Console.WriteLine(gcd(n, m).ToString());

                }
                else if (split[0].Equals("exp"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    long N = long.Parse(split[3]);
                    Console.WriteLine(exp(n, m, N).ToString());

                }
                else if (split[0].Equals("inverse"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    long inv = inverse(n, m);
                    string s = inv != -1 ? inv.ToString() : "none";
                    Console.WriteLine(s);

                }
                else if (split[0].Equals("isprime"))
                {
                    long n = long.Parse(split[1]);
                    string s = isprime(n) == true ? "yes" : "no";
                    Console.WriteLine(s);

                }
                else if (split[0].Equals("key"))
                {
                    long n = long.Parse(split[1]);
                    long m = long.Parse(split[2]);
                    Console.WriteLine(key(n, m));

                }
   

            }
            //foreach (string s in print)
            //    Console.WriteLine(s);
            Console.Read();
        }

        private static HashSet<int> getPrime(long x)
        {
            HashSet<int> list = new HashSet<int>();
            if (x % 2 == 0)
                list.Add(2);
            long sqrt = (long)Math.Ceiling(Math.Sqrt(x));
            for (int i = 3; i < sqrt; i += 2)
            {
                if (x % i == 0)
                    list.Add(i);
            }
            return list;
        }
        private static string key(long p, long q)
        {
            StringBuilder sb = new StringBuilder();
            long N = p * q;
            sb.Append(N);
            sb.Append(" ");
            long phi = (p - 1) * (q - 1);
            HashSet<int> primes = getPrime(phi);
            int e = 1;
            if (primes.Contains(2) == false)
            {
                if (gcd(2, phi) == 1) e = 2;
            }

            else
            {
                while (true)
                {
                    e += 2;
                    if (primes.Contains(e) == false)
                    {
                        if (gcd(e, phi) == 1) break;
                    }
                }
            }
            sb.Append(e);
            sb.Append(" ");
            long P = inverse(e, phi);
            sb.Append(P);

            return sb.ToString();
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
        private static long inverse(long a, long N)
        {
            long[] r = ee(a, N);
            if (r[2] == 1)
                return r[0] % N;
            else
                return -1;
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
