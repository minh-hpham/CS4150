// Written by Joe Zachary for CS 4150, January 2016

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Experiment
{
    /// <summary>
    /// Provides a timing demo.
    /// </summary>
    public class Timer
    {
        public const int WORD_LENGTH = 5;
        /// <summary>
        /// The number of repetitions used in versions 2 and 3
        /// </summary>
        public const int REPTS = 1000;

        /// <summary>
        /// The minimum duration of a timing eperiment (in msecs) in versions 4 and 5
        /// </summary>
        public const int DURATION = 1000;

        /// <summary>
        /// The size of the array in versions 1 through 5
        /// </summary>
        public const int SIZE = 1023;

        private static Random random = new Random();

        /// <summary>
        /// Drives the timing demo.
        /// </summary>
        public static void Main()
        {
            // Let's look at precise the Stopwatch is
            Console.WriteLine("Is high resolution: " + Stopwatch.IsHighResolution);
            Console.WriteLine("Ticks per second: " + Stopwatch.Frequency);

            // Now do an experiment.
            RunExperiment();
            Console.Read();
        }
        /// <summary>
        /// Runs different experiments depending on the value of approach.
        /// </summary>
        public static void RunExperiment()
        {

            // Report the average time required to do a linear search for various sizes
            // of arrays.
            int size = 32;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 17; i++)
            {
                size = size * 2;
                double currentTime = timeMrAnaga(size - 1);
                Console.Write((size - 1) + "\t" + currentTime.ToString("G3"));
                if (i > 0)
                {
                    Console.WriteLine("   \t" + (currentTime / previousTime).ToString("G3"));
                }
                else
                {
                    Console.WriteLine();
                }
                previousTime = currentTime;

            }
        }
        private static int mrAnaga(HashSet<String> list)
        {
            HashSet<String> solution = new HashSet<string>();
            HashSet<String> rejected = new HashSet<string>();

            foreach (String s in list)
            {
                String sort = alphabetized(s);
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
            return solution.Count;
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

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static double msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }



        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static HashSet<String> makeList(int size)
        {
            HashSet<String> list = new HashSet<string>();
            for (int i = 0; i < size; i *= 2)
            {
                list.Add(RandomString(WORD_LENGTH));
            }
            return list;
        }

        /// <summary>
        /// Returns the average time required to find an element in an array of
        /// the given size using binary search, assuming that the element actually
        /// appears in the array.  Uses a different timer than Search5.
        /// </summary>
        public static double timeMrAnaga(int size)
        {
            // Construct a sorted array
            HashSet<String>[] data = new HashSet<String>[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = makeList(i);
            }
            // Get the process
            Process p = Process.GetCurrentProcess();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        // HashSet<String> list = makeList(d);
                        mrAnaga(data[d]);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / size;

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        //HashSet<String> list = makeList(d);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;
        }


    }
}




