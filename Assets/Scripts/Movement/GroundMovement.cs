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
        // Movement on slope
        if (stateMachine.groundCheck.GetOnSlope())
        {
            stateMachine.movement.MovePlayer(CalculateSlopeDirection(moveDirection));

            if(stateMachine.rb.velocity.y < 0)
            {
                stateMachine.rb.AddForce(stateMachine.groundCheck.GetGroundNormal().normalized * -10f, ForceMode.Force);
                //stateMachine.rb.AddForce(Vector3.down * 20f, ForceMode.Force);
            }
            else if(stateMachine.rb.velocity.y > 0)
            {
                stateMachine.rb.AddForce(stateMachine.groundCheck.GetGroundNormal().normalized * -30f, ForceMode.Force);
                //stateMachine.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
            else
            {
                stateMachine.rb.AddForce(stateMachine.groundCheck.GetGroundNormal().normalized * -20f, ForceMode.Force);
                //stateMachine.rb.AddForce(Vector3.down * 20f, ForceMode.Force);
            }
        }
        // Movement on flat ground
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
        return Vector3.ProjectOnPlane(moveDirection, stateMachine.groundCheck.GetGroundNormal()).normalized;
    }
}
