using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Input Action Map")]
    [SerializeField] private string actionMapName = "PlayerBasic";

    [Header("Action Name Refrences")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string look = "Look";
    [SerializeField] private string rightSkill = "RightSkill";
    [SerializeField] private string leftSkill = "LeftSkill";

    //actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction lookAction;
    private InputAction rightSkillAction;
    private InputAction leftSkillAction;
    //refrences to actions current values
    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool rightSkillTriggered { get; private set; }
    public bool leftSkillTriggered { get; private set; }

    public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {
        //if there is Instance of static PlayerInputManager already than make it this GameObject and make it a singleton
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //if there is already an Instance of static PlayerInputManager than destroy this gameObject
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        rightSkillAction = playerControls.FindActionMap(actionMapName).FindAction(rightSkill);
        leftSkillAction = playerControls.FindActionMap(actionMapName).FindAction(leftSkill);

        //subscribes the action logic to the actions
        RegisterInputActions();
    }
    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.started += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        rightSkillAction.performed += context => rightSkillTriggered = true;
        rightSkillAction.canceled += context => rightSkillTriggered = false;

        leftSkillAction.performed += context => leftSkillTriggered = true;
        leftSkillAction.canceled += context => leftSkillTriggered = false;
    }
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        lookAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        lookAction.Disable();
    }
}
