using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eB2 { none, idle, moving, attacking, teleporting, spawningLaser, staggered, dead, dancing }

public class Boss2StateMachine : StateMachine
{
    public eB1 activeState;
    [Space(6)]
    public float stateTimer;
    public float maxStateTime;

    [Header("Movement Variables")]
    public eDir moveDirection;
    public float moveSpeed;
    public float rotationSpeed;
    public bool freeze;

    [Header("Boss Components")]
    public Boss2Health health;
    public Boss2Animation anim;
    public GameObject bossModel;
    public PortalManager portalManager;

    [Header("Environment Detection")]
    public LayerMask environment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region State-Switchers

    public void SwitchToIdleState()
    {
        //SwitchState(new Boss2IdleState(this));
    }

    public void SwitchToMoveState()
    {
        //SwitchState(new Boss2MoveState(this));
    }

    public void SwitchToMeleeState()
    {
        //SwitchState(new Boss2MeleeState(this));
    }

    public void SwitchToLaserState()
    {
        //SwitchState(new Boss2LaserState(this));
    }

    public void SwitchToTeleportState()
    {
        //SwitchState(new Boss1PortalState(this));
    }

    public void SwitchToStaggerState()
    {
        //SwitchState(new Boss2StaggerState(this));
    }

    public void SwitchToDeadState()
    {
        //SwitchState(new Boss2DeathState(this));
    }

    public void SwitchToDanceState()
    {
        //SwitchState(new Boss2DanceState(this));
    }

    #endregion
}
