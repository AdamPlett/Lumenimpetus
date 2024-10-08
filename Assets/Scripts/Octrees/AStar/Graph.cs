using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Octrees
{
    public class Graph
    {
        public readonly Dictionary<OctreeNode, Node> nodes = new Dictionary<OctreeNode, Node>();
        public readonly HashSet<Edge> edges = new HashSet<Edge>();

        List<Node> pathList = new List<Node>();

        public int GetPathLength => pathList.Count;

        public OctreeNode GetPathNode(int index)
        {
            if (pathList == null) return null;

            if (index < 0 || index >= pathList.Count)
            {
                Debug.LogError($"Index out of bounds. Path length: {pathList.Count}, Index: {index}");
                return null;
            }
            return pathList[index].octreeNode;
        }

        const int maxIterations = 10000;
        public bool AStar(OctreeNode startNode, OctreeNode endNode)
        {
            pathList.Clear();
            Node start = FindNode(startNode);
            Node end = FindNode(endNode);

            if (start == null || end == null)
            {
                Debug.Log("Start or End node not found in the graph");
                return false;
            }

            SortedSet<Node> openSet = new SortedSet<Node>(new NodeComparer());
            HashSet<Node> closedSet = new HashSet<Node>();
            int iterationCount = 0;

            start.g = 0;
            start.h = Heuristic(start, end);
            start.f = start.g + start.h;
            start.from = null;
            openSet.Add(start);

            while(openSet.Count > 0)
            {
                if (++iterationCount > maxIterations)
                {
                    Debug.Log("A* exceeded maxium iterations.");
                    return false;
                }

                Node current = openSet.First();
                openSet.Remove(current);

                if (current.Equals(end))
                {
                    ReconstructPath(current);
                    return true;
                }

                closedSet.Add(current);

                foreach (Edge edge in current.edges)
                {
                    Node neighbor = Equals(edge.a, current) ? edge.b : edge.a;

                    if (closedSet.Contains(neighbor)) continue;

                    //float tentative_gScore = current.g + (current.octreeNode.bounds.center - neighbor.octreeNode.bounds.center).sqrMagnitude;
                    float tentative_gScore = current.g + Heuristic(current, neighbor);

                    if (tentative_gScore < neighbor.g || !openSet.Contains(neighbor))
                    {
                        neighbor.g = tentative_gScore;
                        neighbor.h = Heuristic(neighbor, end);
                        neighbor.f = neighbor.g + neighbor.h;
                        neighbor.from = current;

                        openSet.Add(neighbor);
                    }
                }
            }
            Debug.Log("No Path Found");
            return false;
        }

        void ReconstructPath(Node current)
        {
            while (current != null)
            {
                pathList.Add(current);
                current = current.from;
            }

            pathList.Reverse();
        }

        float Heuristic(Node a, Node b) => (a.octreeNode.bounds.center - b.octreeNode.bounds.center).sqrMagnitude; 

        //for our SortedSet openSet, makes the most efficient paths at the top of the set
        public class NodeComparer : IComparer<Node>
        {
            public int Compare(Node x, Node y)
            {
                if (x == null || y == null) return 0;

                int compare = x.f.CompareTo(y.f);

                if (compare == 0)
                {
                    return x.id.CompareTo(y.id);
                }

                return compare;
            }
        }


        public void AddNode(OctreeNode octreeNode)
        {
            if (!nodes.ContainsKey(octreeNode))
            {
                nodes.Add(octreeNode, new Node(octreeNode));
            }
        }

        public void AddEdge(OctreeNode a, OctreeNode b)
        {
            Node nodeA = FindNode(a);
            Node nodeB = FindNode(b);

            if (nodeA == null || nodeB == null) return;

            var edge = new Edge(nodeA, nodeB);

            if (edges.Add(edge))
            {
                nodeA.edges.Add(edge);
                nodeB.edges.Add(edge);
            }
        }

        public void DrawGraph()
        {
            Gizmos.color = Color.red;

            //draws every Edge in edges
            foreach (Edge edge in edges)
            {
                Gizmos.DrawLine(edge.a.octreeNode.bounds.center, edge.b.octreeNode.bounds.center);
            }

            //draws every Node in nodes
            foreach (var node in nodes.Values)
            {
                Gizmos.DrawWireSphere(node.octreeNode.bounds.center, 0.2f);
            }
        }

        //finds the Node related to the passed in OctreeNode in the "nodes" dictonary
        Node FindNode(OctreeNode octreeNode)
        {
            nodes.TryGetValue(octreeNode, out Node node);
            return node;
        }
    }
}
