using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStats : ScriptableObject
{
    [Tooltip("The est. height the player should get from jumping"), Min(0)]
    public float jumpHeight;
    
    [Tooltip("The amount of time before a player can input another jump after inputing a jump"), Min(0)]
    public float jumpInputDelay = .1f;

    [Tooltip("Amount of time jump input will be buffered for"), Range(0, .5f)]
    public float jumpBuffer = .1f;

    [Tooltip("Amount of time you can jump after leaving the ground"), Range(0, .5f)]
    public float coyoteTime = .1f;

    //change to minimum jump height
    [Tooltip("Minimum amount of time before you can start falling because of variable jump hieght"), Min(0)]
    public float minimumJumpingTime;

    [Tooltip("Raycast distance to detect ground and ceiling"), Min(0)]
    public float platformDetectorDistance = .05f;

}
