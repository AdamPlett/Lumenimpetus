using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //References
    [SerializeField] private JumpStats stats;
    private Controller controller;
    private Rigidbody rb;
    //private Ground ground

    private Vector3 velocity;
    private float jumpSpeed, defaultGravityScale = 1f;

    private int jumpPhase = 0;
    private bool desiredJump, grounded;

    private float jumpInputDelayTimer = 0f, jumpBufferTimer = 0f, coyoteTimeTimer = 0f;
    private float timeBetweenJumps = -1f;

    private void Awake()
    {
        controller = GetComponent<Controller>();
        rb = GetComponent<Rigidbody>();
        //ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    private void Update()
    {
        jumpInputDelayTimer -= Time.deltaTime;
        if(jumpInputDelayTimer<0)
        {
            desiredJump |= controller.input.RetrieveJumpInput();

            if (desiredJump)
            {
                jumpInputDelayTimer = stats.jumpInputDelay;
                jumpBufferTimer = stats.jumpBuffer;
            }
        }
    }

    private void FixedUpdate()
    {
        jumpBufferTimer -= Time.deltaTime;
        coyoteTimeTimer -= Time.deltaTime;
        timeBetweenJumps -= Time.deltaTime;

        //grounded = ground.grounded
        velocity = rb.velocity;

        //if the player is grounded than reset double jumps and coyote timer
        if (grounded)
        {
            jumpPhase = 0;
            coyoteTimeTimer = stats.coyoteTime;
        }

        if (jumpBufferTimer>0)
        {
            desiredJump = false;
            JumpAction();
        }

        //sets the current gravity scale (lower gravity while jumping upwards)
        /*
        if (rb.velocity.y < 0) rb.gravityScale = stats.downwardsGravityMultiplier;
        else if (rb.velocity.y == 0) rb.gravityScale = defaultGravityScale;
        else if (rb.velocity.y > 0 && controller.input.RetrieveJumpInput()) rb.gravityScale = stats.jumpingGravityMultiplier;
        else rb.gravityScale = stats.downwardsGravityMultiplier; */

        if (velocity.y < stats.maxFallSpeed)
        {
            velocity.y = stats.maxFallSpeed;
        }

        rb.velocity = velocity;
    }
    private void JumpAction()
    {
        if ((jumpBufferTimer > 0 && coyoteTimeTimer > 0 && timeBetweenJumps < 0) || (jumpPhase < stats.doubleJumps && timeBetweenJumps <0))
        {
            timeBetweenJumps = stats.jumpInputDelay;

            jumpPhase += 1;
            
            //calculates jumpSpeed based the JumpStats jumpHeight 
            jumpSpeed = Mathf.Sqrt(-1f * Physics.gravity.y * stats.jumpHeight);

            if (velocity.y > 0) 
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);

            velocity.y += jumpSpeed;
            Debug.Log("Jump Applied");
        }
    }
}
