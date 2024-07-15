using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Variables")]
    public float dashForce;
    public float dashUpwardForce;

    [Header("Dash Timer")]
    public bool dashing;
    [Space(5)]
    public float dashDuration;
    public float dashTimer;

    [Header("Cooldown Timer")]
    public bool canDash;
    [Space(5)]
    public float cooldown;
    private float cooldownTimer;

    [Header("Settings")]
    public bool allowAllDirections;
    public bool disableGravity;
    public bool resetVelocity;

    [Header("Misc")]
    public PlayerMovementStateMachine stateMachine;

    public void DashAction()
    {
        Vector3 dashDirection = CalculateDashDirection(stateMachine.transform);

        Vector3 force = dashDirection * dashForce + stateMachine.transform.up * dashUpwardForce;

        ApplyDashForce(force);
    }

    private void ApplyDashForce(Vector3 force)
    {
        if(resetVelocity)
        {
            stateMachine.rb.velocity = Vector3.zero;
            stateMachine.rb.angularVelocity = Vector3.zero;
        }

        stateMachine.rb.AddForce(force, ForceMode.Impulse);
    }

    private Vector3 CalculateDashDirection(Transform obj)
    {
        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        Vector3 direction = new Vector3();

        if (allowAllDirections)
        {
            direction = obj.forward * verticalInput + obj.right * horizontalInput;
        }
        else
        {
            direction = obj.forward;
        }

        if (verticalInput == 0 && horizontalInput == 0)
        {
            direction = obj.forward;
        }

        return direction.normalized;
    }

    #region Dash Timer

    public void IncrementDashTimer()
    {
        dashTimer += Time.deltaTime;
    }

    public void ResetDashTimer()
    {
        dashTimer = 0f;
    }

    #endregion

    #region Cooldown Timer

    public void StartCooldown()
    {
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        dashing = true;
        canDash = false;

        while(cooldownTimer > 0f)
        {
            IncrementCooldown();

            yield return null;
        }

        dashing = false;
        canDash = true;

        ResetCooldown();
    }

    private void IncrementCooldown()
    {
        cooldownTimer -= Time.deltaTime;
    }

    private void ResetCooldown()
    {
        cooldownTimer = cooldown;
    }

    #endregion
}
