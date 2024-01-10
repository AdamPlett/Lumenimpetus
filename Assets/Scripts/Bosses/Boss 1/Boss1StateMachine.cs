using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateMachine : StateMachine
{
    [Header("Movement Variables")]
    public float moveSpeed;

    [Header("Boss Components")]
    public BossHealth health;
    public Boss1Animation animator;
    public GameObject bossModel;

    public GameObject playerRef;

    void Start()
    {
        SwitchState(new Boss1MoveState(this));
    }

    public Transform GetBossTransform()
    {
        return gameObject.transform;
    }
}
