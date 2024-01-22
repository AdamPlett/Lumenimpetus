using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using static GameManager;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallRunSpeed;

    public Vector3 playerVelocity;

    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed; //max vertical velocity

    public float groundDrag;//used in speed control

    public float jumpForce; //used in jump
    public float jumpCooldown;//used in jump
    public float airMultiplayer; //higher number means more air control
    public float airDrag; //used in speed control
    public float coyoteTime;
    private float coyoteTimer;
    private bool readyToJump = true;



    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    public AudioSource audioSource;
    

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Camera Effects")]
    public Transform orientation;
    public PlayerCam cam;
    public float grappleFov = 95f;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public float comboTime = 0.5f;
    private float comboTimer = 0;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    public bool combo = false;
    public bool attacking = false;
    public bool readyToAttack = true;
    public int attackCount;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    [Header("Animations")]
    public Animator animator;
    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    public const string ATTACK3 = "Attack 3";

    private string currentAnimationState;


    public enum MovementState
    {
        freeze,
        walking,
        sprinting,
        air,
        dashing,
        wallrunning,
        grappling,
    }

    public bool dashing;

    public bool wallrunning;

    public bool freeze;

    public bool activeGrapple;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        coyoteTimer -= Time.deltaTime;
        if (grounded) coyoteTimer = coyoteTime;
        playerVelocity = rb.velocity;

        MyInput();
        SpeedControl();
        StateHandler();
        SetAnimations();

        // handle drag
        if (state == MovementState.walking || state == MovementState.sprinting)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (combo)
        {
            comboTimer += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (!wallrunning) MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && (coyoteTimer > 0))
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(attackKey))
        {
            Attack();
        }
    }

    private void StateHandler()
    {
        // Mode - Freeze
        if (freeze)
        {
            state = MovementState.freeze;
            desiredMoveSpeed = 0;
            rb.velocity = Vector3.zero;
        }
        // Mode - Grappling
        else if (activeGrapple)
        {
            state = MovementState.grappling;
            moveSpeed = sprintSpeed;
        }

        // Mode - Wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallRunSpeed;
        }

        // Mode - Dashing
        else if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            cam.DoFov(85f, .25f);
        }

        // Mode - Walking
        else if (grounded) {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
            cam.DoFov(80f, .25f);
        }

        // Mode - Air
        else
        {
            state = MovementState.air;

            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = walkSpeed;
            else
                desiredMoveSpeed = sprintSpeed;
        }
    

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (lastState == MovementState.dashing) keepMomentum = true;

        if(desiredMoveSpeedHasChanged)
        {
            if(keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desire value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void MovePlayer()
    {
        // escape if grappling
        if (activeGrapple) return;

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);

            if (rb.velocity.y < 0)
                rb.AddForce(Vector3.down * 20f, ForceMode.Force);
        }
        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded && !activeGrapple && !dashing)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplayer, ForceMode.Force);

        if (state == MovementState.dashing) return;
        
        if(!wallrunning) rb.useGravity = !OnSlope();
        
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }


    private void SpeedControl()
    {
        // escape if grapple active
        if (activeGrapple) return;
        
        // limiting velocity on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting ground and air velocity
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
            if (!grounded)
            {
                
                
                Vector3 rawSpd = rb.velocity;
                Vector3 horiSpd = rawSpd;
                horiSpd.y = 0;
                Vector3 horiDrag = horiSpd * airDrag;//increase the percentage amount for more drag, and vice versa.
                rb.velocity = rawSpd - horiDrag;
            }
           
            // limit y vel
            if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
                rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }
    }


    //jump
    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        coyoteTimer = 0;
    }

    private void ResetJump()
    {
        readyToJump = true;
        
        exitingSlope = false;
    }

    //speed control/lerping
    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov, 0.25f);
    }

    //slope movement
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight *0.5f + 0.3f)) 
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    //Used in Grapple for pull velocity.
    private bool enableMovementOnNextTouch;


    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
    public void ResetRestrictions()
    {
        activeGrapple = false;
        cam.DoFov(80f, 0.25f);
    }




    //Attacks
    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        if (comboTimer > comboTime)
        {
            attackCount = 0;
            comboTimer = 0;
            combo = false;
        }
        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
        // plays attack SFX
     //   audioSource.pitch = Random.Range(0.9f, 1.1f);
     //   audioSource.PlayOneShot(swordSwing); 
        
        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            combo = true;
            attackCount++;
        }
        else if(attackCount == 1)
        {
            ChangeAnimationState(ATTACK2);
            comboTimer = 0;
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK3);
            attackCount = 0;
            combo = false;
            comboTimer = 0;
        }

    }

    private void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    private void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            
            //HitTarget(hit.point);

            if (hit.transform.gameObject.Equals(gm.bossRef))
            {
                Debug.Log("HIT BOSS");
                gm.bh.DamageBoss(attackDamage);
                
            }
        }
    }

    private void HitTarget(Vector3 pos)
    {
        Debug.Log("HIT");
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }


    //animation
    public void SetAnimations()
    {
        // if player is not attacking;
        if (!attacking)
        {
            if (playerVelocity.x == 0 && playerVelocity.z == 0)
            {
                ChangeAnimationState(IDLE);
            }
            else
            {
                ChangeAnimationState(WALK);
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself

        if (currentAnimationState == newState) return;

        // Play the animation
        currentAnimationState = newState;
        if (currentAnimationState == IDLE)
        {
            animator.CrossFadeInFixedTime(currentAnimationState, 0.4f);
        }
        else
        {
            animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
        }
    }
}
