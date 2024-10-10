using System.Collections.Generic;
using UnityEngine;

namespace Octrees
{
    public class Octree
    {
        public OctreeNode root;
        public Bounds bounds;
        public Graph graph;

        List<OctreeNode> emptyLeaves = new List<OctreeNode>();

        //constructor
        public Octree(GameObject[] worldObjects, float minNodeSize, Graph graph)
        {
            this.graph = graph;
            CalculateBounds(worldObjects);
            CreateTree(worldObjects, minNodeSize);

            GetEmptyLeaves(root);
            GetEdges();
            Debug.Log(graph.edges.Count);
        }
        //creates the tree by initailizing the root and then calling the recursive divide function
       void CreateTree(GameObject[] worldObjects, float minNodeSize)
        {
            root = new OctreeNode(bounds, minNodeSize);

            foreach (var obj in worldObjects)
            {
                root.Divide(obj);
            }
        }

        public OctreeNode FindClosestNode(Vector3 pos) => FindClosestNode(root, pos);

        public OctreeNode FindClosestNode(OctreeNode node, Vector3 pos)
        {
            OctreeNode found = null;
            for (int i = 0; i < node.children.Length; i++)
            {
                if (node.children[i].bounds.Contains(pos))
                {
                    if (node.children[i].IsLeaf)
                    {
                        found = node.children[i];
                        break;
                    }
                    found = FindClosestNode(node.children[i], pos);
                }
            }
            return found;
        }

        void GetEdges()
        {
            foreach (OctreeNode leaf in emptyLeaves)
            {
                foreach(OctreeNode otherLeaf in emptyLeaves)
                {
                    if (leaf.bounds.Intersects(otherLeaf.bounds))
                    {
                        graph.AddEdge(leaf, otherLeaf);
                    }
                }
            }
        }

        void GetEmptyLeaves(OctreeNode node)
        {
            if (node.IsLeaf && node.objects.Count == 0)
            {
                emptyLeaves.Add(node);
                graph.AddNode(node);
                return;
            }

            if (node.children == null) return;

            //recurisve call
            foreach (OctreeNode child in node.children)
            {
                GetEmptyLeaves(child);
            }

            //add all edges when coming out of the recurision
            for (int i = 0; i < node.children.Length; i++)
            {
                for (int j = i+1; j < node.children.Length; j++)
                {
                    graph.AddEdge(node.children[i], node.children[j]);
                }
            }
        }
        
        // calculates the bounds of the octree based off an array of game objects
        void CalculateBounds(GameObject[] worldObjects)
        {
            foreach (var obj in worldObjects)
            {
                bounds.Encapsulate(obj.GetComponent<Collider>().bounds);
            }
            //makes the bounds a perfect square
            Vector3 size = Vector3.one * Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) * 0.6f;
            bounds.SetMinMax(bounds.center - size, bounds.center + size);
        }
    }
}