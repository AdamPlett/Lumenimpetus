using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octrees
{
    public class OctreeGenerator : MonoBehaviour
    {
        public GameObject[] objects;
        public float minNodeSize = 1f;
        public Octree ot;

        public readonly Graph waypoints = new Graph();

        //on awake create a new octree based on the array of game objects and min node size set in the editor
        private void Awake() => ot = new Octree(objects, minNodeSize, waypoints);

        //when the application is playing it draws the bounds of the octree
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(ot.bounds.center, ot.bounds.size);

            ot.root.DrawNode();
            ot.graph.DrawGraph();
        }
    }

}

