using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octrees
{
    public class OctreeObject 
    {
        Bounds bounds;

        //constructor
        public OctreeObject(GameObject obj)
        {
            bounds = obj.GetComponent<Collider>().bounds;
        }

        //Checks if another bounds intersects with this bounds
        public bool Intersects(Bounds boundsToCheck) => bounds.Intersects(boundsToCheck);
    }
}
