using System.Text;
using System.Collections.Generic;
using System.IO;
using System;

namespace ArraySearch
{
    class Solution
    {
        static void Main(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            int queries = Int32.Parse(Console.ReadLine());
            for (int t = 0; t < queries; t++)
            {
                // Create graph of size n
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                Graph graph = new Graph(Int32.Parse(split[0]));
                int m = Int32.Parse(split[1]);
                // read and set edges
                for (int j = 0; j < m; j++)
                {
                    string[] Split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);

                    int u = Int32.Parse(Split[0]) - 1;
                    int v = Int32.Parse(Split[1]) - 1;

                    //add edge to graph 
                    graph.addEdge(u, v);
                }

                //find shortest reach from node s
                int startId = Int32.Parse(Console.ReadLine()) - 1;
                int[] distances = graph.shortestReach(startId);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < distances.Length; i++)
                {
                    if (i != startId)
                    {
                        sb.Append(distances[i]);
                        sb.Append(" ");
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }
    class Graph
    {
        Dictionary<int, LinkedList<int>> nodes = new Dictionary<int, LinkedList<int>>();
        int[] dist;
        int[] prev;
        int size;
        public Graph(int s)
        {
            for(int i = 0; i<s; i++)
            {
                nodes.Add(i, new LinkedList<int>());
            }
            dist = new int[s];
            prev = new int[s];
            size = s;
        }
        public void addEdge(int first, int second)
        {
            nodes[first].AddLast(second);
            nodes[second].AddLast(first);
        }

        public int[] shortestReach(int startId)
        {
            for (int i = 0; i < size; i++)
            {
                dist[i] = -1;
            }
            dist[startId] = 0;

            Queue<int> q = new Queue<int>();
            q.Enqueue(startId);

            while (q.Count != 0)
            {
                int u = q.Dequeue();
                foreach (int v in nodes[u])
                {
                    if (dist[v] == -1)
                    {
                        q.Enqueue(v);
                        dist[v] = dist[u] + 1;
                        prev[v] = u;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (dist[i] != -1)
                    dist[i] *= 6;
            }

            return dist;
        }
    }
}