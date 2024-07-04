using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStats : ScriptableObject
{
    [Tooltip("Max walking speed"), Min(0)]
    public float walkSpeed;

    [Tooltip("Determines how much faster sprint speed should be comapred to walking"), Min(0)]
    public float sprintSpeedMultiplier;
    
    [Tooltip("The speed at which you acceleration while grounded"), Min(0)]
    public float groundAcceleration;

    [Tooltip("The speed at which you acceleration in the air"), Min(0)]
    public float airAcceleration;

    [Tooltip("The speed at which the player slows down while grounded"), Min(0)]
    public float groundDeceleration;

    [Tooltip("The speed at which the player slows down in the air"), Min(0)]
    public float airDecelertaion;
    
    [Tooltip("How much gravity affects you while you are moving upwards"), Min(0)]
    public float upwardsGravityMultiplier;

    [Tooltip("How much gravity affects you while your falling"), Min(0)]
    public float downwardsGravityMultiplier;

    [Tooltip("Terminal Velocity")]
    public float maxFallSpeed;

    [Tooltip("A small amount of gravity to keep the player grounded on slopes"), Min(0)]
    public float groundedGravity;
}
