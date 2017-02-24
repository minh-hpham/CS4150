import java.util.Collections;
import java.util.Comparator;

import java.util.LinkedList;
import java.util.PriorityQueue;

public class GetShorty {

	public static void main(String[] args) {
		Kattio io = new Kattio(System.in, System.out);

		while (io.hasMoreTokens()) {
			int n = io.getInt();
			int m = io.getInt();

			if (n == 0 && m == 0)
				break;
			// Create graph dijkstra
			Dijkstra graph = new Dijkstra(n);
			for (int i = 0; i < m; i++) {
				int from = io.getInt();
				int to = io.getInt();
				float factor = (float) io.getDouble();
				// add edge to graph
				graph.addEdge(from, to, factor);
			}
			// print out the final factor			
			io.println(String.format("%.4f", graph.shortestReach(0)));
		}

		io.close();
	}

}

class Dijkstra {
	LinkedList<edge>[] nodes;
	float[] dist;
	int[] prev;
	int size;

	public Dijkstra(int size) {
		this.size = size;
		nodes = new LinkedList[size];
		dist = new float[size];
		prev = new int[size];

		// initialize every element in the array
		for (int i = 0; i < size; i++) {
			nodes[i] = new LinkedList<edge>();
			dist[i] = 0;
			prev[i] = -1;
		}
	}

	public void addEdge(int first, int second, float f) {
		nodes[first].add(new edge(second, f));
		nodes[second].add(new edge(first, f));
	}

	Comparator<edge> edgeCompare = new Comparator<edge>() {

		@Override
		public int compare(edge left, edge right) {
			return Float.compare(left.factor, right.factor);
		}
	};

	public float shortestReach(int startId) {
		/*
		 * for(int i in nodes.Keys) { dist[i] = 1; prev[i] = -1; }
		 */
		dist[startId] = 1;

		PriorityQueue<edge> q = new PriorityQueue<edge>(10, Collections.reverseOrder(edgeCompare));

		q.add(new edge(startId, 1));

		while (q.isEmpty() == false) {
			edge u = q.remove();

			for (edge v : nodes[u.node]) {
				if (dist[v.node] < dist[u.node] * v.factor) {
					dist[v.node] = dist[u.node] * v.factor;
					prev[v.node] = u.node;
					if (q.contains(v)) {
						q.remove(v);
						q.add(new edge(v.node, dist[v.node]));
					} else
						q.add(new edge(v.node, dist[v.node]));
				}
			}
		}

		return dist[size - 1];
	}

}

class edge {
	int node;
	float factor;

	public edge(int t, float f) {
		node = t;
		factor = f;
	}
}