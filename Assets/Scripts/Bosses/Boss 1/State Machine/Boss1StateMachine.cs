using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateMachine : StateMachine
{
    [Header("Movement Variables")]
    public float moveSpeed;
    public float rotationSpeed;

    [Header("Boss Components")]
    public Boss1Health health;
    public Boss1AttackManager weapons;
    public Boss1AnimationManager anim;
    public GameObject bossModel;

    [Header("Environment Detection")]
    public LayerMask environment;

    [Header("Player Detection")]
    public GameObject playerRef;
    public LayerMask playerLayer;
    public Transform viewPoint;

    [Header("Booleans")]
    public bool isAttacking;
    public bool isShooting;
    public bool isGrappling;

    void Start()
    {
        SwitchState(new Boss1MoveState(this));
    }

    public void LookAtPlayer()
    {
        Vector3 directionToPlayer = playerRef.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        RotatePlayer(targetRotation);
    }

    public void RotatePlayer(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Adjust x and z back to zero, thus keeping enemy level with the ground
        Quaternion tempRot = transform.rotation;

        tempRot.x = 0;
        tempRot.z = 0;

        transform.rotation = tempRot;
    }

    public void CheckForPlayer()
    {
        if (CheckFacingPlayer())
        {
            if(CheckSeePlayer())
            {
                if (weapons.CheckMeleeRange() && weapons.canMelee)
                {
                    SwitchToMeleeState();
                }
                else if (weapons.CheckCannonRange() && weapons.canShoot)
                {
                    SwitchToShootState();
                }
                else if (weapons.CheckPullRange() && weapons.canGrapple)
                {
                    if (weapons.grappleTarget.position.y > transform.position.y + 2f)
                    {
                        SwitchToSlamState();
                    }
                    else
                    {
                        SwitchToPullState();
                    }
                }
                else if (weapons.canGrapple && health.GetCurrentPhase() > 1)
                {
                    if (weapons.CheckGrappleRange())
                    {
                        SwitchToGrappleState();
                    }
                }
            }
            else
            {
                if (weapons.CheckCannonRange() && weapons.canShoot)
                {
                    SwitchToShootState();
                }
                else if (weapons.canGrapple && health.GetCurrentPhase() > 1)
                {
                    if (weapons.CheckGrappleRange())
                    {
                        SwitchToGrappleState();
                    }
                }
            }
        }
        else
        {
            LookAtPlayer();
        }
    }

    public bool CheckFacingPlayer()
    {
        Vector3 forward = transform.forward;
        Vector3 toPlayer = (playerRef.transform.position - transform.position).normalized;

        if(Vector3.Dot(forward, toPlayer) < 0.75f)
        {
            return false;
        }

        return true;
    }

    public bool CheckSeePlayer()
    {
        Vector3 viewDirection = (playerRef.transform.position - viewPoint.position);

        Debug.DrawRay(viewPoint.position, viewDirection, Color.green);

        if (Physics.Raycast(viewPoint.position, viewDirection, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if(hitInfo.transform.gameObject == playerRef)
            {
                //Debug.Log("Can See Player");
                return true;
            }
            else
            {
               // Debug.Log("Can NOT See Player");
                return false;
            }
        }
        else
        {
            //Debug.Log("Can NOT See Player");
            return false;
        }
    }

    public bool CheckSeeFloor(eDir direction)
    {
        Vector3 viewDirection = Vector3.zero;
        float rayDistance = 0f;

        switch(direction)
        {
            case eDir.forward:
                viewDirection = (viewPoint.forward * 2.5f) - (viewPoint.up);
                rayDistance = 4f;
                break;

            case eDir.backward:
                viewDirection = (viewPoint.forward * -2f) - (viewPoint.up * 4f);
                rayDistance = 6f;
                break;

            case eDir.right:
                viewDirection = (viewPoint.right * 1.25f) - (viewPoint.up);
                rayDistance = 5.5f;
                break;

            case eDir.left:
                viewDirection = (viewPoint.right * -1.25f) - (viewPoint.up);
                rayDistance = 5.5f;
                break;
        }

        viewDirection = viewDirection.normalized * rayDistance;

        Debug.DrawRay(viewPoint.position, viewDirection, Color.red);

        if (Physics.Raycast(viewPoint.position, viewDirection, rayDistance, environment))
        {
            //Debug.Log("Can See Floor");
            return true;
        }
        else
        {
           // Debug.Log("Can NOT See Floor");
            return false;
        }
    }

    public bool CheckAbovePlayer()
    {
        if(transform.position.y > playerRef.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, playerRef.transform.position);
    }

    public Vector3 GetDirectionToPlayer()
    {
        return (playerRef.transform.position - transform.position).normalized;
    }



    #region State-Switchers

    public void SwitchToMoveState()
    {
        SwitchState(new Boss1MoveState(this));
    }

    public void SwitchToMeleeState()
    {
        SwitchState(new Boss1MeleeState(this));
    }

    public void SwitchToShootState()
    {
        SwitchState(new Boss1ShootState(this));
    }

    public void SwitchToGrappleState()
    {
        SwitchState(new Boss1GrappleState(this));
    }

    public void SwitchToPullState()
    {
        SwitchState(new Boss1PullState(this));
    }

    public void SwitchToSlamState()
    {
        SwitchState(new Boss1SlamState(this));
    }

    public void SwitchToFallState()
    {
        SwitchState(new Boss1SlamState(this));
    }

    public void SwitchToStunnedState()
    {
        //SwitchState(new Boss1StunState(this));
    }

    public void SwitchToDeadState()
    {
        //SwitchState(new Boss1DeathState(this));
    }

    #endregion
}
