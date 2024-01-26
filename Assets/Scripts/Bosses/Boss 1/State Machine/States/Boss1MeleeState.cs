using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss1MeleeState : Boss1BaseState
{
    public Boss1MeleeState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private float stateTimer = 0f;

    public override void Enter()
    {
        SelectMeleeAttack();

        stateMachine.isAttacking = true;
        stateMachine.weapons.canMelee = false;

        gm.boss1.activeState = eB1.attacking;
    }

    public override void Tick()
    {
        stateTimer += Time.deltaTime;

        if (stateTimer > 3f)
        {
            stateMachine.SwitchToMoveState();
        }
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

            if (stateMachine.weapons.enraged)
            {
                stateMachine.weapons.SpawnEnergyWave(3);
            }
            //SFX
            stateMachine.bossAudio.pitch = Random.Range(.9f, 1.0f);
            stateMachine.bossAudio.PlayOneShot(stateMachine.attack2SFX);
        }
        else
        {
            int randomInt = Random.Range(0, 3);

            if (randomInt == 0)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash1);

                if(stateMachine.weapons.enraged)
                {
                    stateMachine.weapons.SpawnEnergyWave(0);
                }
                //SFX
                stateMachine.bossAudio.pitch = Random.Range(.9f, 1.0f);
                stateMachine.bossAudio.PlayOneShot(stateMachine.attack1SFX);

                Debug.Log("Melee attack #1 selected");
            }
            else if (randomInt == 1)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash2);

                if (stateMachine.weapons.enraged)
                {
                    stateMachine.weapons.SpawnEnergyWave(1);
                }
                //SFX
                stateMachine.bossAudio.pitch = Random.Range(1.0f, 1.1f);
                stateMachine.bossAudio.PlayOneShot(stateMachine.attack2SFX);

                Debug.Log("Melee attack #2 selected");
            }
            else if (randomInt == 2)
            {
                stateMachine.anim.SwitchAnimation(stateMachine.anim.MeleeHash3);

                if (stateMachine.weapons.enraged)
                {
                    stateMachine.weapons.SpawnEnergyWave(2);
                }
                //SFX
                stateMachine.bossAudio.pitch = Random.Range(1.0f, 1.1f); 
                stateMachine.bossAudio.PlayOneShot(stateMachine.attack1SFX);

                Debug.Log("Melee attack #3 selected");
            }
        }
    }
}
