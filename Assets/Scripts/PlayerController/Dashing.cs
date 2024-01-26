using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    public ImageAnimation canDashAnim;
    private Rigidbody rb;
    private PlayerMovement pm;
    public AudioSource dashPlayer;
    public AudioClip dashSFX;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("CameraFX")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Dash Particles")]
    [SerializeField] private ParticleSystem dashForwardFX;
    [SerializeField] private ParticleSystem dashBackwardFX;
    [SerializeField] private ParticleSystem dashRightFX;
    [SerializeField] private ParticleSystem dashLeftFX;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;
    //makes sure the dash doesn't refresh mid air
    public bool canDash;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
        if (!pm.dashing && pm.grounded || pm.wallrunning)
        {
            canDash = true;
        }
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
        //turns on can dash animation
        if (canDash && dashCdTimer<=0)
        {
            canDashAnim.setActiveTrue();
        }
        //turns off can dash animation
        if (!canDash && dashCdTimer > 0)
        {
            canDashAnim.setActiveFalse();
        }
    }

    private void Dash()
    {
        if (dashCdTimer > 0 || !canDash) return;

        dashPlayer.PlayOneShot(dashSFX);

        dashCdTimer = dashCd;
        canDash = false;
        pm.dashing = true;
        pm.maxYSpeed = maxDashYSpeed;

        cam.DoFov(dashFov, .25f);

        Transform forwardT;

        if (useCameraForward)
            forwardT = playerCam;
        else
            forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);

        //playDashFX(direction);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(EndDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        if(resetVel) rb.velocity = Vector3.zero;

        playDashFX(delayedForceToApply);
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    //stops the dash
    private void EndDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0;
        //FOV
        cam.DoFov(80f, .25f);
        //enable gravity
        if (disableGravity)
            rb.useGravity = true;

    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;
        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;
        
        return direction.normalized;
    }

    void playDashFX(Vector3 dir)
    {
        //forward dash particles
        if (dir.z > 0 && Mathf.Abs(dir.x) <= dir.z)
        {
            dashForwardFX.Play();
        }

        //backwards dash particles
        else if (dir.z < 0 && Mathf.Abs(dir.x) <= Mathf.Abs(dir.z))
        {
            dashBackwardFX.Play();
        }
        else if (dir.x > 0)
        {
            dashRightFX.Play();
        }
        else if (dir.x < 0 )
        {
            dashLeftFX.Play();
        }
        else
        {
            dashForwardFX.Play();
        }

    }
}
