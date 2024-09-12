using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class WallDetector : MonoBehaviour
{
    [Header("Wall Detection")]
    [SerializeField] private bool facingWall;
    [SerializeField] private bool wallToRight, wallToLeft;
    [Space(5)]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float detectionLength;

    private RaycastHit wallHit;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool CheckWallForward()
    {
        return facingWall;
    }

    public bool CheckWallRight()
    {
        return wallToRight;
    }

    public bool CheckWallLeft()
    {
        return wallToLeft;
    }

    public RaycastHit GetWallHit()
    {
        return wallHit;
    }

    public void CheckForWalls()
    {

    }

}
