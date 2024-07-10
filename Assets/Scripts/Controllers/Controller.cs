using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public InputController input = null;

    public void Update()
    {
        input?.CheckJumpInput();
        input?.CheckDashInput();
        input?.CheckPrimaryInput();
        input?.CheckSecondaryInput();
        input?.CheckRightSkillInput();
        input?.CheckLeftSkillInput();
    }

}
