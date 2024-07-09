using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    // Actions
    public Action jumpPerformed;
    public Action sprintPerformed;
    public Action dashPerformed;
    public Action primaryPerformed;
    public Action secondaryPerformed;
    public Action rightSkillPerformed;
    public Action leftSkillPerformed;
    
    public abstract Vector2 RetrieveMoveInput();
    public abstract Vector2 RetrieveLookInput();
    public abstract bool RetrieveJumpInput();
    public abstract float RetrieveSprintInput();
    public abstract bool RetrieveDashInput();
    public abstract bool RetrievePrimaryAttack();
    public abstract bool RetrieveSecondaryAttack();
    public abstract bool RetrieveRightSkill();
    public abstract bool RetrieveLeftSkill();
}
