using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //References
    public JumpStats stats;
    public PlayerMovementStateMachine stateMachine;
    //private Ground ground

    private Vector3 velocity;
    private float jumpSpeed, defaultGravityScale = 1f;

    [Space(5)]
    [Header("READ ONLY! (use stats to change jump)")]
    public int jumpPhase = 0;
    public bool desiredJump;
    private bool grounded;

    public float jumpInputDelayTimer = 0;
    public float jumpBufferTimer = 0;
    public float coyoteTimeTimer = 0f;
    public float timeBetweenJumps = -1f;


    // Update is called once per frame
    private void Update()
    {
        jumpInputDelayTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        jumpBufferTimer -= Time.deltaTime;
        coyoteTimeTimer -= Time.deltaTime;
        timeBetweenJumps -= Time.deltaTime;

        velocity = stateMachine.rb.velocity;

        //if the player is grounded than reset double jumps and coyote timer
        if (grounded)
        {
            jumpPhase = 0;
            coyoteTimeTimer = stats.coyoteTime;
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

        stateMachine.rb.velocity = velocity;
    }
    public void JumpAction()
    {
        Debug.Log("Jumping");
        timeBetweenJumps = stats.jumpInputDelay;

        jumpPhase += 1;
            
        //calculates jumpSpeed based the JumpStats jumpHeight 
        jumpSpeed = Mathf.Sqrt(-1f * Physics.gravity.y * stats.jumpHeight);

        if (velocity.y > 0)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
        }
        Debug.Log("jumpSpeed:" + jumpSpeed);

        velocity.y += jumpSpeed;
        stateMachine.rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.VelocityChange);
        Debug.Log("Jump Applied");
    }
}
