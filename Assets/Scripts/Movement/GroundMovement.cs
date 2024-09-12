using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundMovement : MonoBehaviour
{
    public PlayerMovementStateMachine stateMachine;
    public BasicMovement playerMovement;

    public void Move()
    {
        MoveOnGround(CalculateMoveDirection());
    }

    public void MoveOnGround(Vector3 moveDirection)
    {
        if (stateMachine.slopeCheck.GetOnSlope())
        {
            stateMachine.movement.MovePlayer(CalculateSlopeDirection(moveDirection));

            stateMachine.rb.AddForce(stateMachine.slopeCheck.GetSlopeHit().normal.normalized * -1f, ForceMode.Force);
        }
        else
        {
            stateMachine.movement.MovePlayer(moveDirection);
        }
    }

    public Vector3 CalculateMoveDirection()
    {
        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        Vector3 camForward = new Vector3(stateMachine.cm.cameraForward.x, 0, stateMachine.cm.cameraForward.z);
        Vector3 camRight = new Vector3(stateMachine.cm.cameraRight.x, 0, stateMachine.cm.cameraRight.z);

        return camForward.normalized * verticalInput + camRight.normalized * horizontalInput;
    }

    private Vector3 CalculateSlopeDirection(Vector3 moveDirection)
    {
        return Vector3.ProjectOnPlane(moveDirection, stateMachine.slopeCheck.GetSlopeHit().normal).normalized;
    }
}
