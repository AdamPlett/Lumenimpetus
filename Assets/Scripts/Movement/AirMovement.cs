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
    }

    public Vector3 CalculateMoveDirection()
    {
        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        Vector3 camForward = new Vector3(stateMachine.cm.cameraForward.x, 0, stateMachine.cm.cameraForward.z);
        Vector3 camRight = new Vector3(stateMachine.cm.cameraRight.x, 0, stateMachine.cm.cameraRight.z);

        return camForward.normalized * verticalInput + camRight.normalized * horizontalInput;
    }
}
