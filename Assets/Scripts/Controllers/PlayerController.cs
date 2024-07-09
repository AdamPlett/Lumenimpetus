using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "ScriptableObjects/InputControllers/Player")]
public class PlayerController : InputController
{
    private PlayerInputManager inputManager;

    //doesnt work with on enable or awake
    private void OnEnable()
    {
        inputManager = PlayerInputManager.Instance;
    }
    public override bool RetrieveJumpInput()
    {
        if (inputManager==null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }
        //returns the jump triggered bool from the player input manager or returns false if the player manager is null
        return inputManager != null ? inputManager.JumpTriggered : false;
    }
    public override Vector2 RetrieveMoveInput()
    {
        if (inputManager == null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }
        jumpPerformed.Invoke();
        return inputManager != null ? inputManager.MoveInput : Vector2.zero;

    }
    public override Vector2 RetrieveLookInput()
    {
        if (inputManager == null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }
        return inputManager != null ? inputManager.LookInput : Vector2.zero;
    }
    public override float RetrieveSprintInput()
    {
        return inputManager.SprintValue;
    }
    public override bool RetrieveDashInput()
    {
        return inputManager.dashTriggered;
    }
    public override bool RetrievePrimaryAttack()
    {
        return inputManager.primaryAttackTriggered;
    }
    public override bool RetrieveSecondaryAttack()
    {
        return inputManager.secondaryAttackTriggered;
    }
    public override bool RetrieveRightSkill()
    {
        return inputManager.rightSkillTriggered;
    }
    public override bool RetrieveLeftSkill()
    {
        return inputManager.leftSkillTriggered;
    }
}
