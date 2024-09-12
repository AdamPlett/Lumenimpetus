using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SlopeDetector : MonoBehaviour
{
    [Header("Slope Detection")]
    [SerializeField] private bool onSlope;
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float detectionLength;
    [Space(5)]
    [SerializeField] private float slopeAngle;
    [SerializeField] private float minSlopeAngle;
    [SerializeField] private float maxSlopeAngle;

    private RaycastHit slopeHit;

    void Update()
    {
        CheckOnSlope();
    }

    public bool GetOnSlope()
    {
        return onSlope;
    }

    public RaycastHit GetSlopeHit()
    {
        return slopeHit;
    }

    private void CheckOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, gm.player.GetHeight() * 0.5f + detectionLength, groundLayer))
        {
            slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);

            onSlope = slopeAngle < maxSlopeAngle && slopeAngle > minSlopeAngle;
        }
    }
}
