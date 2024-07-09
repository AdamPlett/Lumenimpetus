using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public PlayerInputManager inputManager = null;

    // Actions
    public Action jumpPerformed;
    public Action dashPerformed;
    public Action primaryPerformed;
    public Action secondaryPerformed;
    public Action rightSkillPerformed;
    public Action leftSkillPerformed;
    
    public void Init()
    {
        if(inputManager == null)
        {
            inputManager = PlayerInputManager.Instance;
        }
    }

    public void Update()
    {
        CheckJumpInput();
        CheckDashInput();
        CheckPrimaryInput();
        CheckSecondaryInput();
        CheckRightSkillInput();
        CheckLeftSkillInput();
    }

    public void CheckJumpInput()
    {
        if (RetrieveJumpInput())
        {
            jumpPerformed?.Invoke();
        }
    }

    public void CheckDashInput()
    {
        if (RetrieveDashInput())
        {
            dashPerformed?.Invoke();
        }
    }

    public void CheckPrimaryInput()
    {
        if(RetrievePrimaryAttack())
        {
            primaryPerformed?.Invoke();
        }
    }

    public void CheckSecondaryInput()
    {
        if (RetrieveSecondaryAttack())
        {
            secondaryPerformed?.Invoke();
        }
    }

    public void CheckRightSkillInput()
    {
        if (RetrieveRightSkill())
        {
            rightSkillPerformed?.Invoke();
        }
    }

    public void CheckLeftSkillInput()
    {
        if (RetrieveLeftSkill())
        {
            leftSkillPerformed?.Invoke();
        }
    }

    public abstract Vector2 RetrieveMoveInput();
    public abstract Vector2 RetrieveLookInput();
    public abstract float RetrieveSprintInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveDashInput();
    public abstract bool RetrievePrimaryAttack();
    public abstract bool RetrieveSecondaryAttack();
    public abstract bool RetrieveRightSkill();
    public abstract bool RetrieveLeftSkill();
}
