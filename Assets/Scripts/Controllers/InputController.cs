using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    // Actions
    public Action jumpPerformed;
    public Action jumpCancelled;
    public Action dashPerformed;
    public Action crouchPerformed;
    public Action primaryPerformed;
    public Action secondaryPerformed;
    public Action rightSkillPerformed;
    public Action leftSkillPerformed;


    public virtual void CheckJumpInput()
    {
        if (RetrieveJumpInput())
        {
            jumpPerformed?.Invoke();
        }
        else
        {
            jumpCancelled?.Invoke();
        }
    }

    public virtual void CheckDashInput()
    {
        if (RetrieveDashInput())
        {
            dashPerformed?.Invoke();
        }
    }

    public virtual void CheckCrouchInput()
    {
        if (RetrieveDashInput())
        {
            dashPerformed?.Invoke();
        }
    }

    public virtual void CheckPrimaryInput()
    {
        if(RetrievePrimaryAttack())
        {
            primaryPerformed?.Invoke();
        }
    }

    public virtual void CheckSecondaryInput()
    {
        if (RetrieveSecondaryAttack())
        {
            secondaryPerformed?.Invoke();
        }
    }

    public virtual void CheckRightSkillInput()
    {
        if (RetrieveRightSkill())
        {
            rightSkillPerformed?.Invoke();
        }
    }

    public virtual void CheckLeftSkillInput()
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
    public abstract bool RetrieveCrouchInput();
    public abstract bool RetrievePrimaryAttack();
    public abstract bool RetrieveSecondaryAttack();
    public abstract bool RetrieveRightSkill();
    public abstract bool RetrieveLeftSkill();
}
