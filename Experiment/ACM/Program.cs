using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] split = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
            int n = Int32.Parse(split[0]);
            int k = Int32.Parse(split[1]);

            String line;
            HashSet<BinarySearchTree> solution = new HashSet<BinarySearchTree>();
            HashSet<String> rejected = new HashSet<string>();
            for(int a = 0; a<n;a++) 
            {
                line = Console.ReadLine();
                string[] arr = line.Split(new char[] { ' ' }, StringSplitOptions.None);
                BinarySearchTree b = new BinarySearchTree();
                for(int i = 0; i<k; i++)
                {
                    b.add(Int32.Parse(arr[i]));
                }

                if(solution.Count == 0)
                {
                    solution.Add(b);
                }
                else
                {
                    bool contain = false;
                    foreach (BinarySearchTree t in solution)
                    {
                        if (equalTrees(b.root, t.root))
                        {
                            contain = true;
                            break;
                        }
                    }

                    if (contain == false) solution.Add(b); 
                }

            }
            Console.WriteLine(solution.Count);
        }

        public static bool equalTrees(Node lhs, Node rhs)
        {
            if (lhs == null && rhs == null)
            {
                return true;
            }
            else if ((lhs == null && rhs != null) || (lhs != null && rhs == null))
                return false;
            else
                return equalTrees(lhs.left, rhs.left) && equalTrees(lhs.right, rhs.right);
        }
        



    }
    class Node
    {
        public int data;
        public Node left, right;

        public Node(int d)
        {
            data = d;
            left = null;
            right = null;
        }

        public void add(ref Node node, int data)
        {
            if (node == null)
            {
                node = new Node(data);
            }
            else if (node.data > data)
            {
                add(ref node.left, data);
            }
            else if(node.data <data)
                add(ref node.right, data);
        }

    }

    class BinarySearchTree
    {
        public Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void add(int d)
        {
            if (root == null) root = new Node(d);
            else root.add(ref root, d);
        }



    }

}
