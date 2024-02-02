using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Animation : MonoBehaviour
{
    public Animator bossAnimator;

    [Header("Animation Hashes")]
    public readonly int IdleHash = Animator.StringToHash("Idle");

    public readonly int WalkHashF = Animator.StringToHash("WalkForward");
    public readonly int WalkHashB = Animator.StringToHash("WalkBack");
    public readonly int WalkHashL = Animator.StringToHash("WalkLeft");
    public readonly int WalkHashR = Animator.StringToHash("WalkRight");

    public readonly int JumpHash = Animator.StringToHash("Jump");

    public readonly int PortalHash = Animator.StringToHash("Portal");

    public readonly int LaserHash = Animator.StringToHash("Laser");

    public readonly int AttackHash1 = Animator.StringToHash("Spell_01");
    public readonly int AttackHash2 = Animator.StringToHash("Spell_02");
    public readonly int AttackHash3 = Animator.StringToHash("Spell_03");
    public readonly int AttackHash4 = Animator.StringToHash("Spell_04");
    public readonly int AttackHash5 = Animator.StringToHash("Spell_05");

    public readonly int StaggerHash = Animator.StringToHash("Stagger");
    public readonly int DeathHash = Animator.StringToHash("Death");
    public readonly int DanceHash = Animator.StringToHash("Dance");

    // public readonly int PhaseTransition = Animator.StringToHash("PhaseTransition"); ??

    public void PlayAnimation(int animHash)
    {
        bossAnimator.Play(animHash);
    }

    public void SwitchAnimation(int animHash)
    {
        bossAnimator.CrossFadeInFixedTime(animHash, 0.25f);
    }
}
