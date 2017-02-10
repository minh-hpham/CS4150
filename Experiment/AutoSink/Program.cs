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
        public Vertex(String s)
        {
            name = s;
            pre = 0;
            post = 0;
        }
    }

    class Tuple
    {
        public Vertex start, end;
        public Tuple(Vertex s, Vertex e)
        {
            start = s;
            end = e;
        }
        
    }

    class Program
    {

        public static int clock = 0;
        public static List<Tuple> edges = new List<Tuple>();
        public static Dictionary<Vertex, int> vertices = new Dictionary<Vertex, int>();
        static void Main(string[] args)
        {
            // number of cities
            int n = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                // (Key,value) is (city_name, toll)
                vertices.Add(new Vertex(split[0]), Int32.Parse(split[1]));
            }
            // number of highway
            int h = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < h; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                edges.Add(new Tuple(new Vertex(split[0]), new Vertex(split[1])));
            }

            // number of trips
            int t = Int32.Parse(Console.ReadLine());

            List<Vertex> order = dfs(vertices.Keys.First());

            for (int i = 0; i < t; i++)
            {
                // each string is in format: "city1 city2". Unique
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);

                String start = split[0];
                String end = split[1];

                int startIndex = order.FindIndex(vertex => vertex.name.Equals(start));
                int endIndex = order.FindIndex(vertex => vertex.name.Equals(end));

                if (startIndex == endIndex) Console.WriteLine(0);
                else if (startIndex > endIndex) Console.WriteLine("NO");

                else
                {
                    int toll = 0;

                    while (startIndex < endIndex)
                    {
                        int tollOfVertex;
                        vertices.TryGetValue(order.ElementAt(startIndex++), out tollOfVertex);
                        toll += tollOfVertex;
                    }

                    Console.WriteLine(toll);
                }
                Console.Read();
            }
            


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

        // get edges starting from v
        private static List<Tuple> getEdgesFrom(Vertex v)
        {
            List<Tuple> result = new List<Tuple>();
            foreach(Tuple t in edges)
            {
                if (t.start.Equals(v))
                    result.Add(t);
            }
            
            return result;
        }
        // depth first search to find post value of each vertex
        private static List<Vertex> dfs(Vertex G)
        {
            foreach(Vertex v in vertices.Keys )
            {
                v.visited = false;
            }
            foreach(Vertex v in vertices.Keys)
            {
                if(v.visited == false)
                {
                    explore(G, v);
                }
            }

           List<Vertex> result = (List<Vertex>)vertices.Keys.OrderBy(i => i.post);
            return result;
        }

        private static void explore(Vertex G, Vertex v)
        {
            v.visited = true;
            previsit(v);

            List<Tuple> edgesfromv = getEdgesFrom(v);
            foreach(Tuple t in edgesfromv)
            {
                Vertex u = t.end;
                if (u.visited == false) // if(!visted[u]
                    explore(G, u);
            }
            postvisit(v);
        }
    }
}
