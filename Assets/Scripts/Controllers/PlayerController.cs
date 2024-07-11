using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "ScriptableObjects/InputControllers/Player")]
public class PlayerController : InputController
{
    private PlayerInputManager inputManager = null;
    
    public override Vector2 RetrieveMoveInput()
    {
        return PlayerInputManager.Instance.MoveInput;
    }

    public override Vector2 RetrieveLookInput()
    {        
        return PlayerInputManager.Instance.LookInput;
    }

    public override float RetrieveSprintInput()
    {
        return PlayerInputManager.Instance.SprintValue;
    }

    public override bool RetrieveJumpInput()
    {
        return PlayerInputManager.Instance != null ? PlayerInputManager.Instance.JumpTriggered : false;
    }

    public override bool RetrieveDashInput()
    {
        return PlayerInputManager.Instance.dashTriggered;
    }

    public override bool RetrieveCrouchInput()
    {
        return PlayerInputManager.Instance.crouchTriggered;
    }

    public override bool RetrievePrimaryAttack()
    {
        return PlayerInputManager.Instance.primaryAttackTriggered;
    }

    public override bool RetrieveSecondaryAttack()
    {
        return PlayerInputManager.Instance.secondaryAttackTriggered;
    }

    public override bool RetrieveRightSkill()
    {
        return PlayerInputManager.Instance.rightSkillTriggered;
    }

    public override bool RetrieveLeftSkill()
    {
        return PlayerInputManager.Instance.leftSkillTriggered;
    }
}
