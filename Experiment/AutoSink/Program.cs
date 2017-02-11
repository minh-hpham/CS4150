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
        public static Dictionary<String, Vertex> vertices = new Dictionary<String, Vertex>();
        static void Main(string[] args)
        {
            // number of cities
            int n = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // add new vertex with name is split[0] and toll is split[1]
                vertices.Add(split[0],new Vertex(split[0], long.Parse(split[1])));
            }
            // number of highway
            int h = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < h; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // add edge to vertex with name = split[0]
                vertices[split[0]].edges.AddLast(vertices[split[1]]);

            }

            dfs(vertices.First().Value);

            // descending order by post number

            var items = from pair in vertices
                        orderby pair.Value.post descending
                        select pair;
            //var order = vertices.Values.OrderByDescending(a => a.post).ToList();
            var order = vertices.OrderByDescending(a => a.Value.post).ToList();
            // number of trips
            int t = Int32.Parse(Console.ReadLine());

            List<String> print = new List<string>();

            for (int i = 0; i < t; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);

                String start = split[0];
                String end = split[1];
                // if departure and destination is the same return 0 for toll
                if (start.Equals(end)) print.Add(0.ToString());

                else
                {
                    int startIndex = order[start].;
                    int endIndex = order.FindIndex(vertex => vertex.name.Equals(end));

                    // can't find path because no vertex from start to end
                    if (startIndex > endIndex) print.Add("NO");
                    else
                    {
                        var list = new Dictionary<Vertex, long>();

                        list.Add(order.ElementAt(startIndex), 0);
                        // a list contains vertex and total travel toll at that vertex 
                        // similar to the pseudocode you send me
                        for (int j = startIndex + 1; j <= endIndex; j++)
                        {
                            // set every total toll to INF except departure point (0)
                            list.Add(order.ElementAt(j), int.MaxValue);
                        }
                        // go through every vertex from departure to destination according to the 
                        // post number 
                        foreach (Vertex v in list.Keys.ToList())
                        { 
                            // go to each child vertex
                            foreach (Vertex u in v.edges.ToList())
                            {
                                long newToll = list[v] + u.toll;
                                list[u] = newToll < list[u] ? newToll : list[u];
                            }
                        }
                        long toll = list.Last().Value;
                        if (toll > 0) print.Add(toll.ToString());
                        else print.Add("NO");

                    }
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
            foreach (Vertex v in vertices.Values)
            {
                v.visited = false;
            }
            foreach (Vertex v in vertices.Values)
            {
                if (v.visited == false)
                {
                    explore(G, v);
                }
            }



        }

        private static void explore(Vertex G, Vertex v)
        {
            v.visited = true;
            previsit(v);

            foreach (Vertex u in v.edges)
            {
                if (u.visited == false) 
                    explore(G, u);
            }
            postvisit(v);
        }
    }
}
