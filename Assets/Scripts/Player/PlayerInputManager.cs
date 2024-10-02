using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Input Action Map")]
    [SerializeField] private string actionMapName = "PlayerBasic";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string dash = "Dash";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string primaryAttack = "PrimaryAttack";
    [SerializeField] private string secondaryAttack = "SecondaryAttack";
    [SerializeField] private string rightSkill = "RightSkill";
    [SerializeField] private string leftSkill = "LeftSkill";

    //Input Actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction crouchAction;
    private InputAction primaryAttackAction;
    private InputAction secondaryAttackAction;
    private InputAction rightSkillAction;
    private InputAction leftSkillAction;

    [field: Header("Action Values")]
    [field: SerializeField] public Vector2 MoveInput { get; private set; }
    [field: SerializeField] public Vector2 LookInput { get; private set; }
    [field: SerializeField] public float SprintValue { get; private set; }
    [field: SerializeField] public bool JumpTriggered { get; private set; }
    [field: SerializeField] public bool dashTriggered { get; private set; }
    [field: SerializeField] public bool crouchTriggered { get; private set; }
    [field: SerializeField] public bool primaryAttackTriggered { get; private set; }
    [field: SerializeField] public bool secondaryAttackTriggered { get; private set; }
    [field: SerializeField] public bool rightSkillTriggered { get; private set; }
    [field: SerializeField] public bool leftSkillTriggered { get; private set; }

    [SerializeField] public static PlayerInputManager Instance;

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
        crouchAction = playerControls.FindActionMap(actionMapName).FindAction(crouch);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        primaryAttackAction = playerControls.FindActionMap(actionMapName).FindAction(primaryAttack);
        secondaryAttackAction = playerControls.FindActionMap(actionMapName).FindAction(secondaryAttack);
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

        crouchAction.started += context => crouchTriggered = true;
        //crouchAction.performed += context => CrouchPerformed?.Invoke();
        crouchAction.canceled += context => crouchTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        dashAction.performed += context => dashTriggered = true;
        dashAction.canceled += context => dashTriggered = false;

        primaryAttackAction.performed += context => primaryAttackTriggered = true;
        primaryAttackAction.canceled += context => primaryAttackTriggered = false;

        secondaryAttackAction.performed += context => secondaryAttackTriggered = true;
        secondaryAttackAction.canceled += context => secondaryAttackTriggered = false;

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
        crouchAction.Enable();
        lookAction.Enable();
        dashAction.Enable();
        primaryAttackAction.Enable();
        secondaryAttackAction.Enable();
        rightSkillAction.Enable();
        leftSkillAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        lookAction.Disable();
        crouchAction.Disable();
        dashAction.Disable();
        primaryAttackAction.Disable();
        secondaryAttackAction.Disable();
        rightSkillAction.Disable();
        leftSkillAction.Disable();
    }

}
