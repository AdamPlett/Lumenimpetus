using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLaserType { random, linked }

public class PortalManager : MonoBehaviour
{
    [Header("Portal Variables")]
    [SerializeField] private GameObject portalPrefab;
    [Space(5)]
    public List<GameObject> activePortals;
    [Space(10)]
    public bool portalsLinked;
    [SerializeField] private Transform portalPoint1, portalPoint2;
    [Space(10)]
    public List<Transform> portalPoints;

    [Header("Portal Variables")]
    public GameObject laserPrefab;
    [Space(5)]
    public List<GameObject> activeLasers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnPortals();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RemovePortals();
            RemoveLaser();
        }
    }

    private void SpawnPortals()
    {
        // Find the spawn point for the first portal
        portalPoint1 = SelectRandomPoint();

        // Find the spawn point for the second portal
        SearchForLinkedPoint(portalPoint1);

        if(portalsLinked)
        {
            // Spawn portal 1
            GameObject portal1 = Instantiate(portalPrefab, portalPoint1);
            activePortals.Add(portal1);

            // Spawn portal 2
            GameObject portal2 = Instantiate(portalPrefab, portalPoint2);
            activePortals.Add(portal2);

            // Initialize portal variables
            portal1.GetComponentInChildren<PortalTeleporter>().InitPortal(portal2.transform);
            portal2.GetComponentInChildren<PortalTeleporter>().InitPortal(portal1.transform);

            SpawnLaser(portal1.transform, portal2.transform);

            ResetPortalPoints();
        }
    }

    private void SpawnLaser(Transform start, Transform end)
    {        
        GameObject laser = Instantiate(laserPrefab);
        Laser laserScript = laser.GetComponent<Laser>();
        activeLasers.Add(laser);

        laserScript.DrawLaser(start.position, end.position);
    }

    private void RemovePortals()
    {
        
    }

    private void RemoveLaser()
    {
        
    }

    private void ResetPortalPoints()
    {
        portalPoint1 = null;
        portalPoint2 = null;
        portalsLinked = false;
    }

    private Transform SelectRandomPoint()
    {
        int randomInt = Random.Range(0, portalPoints.Count);

        return portalPoints[randomInt];
    }

    private void SearchForLinkedPoint(Transform point1)
    {
        Transform point2 = SelectRandomPoint().transform;

        if(point2 != point1 && CheckPath(point1, point2))
        {
            portalPoint2 = point2;
            portalsLinked = true;
        }
        else
        {
            SearchForLinkedPoint(point1);
        }
    }

    private bool CheckPath(Transform startPoint, Transform endPoint)
    {
        Vector3 direction = endPoint.position - startPoint.position;

        Debug.DrawRay(startPoint.position, direction, Color.yellow, 2f);

        if (Physics.Raycast(startPoint.position, direction, out RaycastHit hit, Mathf.Infinity))
        {
            if(hit.collider.tag.Equals("Portal"))
            {
                // Check to see if portal is facing towards other portal

                float dot = Vector3.Dot(direction, startPoint.up * -1f);

                if(dot > 0)
                {
                    Debug.Log("Portal Spawned!");
                    return true;
                }
                else
                {
                    Debug.Log("Unable to spawn portal!");
                    return false;
                }
            }
            else
            {
                Debug.Log("Unable to spawn portal!");
                return false;
            }
        }
        else
        {
            Debug.Log("Unable to spawn portal!");
            return false;
        }
    }
}
