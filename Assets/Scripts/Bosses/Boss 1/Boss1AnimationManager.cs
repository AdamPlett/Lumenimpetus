using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AnimationManager : MonoBehaviour
{
    public Animator bossAnimator;

    [Header("Animation Hashes")]
    public readonly int IdleHash = Animator.StringToHash("Idle");

    public readonly int WalkHash = Animator.StringToHash("Walk");
    public readonly int WalkHashL = Animator.StringToHash("WalkLeft");
    public readonly int WalkHashR = Animator.StringToHash("WalkRight");
    public readonly int WalkHashB = Animator.StringToHash("WalkBack");
    public readonly int TurnHashL = Animator.StringToHash("TurnLeft");
    public readonly int TurnHashR = Animator.StringToHash("TurnRight");

    public readonly int SwingHash = Animator.StringToHash("Swing");
    public readonly int GrappleHash = Animator.StringToHash("ShootGrapple");
    public readonly int PullHash = Animator.StringToHash("Pull");
    public readonly int Slam1Hash = Animator.StringToHash("Slam_1");
    public readonly int Slam2Hash = Animator.StringToHash("Slam_2");

    public readonly int ShootHash = Animator.StringToHash("Shoot");

    public readonly int MeleeHash1 = Animator.StringToHash("Melee1");
    public readonly int MeleeHash2 = Animator.StringToHash("Melee2");
    public readonly int MeleeHash3 = Animator.StringToHash("Melee3");
    public readonly int MeleeHash360 = Animator.StringToHash("Melee360");
    public readonly int ComboHash = Animator.StringToHash("MeleeCombo");
    public readonly int ChargeHash = Animator.StringToHash("MeleeCharge");

    public void PlayAnimation(int animHash)
    {
        bossAnimator.Play(animHash);
    }

    public void SwitchAnimation(int animHash)
    {
        bossAnimator.CrossFadeInFixedTime(animHash, 0.25f);
    }
}
