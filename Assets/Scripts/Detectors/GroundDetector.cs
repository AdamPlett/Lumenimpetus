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

    private RaycastHit groundHit;

    void Update()
    {
        CheckGrounded();
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public RaycastHit GetGroundHit()
    {
        return groundHit;
    }

    private void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, gm.player.GetHeight() * 0.5f + detectionLength, groundLayer);

        if (grounded)
        {
            gm.player.jump.coyoteTimeTimer = gm.player.jump.stats.coyoteTime;
            gm.player.jump.jumpPhase = 0;
        }
    }
}
