using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateMachine : StateMachine
{
    [Header("Movement Variables")]
    public float moveSpeed;

    [Header("Attack Variables")]
    public float timeBetweenShots;
    public float meleeRadius;

    [Header("Boss Components")]
    public BossHealth health;
    public Boss1Animation animator;
    public GameObject bossModel;

    [Header("References")]
    public GameObject playerRef;
    public GameObject energySword;

    void Start()
    {
        SwitchState(new Boss1MoveState(this));
    }

    public Transform GetBossTransform()
    {
        return gameObject.transform;
    }
}
