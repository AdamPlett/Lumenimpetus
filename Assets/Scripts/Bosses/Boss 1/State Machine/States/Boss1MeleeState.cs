using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1MeleeState : Boss1BaseState
{
    public Boss1MeleeState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        SelectMeleeAttack();

        stateMachine.isAttacking = true;
        stateMachine.weapons.canMelee = false;
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        stateMachine.isAttacking = false;
        stateMachine.weapons.meleeTimer = 0f;
    }

    private void SelectMeleeAttack()
    {
        stateMachine.weapons.comboCounter++;

        if(stateMachine.weapons.comboCounter > 2)
        {
            stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash360);
            stateMachine.weapons.comboCounter = 0;
        }
        else
        {
            int randomInt = Random.Range(0, 3);

            if (randomInt == 0)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash1);

                Debug.Log("Melee attack #1 selected");
            }
            else if (randomInt == 1)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash2);

                Debug.Log("Melee attack #2 selected");
            }
            else if (randomInt == 2)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash3);

                Debug.Log("Melee attack #3 selected");
            }
        }
    }
}
