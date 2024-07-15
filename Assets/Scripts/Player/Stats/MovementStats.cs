using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Stats", menuName = "ScriptableObjects/MovementStats/BaseMovement")]
public class MovementStats : ScriptableObject
{
    [Tooltip("Max walk speed"), Min(0)]
    public float maxWalkSpeed;

    [Tooltip("Max speed"), Min(0)]
    public float maxSpeed;

    [Tooltip("Air Control Multiplier"), Min(0)]
    public float airControlMult = 0.5f;

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

    [Tooltip("Terminal Velocity")]
    public float maxFallSpeed;

    [Tooltip("A small amount of gravity to keep the player grounded on slopes"), Min(0)]
    public float groundedGravity;
}
