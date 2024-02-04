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
    public float groundDrag;//used in speed control
    public bool keepMomentum;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    //for respawning the player
    public Vector3 lastGroundPos= Vector3.zero;


    [Header("Jump")]
    public float jumpForce; //used in jump
    public float jumpCooldown;//used in jump
    public float airMultiplayer; //higher number means more air control
    public float airDrag; //used in speed control
    public float coyoteTime;
    private float coyoteTimer;
    private bool readyToJump = true;
    public MovingPlatformLoop mpl;
    public float timeTillUnparent = .1f;
    bool jump = false;


    [Header("Dash")]
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public float maxYSpeed; //max vertical velocity


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode rangedKey = KeyCode.Mouse1;


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
    public float attackDistance = 3f; //how far attack raycast goes
    public float attackDelay = 0.4f; //how long before attack raycast is sent
    public float attackSpeed = 1f; //how long before attack resets
    public float attackTime = 0.5f; //how long you have to attack after reset to get 2nd and 3rd attack
    public float attackTimer = 0f; //timer to count
    public float comboTime = 5f; //how long you have to continue combo
    private float comboTimer = 0f;
    public float hitstopTime = 0.1f;
    public float hitStunTime = 0.15f;
    public float attackDamage = 5f;
    public float comboMultiplier = 0.05f;
    public float comboCount = 0;
    public LayerMask attackLayer;
    public bool multiAttack = false;
    public bool combo = false;
    public bool attacking = false;
    public bool readyToAttack = true;
    public bool playerStunned = false;
    public int attackCount;


    [Header("Effects")]
    public AudioSource swordSFX;
    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;
    public bool hitStop = false;


    public float horizontalInput;
    public float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;


    [Header("Animations")]
    public Animator animator;
    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string SPRINT = "Sprint";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    public const string ATTACK3 = "Attack 3";
    public const string STUNNED = "Stunned";

    private string currentAnimationState;


    [Header("States")]
    public MovementState state;
    private MovementState lastState;
    public bool dashing;
    public bool wallrunning;
    public bool freeze;
    public bool activeGrapple;
    public bool teleporting;

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
        if (grounded)
        {
            coyoteTimer = coyoteTime;
            lastGroundPos = gameObject.transform.position;
        }
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

        if (combo && !hitStop)
        {
            comboTimer += Time.deltaTime;
        }
        if (multiAttack && !hitStop && attackTimer < 10)
        {
            attackTimer += Time.deltaTime;
        }
        if (comboTimer > comboTime)
        {
            ResetCombo();
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
        if (Input.GetKey(jumpKey) && readyToJump && (coyoteTimer > 0))
        {
            readyToJump = false;

            jump = true;

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    #region Basic Movement & State Handling
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
                StopCoroutine(SmoothlyLerpMoveSpeed());
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopCoroutine(SmoothlyLerpMoveSpeed());
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private void MovePlayer()
    {
        // escape if grappling
        if (activeGrapple || playerStunned) return;

        if (jump)
        {
            Jump();
            jump = false;
        }

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);

            else if (rb.velocity.y < 0)
                rb.AddForce(Vector3.down * 20f, ForceMode.Force);
            else
            {
                rb.AddForce(slopeHit.normal.normalized * 50f, ForceMode.Force);
            }
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

    public void PlayerKnockback(Vector3 knockbackDirection, float knockbackForce)
    {
        rb.AddForce(knockbackDirection.normalized *  knockbackForce, ForceMode.Impulse);
    }

    #endregion



    #region Jump & Slope Movememnt


    //jump
    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        
        //transform.SetParent(null);

        if (mpl != null)
        {
            //rb.velocity += mpl.GetVelocity();
            //mpl = null;
            Invoke(nameof(setParentNull), timeTillUnparent);
        }
        

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        coyoteTimer = 0;
    }

    private void setParentNull()
    {
        transform.SetParent(null);
        rb.velocity += mpl.GetVelocity();
        mpl = null;
    }

    private void ResetJump()
    {
        readyToJump = true;
        
        exitingSlope = false;
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
    #endregion


    
    #region Speed Control
    //speed control/lerping
    public float speedChangeFactor;
    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov, 0.25f);
    }
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
            if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed && dashing)
                rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }
    }
#endregion



    #region Grapple

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

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        cam.DoFov(80f, 0.25f);
    }
    #endregion



    #region Attack
    //Attacks

    public void Attack()
    {
        if (!readyToAttack || attacking || playerStunned || gm.ph.dead) return;

        readyToAttack = false;
        attacking = true;

        
        if (attackTimer - attackSpeed > attackTime)
        {
            attackCount = 0;
            attackTimer = 0;
            multiAttack = false;
        }



        // plays attack SFX

        if (!playerStunned)
        {
            swordSFX.pitch = Random.Range(0.9f, 1.1f);
            swordSFX.PlayOneShot(swordSwing);
        }

        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            multiAttack = true;
            attackDamage = 4;
            attackCount++;
            attackTimer = 0;
        }
        else if(attackCount == 1)
        {
            ChangeAnimationState(ATTACK2);
            attackDamage = 5;
            attackCount++;
            attackTimer = 0;
        }
        else
        {
            ChangeAnimationState(ATTACK3);
            attackDamage = 7;
            attackCount = 0;
            attackTimer = 0;
        }
        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
    }

    private void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    private void AttackRaycast()
    {
        if(!playerStunned && Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {

            HitTarget(hit.point);

            if (hit.transform.gameObject.Equals(gm.bossRef) && gm.boss1.activeState != eB1.dead)
            {
                Debug.Log("HIT BOSS");
                gm.bh.DamageBoss(attackDamage + attackDamage * comboMultiplier);
                Combo();
                
                if (gm.boss1.activeState != eB1.attacking) StartCoroutine(BossHitstop());
                
            }
            StartCoroutine(SwordHitstop());
        }
    }

    private void HitTarget(Vector3 pos)
    {
        Debug.Log("HIT");
        swordSFX.pitch = Random.Range(0.9f, 1.1f);
        swordSFX.PlayOneShot(hitSound);

        

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    public void Combo()
    {
        combo = true;
        comboCount++;
        comboTimer = 0;
        if (comboMultiplier < 0.5f && (comboCount % 3 == 0))
        {
            comboMultiplier += 0.05f;
        }
        if (comboCount > 2) gm.ui.cc.UpdateText(comboCount);
        Debug.Log(comboCount);
    }
    public void ResetCombo()
    {
        comboCount = 0;
        combo = false;
        comboMultiplier = 0;
        comboTimer = 0;
        
    }

    #endregion



    #region Animation
    //animation
    public void SetAnimations()
    {
        // if player is not attacking;
        if (!attacking && !playerStunned)
        {
            if (horizontalInput == 0 && verticalInput == 0)
            {
                ChangeAnimationState(IDLE);
            }
            else if(state == MovementState.sprinting)
            {
                ChangeAnimationState(SPRINT);
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
        else if (playerStunned)
        {
            animator.CrossFadeInFixedTime(currentAnimationState, hitStunTime);
        }
        else
        {
            animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
        }
    }
    public IEnumerator SwordHitstop()
    {
        float prevSpeed;

        hitStop = true;

        // Pause
        prevSpeed = animator.speed;
        animator.speed = 0;


        yield return new WaitForSeconds(hitstopTime);

        // Continue
        animator.speed = prevSpeed;
        hitStop = false;

    }

    public IEnumerator BossHitstop()
    {
        float prevSpeed;
        Animator anim = gm.boss1.GetComponent<Animator>();
        prevSpeed = anim.speed;
        anim.speed = 0;

        yield return new WaitForSeconds(hitstopTime);

        anim.speed = prevSpeed;
    }


    public void BossHitsPlayerStun()
    {
        playerStunned = true;
        StartCoroutine(PlayerStun());

    }

    public IEnumerator PlayerStun()
    {
        attackCount = 0;
        ResetCombo();
        ChangeAnimationState(STUNNED);
        yield return new WaitForSeconds(hitStunTime);
        playerStunned = false;
    }
    #endregion
}
