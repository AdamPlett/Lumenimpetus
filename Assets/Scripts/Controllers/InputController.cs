using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract Vector2 RetrieveMoveInput();
    public abstract Vector2 RetrieveLookInput();
    public abstract bool RetrieveJumpInput();
    public abstract float RetrieveSprintInput();
    public abstract bool RetrieveRightSkill();
    public abstract bool RetrieveLeftSkill();

}
