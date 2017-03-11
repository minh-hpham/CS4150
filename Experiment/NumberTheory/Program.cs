using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
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
                    BigInteger inv = inverse(n, m);
                    string s;
                    if (inv == -1) s = "none";
                    else if (inv < 0)
                    {
                        inv += m;
                        s = inv.ToString();
                    }
                    else s = inv.ToString();
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
       
            int e = 2;
            if (phi % e == 0)
            {
                e = 1;
                while (true)
                {
                    e += 2;
                    if (phi % e != 0)
                    {
                        if (gcd(e, phi) == 1) break;
                    }
                }
            }
            sb.Append(e);
            sb.Append(" ");
            BigInteger P = inverse(e, phi);
            if (P < 0) P += phi;
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
        private static BigInteger[] ee(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return new BigInteger[3] { 1, 0, a };
            else
            {
                
                BigInteger[] r = ee(b, a % b);
                BigInteger x = r[0];
                BigInteger y = r[1];
                BigInteger d = r[2];
                return new BigInteger[3] {y, x-(a/b)*y,d };
            }
        }
        private static BigInteger inverse(BigInteger a, BigInteger N)
        {
            BigInteger[] r = ee(a, N);
            if (r[2] == 1)
                return r[0] % N;
            else
                return -1;
        }

        private static BigInteger exp(long x, long y, long N)
        {
            if (y == 0)
                return 1;
            else
            {
                BigInteger z = exp(x, y / 2, N);

                if (y % 2 == 0)
                    return (z * z) % N;
                else
                    return (x * z * z) % N;
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
