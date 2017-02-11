using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class Vertex
    {
        public String name;
        public int pre, post;
        public bool visited;
        public long toll;
        public LinkedList<Vertex> edges;
        public Vertex(String s, long t)
        {
            name = s;
            toll = t;
            pre = 0;
            post = 0;
            edges = new LinkedList<Vertex>();
        }
    }


    class Program
    {

        public static int clock = 1;
        public static List<Vertex> vertices = new List<Vertex>();
        static void Main(string[] args)
        {
            // number of cities
            int n = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // add new vertex with name is split[0] and toll is split[1]
                vertices.Add(new Vertex(split[0], long.Parse(split[1])));
            }
            // number of highway
            int h = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < h; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // add edge to vertex with name = split[0]
                vertices.Find(v => v.name.Equals(split[0])).edges.AddLast(vertices.Find(v => v.name.Equals(split[1])));

            }

            dfs(vertices.First());

            var order = from vertex in vertices
                          orderby vertex.post descending
                          select vertex;


            // number of trips
            int t = Int32.Parse(Console.ReadLine());


            List<String> print = new List<string>();
            for (int i = 0; i < t; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);

                String start = split[0];
                String end = split[1];

                int startIndex = vertices.FindIndex(vertex => vertex.name.Equals(start));
                int endIndex = vertices.FindIndex(vertex => vertex.name.Equals(end));

                if (startIndex == endIndex) print.Add(0.ToString());
                else if (startIndex > endIndex) print.Add("NO");
                else
                {
                    long toll = 0;

                    while (startIndex < endIndex)
                    {
                        if (vertices.ElementAt(startIndex).edges.Count == 0)
                        {
                            toll = 0;
                            break;
                        }
                        else
                        {
                            startIndex += 1;
                            toll += vertices.ElementAt(startIndex).toll;
                        }

                    }
                    if (toll == 0) print.Add("NO");
                    else print.Add(toll.ToString());
                }

            }

            foreach (String s in print)
                Console.WriteLine(s);
            Console.Read();

        }
       

        // set prevalue for vertice
        private static void previsit(Vertex v)
        {
            v.pre = clock++;
        }
        // set postvalue for vertice
        private static void postvisit(Vertex v)
        {
            v.post = clock++;
        }

        // depth first search to find post value of each vertex
        private static void dfs(Vertex G)
        {
            foreach (Vertex v in vertices)
            {
                v.visited = false;
            }
            foreach (Vertex v in vertices)
            {
                if (v.visited == false)
                {
                    explore(G, v);
                }
            }

            vertices.OrderBy(i => i.post);
        }

        private static void explore(Vertex G, Vertex v)
        {
            v.visited = true;
            previsit(v);

            foreach (Vertex u in v.edges)
            {
                if (u.visited == false) // if(!visted[u]
                    explore(G, u);
            }
            postvisit(v);
        }
    }
}
