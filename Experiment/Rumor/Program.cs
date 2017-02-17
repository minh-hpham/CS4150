using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rumor
{
    class Vertex
    {
        public String name;
        public LinkedList<Vertex> friends;
        public Vertex(String s)
        {
            name = s;
            friends = new LinkedList<Vertex>();
        }
    }
    class Program
    {


        static void Main(string[] args)
        {
            int n = Int32.Parse(Console.ReadLine());
            Dictionary<String, Vertex> vertices = new Dictionary<string, Vertex>();
            for (int i = 0; i < n; i++)
            {
                String name = Console.ReadLine();
                vertices.Add(name, new Vertex(name));
            }

            int f = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < f; i++)
            {
                string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                vertices[split[0]].friends.AddLast(vertices[split[1]]);
                vertices[split[1]].friends.AddLast(vertices[split[0]]);
            }
            int r = Int32.Parse(Console.ReadLine());
            String[] startRumor = new String[r];
            for (int i = 0; i < r; i++)
            {
                startRumor[i] = Console.ReadLine();
                Console.WriteLine(bfs(vertices, startRumor[i]));
            }

            Console.Read();
        }

        private static String bfs(Dictionary<string, Vertex> vertices, string s)
        {
            var dist = new Dictionary<Vertex, int>();
            var prev = new Dictionary<Vertex, Vertex>();


            foreach (Vertex u in vertices.Values)
            {
                dist[u] = 100000;
                prev[u] = null;
            }

            dist[vertices[s]] = 0;

            var Q = new Queue<Vertex>();
            Q.Enqueue(vertices[s]);

            while (!(Q.Count == 0))
            {
                Vertex u = Q.Dequeue();
   
                foreach(Vertex v in u.friends)
                {
                    if(dist[v] == 100000)
                    {
                        Q.Enqueue(v);
                        dist[v] = dist[u] + 1;
                        prev[v] = u;
                    }
                }
            }

       
            var sortResult = vertices.Values.OrderBy(a => dist[a]).ThenBy(a => a.name).ToList();
           
            StringBuilder sb = new StringBuilder();
            foreach (Vertex u in sortResult)
            {
                sb.Append(u.name);
                sb.Append(" ");
            }
                
            String result = sb.ToString();
            return result;
        }
    }
}
