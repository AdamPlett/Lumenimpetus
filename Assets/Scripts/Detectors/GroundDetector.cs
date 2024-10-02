using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GroundDetector : MonoBehaviour
{
    [Header("Ground Detection")]
    [SerializeField] private bool grounded;
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float detectionLength;

    [Header("Slope Detection")]
    [SerializeField] private bool onSlope;
    [SerializeField] private bool exitingSlope;

    [Space(5)]
    [SerializeField] private float slopeAngle;
    [SerializeField] private float minSlopeAngle;
    [SerializeField] private float maxSlopeAngle;

    private RaycastHit groundHit;

    void Update()
    {
        DetectGround();
    }

    public void DetectGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, gm.player.GetHeight() * 0.5f + detectionLength, groundLayer);

        Debug.Log(grounded);

        if (grounded)
        {
            // Reset Coyote Timer
            gm.player.jump.coyoteTimeTimer = gm.player.jump.stats.coyoteTime;
            gm.player.jump.jumpPhase = 0;

            // Determine if on slope
            slopeAngle = Vector3.Angle(Vector3.up, groundHit.normal);
            onSlope = slopeAngle < maxSlopeAngle && slopeAngle > minSlopeAngle;
        }
    }

    public RaycastHit GetGroundHit()
    {
        return groundHit;
    }

    public Vector3 GetGroundNormal()
    {
        return groundHit.normal;
    }

    public float GetSlopeAngle()
    {
        return slopeAngle;
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public bool GetOnSlope()
    {
        return onSlope;
    }

    public bool GetExitingSlope()
    {
        return exitingSlope;
    }
}
