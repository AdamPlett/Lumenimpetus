using System.Linq;
using UnityEngine;
using System.Collections;

namespace Octrees
{
    public class Mover : MonoBehaviour
    {
        public float speed = 5f;
        public float turnSpeed = 5f;
        public float accuracy = 1f;

        int currentWaypoint;
        OctreeNode currentNode;
        Vector3 destination;

        public OctreeGenerator octreeGenerator;
        Graph graph;

        public bool randomPathing=true;
        public Vector3 setDestination;

        void Start()
        {
            graph = octreeGenerator.waypoints;
            currentNode = GetClosestNode(transform.position);
            if (randomPathing) GetRandomDestination();
            else SetDestination(setDestination);
        }

        void Update()
        {
            if (graph == null) return;

            if (randomPathing) RandomPathing();
            else Pathing();
        }

        void RandomPathing()
        {
            if (graph.GetPathLength() == 0 || currentWaypoint >= graph.GetPathLength())
            {
                GetRandomDestination();
                return;
            }

            if (Vector3.Distance(graph.GetPathNode(currentWaypoint).bounds.center, transform.position) < accuracy)
            {
                currentWaypoint++;
                Debug.Log($"Waypoint {currentWaypoint} reached");

            }
            if (currentWaypoint < graph.GetPathLength())
            {
                currentNode = graph.GetPathNode(currentWaypoint);
                destination = currentNode.bounds.center;

                Vector3 direction = destination - transform.position;
                direction.Normalize();

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            else
            {
                GetRandomDestination();
            }
        }

        void Pathing()
        {
            //for in editor testing
            if (Input.GetKeyDown("space"))
            {
                SetDestination(setDestination);
            }

            if (graph.GetPathLength() == 0 || currentWaypoint >= graph.GetPathLength())
            {
                return;
            }

            if (Vector3.Distance(graph.GetPathNode(currentWaypoint).bounds.center, transform.position) < accuracy)
            {
                currentWaypoint++;
                Debug.Log($"Waypoint {currentWaypoint} reached");

            }
            if (currentWaypoint < graph.GetPathLength())
            {
                currentNode = graph.GetPathNode(currentWaypoint);
                destination = currentNode.bounds.center;

                Vector3 direction = destination - transform.position;
                direction.Normalize();

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            else
            {

            }
        }

        void GetRandomDestination()
        {
            OctreeNode destinationNode;
            do
            {
                destinationNode = graph.nodes.ElementAt(Random.Range(0, graph.nodes.Count)).Key;
            } while (!graph.AStar(currentNode, destinationNode));
            currentWaypoint = 0;
        }
        bool SetDestination(Vector3 end)
        {
            OctreeNode destinationNode = octreeGenerator.ot.FindClosestNode(end);
            return graph.AStar(currentNode, destinationNode);
        }

        OctreeNode GetClosestNode(Vector3 position)
        {
            return octreeGenerator.ot.FindClosestNode(transform.position);
        }

        private void OnDrawGizmos()
        {
            if (graph == null || graph.GetPathLength() == 0) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(graph.GetPathNode(0).bounds.center, 0.7f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(graph.GetPathNode(graph.GetPathLength() - 1).bounds.center, 0.7f);

            Gizmos.color = Color.green;
            for (int i=0; i < graph.GetPathLength(); i++ )
            {
                Gizmos.DrawWireSphere(graph.GetPathNode(i).bounds.center, 0.5f);
                if (i< graph.GetPathLength()-1)
                {
                    Vector3 start = graph.GetPathNode(i).bounds.center;
                    Vector3 end = graph.GetPathNode(i + 1).bounds.center;
                    Gizmos.DrawLine(start, end);
                }
            }
        }
    }

}