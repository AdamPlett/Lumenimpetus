using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AirMovement : MonoBehaviour
{
    public PlayerMovementStateMachine stateMachine;

    public void Move()
    {
        MoveInAir(CalculateMoveDirection());
    }

    public void MoveInAir(Vector3 moveDirection)
    {
        stateMachine.movement.MovePlayer(moveDirection);

        /*
        Vector3 projVel = Vector3.Project(rb.velocity, moveDirection);

        bool facingAway = Vector3.Dot(moveDirection, projVel) <= 0f;

        if (projVel.magnitude < maxAirSpeed || facingAway)
        {
            Vector3 airVel = moveDirection * stateMachine.movement.currentSpeed;

            if (!facingAway)
            {
                airVel = Vector3.ClampMagnitude(airVel, maxAirSpeed - projVel.magnitude);
            }
            else
            {
                airVel = Vector3.ClampMagnitude(airVel, maxAirSpeed + projVel.magnitude);
            }

            rb.AddForce(airVel.normalized, ForceMode.VelocityChange);
        }
        */
    }

    public Vector3 CalculateMoveDirection()
    {
        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        Vector3 camForward = new Vector3(stateMachine.cm.cameraForward.x, 0, stateMachine.cm.cameraForward.z);
        Vector3 camRight = new Vector3(stateMachine.cm.cameraRight.x, 0, stateMachine.cm.cameraRight.z);

        return camForward.normalized * verticalInput + camRight.normalized * horizontalInput;

        /*
        if(stateMachine.controller.input.RetrieveLookInput().magnitude > 0.5f && horizontalInput == 0 && verticalInput == 0)
        {
            return camForward.normalized;
        }
        else
        {
            return camForward.normalized * verticalInput + camRight.normalized * horizontalInput;
        }
        */
    }
}
