using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octrees
{
    public class Octree
    {
        public OctreeNode root;
        public Bounds bounds;

        //constructor
        public Octree(GameObject[] worldObjects, float minNodeSize)
        {
            CalculateBounds(worldObjects);
            CreateTree(worldObjects, minNodeSize);
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
        
        // calculates the bounds of the octree based off an array of game objects
        void CalculateBounds(GameObject[] worldObjects)
        {
            foreach (var obj in worldObjects)
            {
                bounds.Encapsulate(obj.GetComponent<Collider>().bounds);
            }
            //makes the bounds a perfect square
            Vector3 size = Vector3.one * Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) * 0.5f;
            bounds.SetMinMax(bounds.center - size, bounds.center + size);
        }
    }
}