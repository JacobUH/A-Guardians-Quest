using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    public PlayerStateMachine playerStateMachine;

    public PlayerState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public void Move(Vector3 movement)
    {
        playerStateMachine.controller.Move((movement + playerStateMachine.forceReceiver.GetForce()) * Time.deltaTime);
    }

    public Vector3 CalculateMovement()
    {
        Vector2 movementInput = InputReader.Instance.leftStickValue;
        Vector3 forward = playerStateMachine.mainCameraTransform.forward;
        Vector3 right = playerStateMachine.mainCameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return forward * movementInput.y + right * movementInput.x;
    }

    public void FaceTarget(GameObject target)
    {
        Vector3 lookPos = target.transform.position - playerStateMachine.transform.position;
        lookPos.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        playerStateMachine.transform.rotation = Quaternion.Slerp(playerStateMachine.transform.rotation, lookRotation, playerStateMachine.changeDirectionSpeed * Time.deltaTime);
    }

    public void ChangeDirection(Vector3 movement)
    {
        playerStateMachine.transform.rotation = Quaternion.Lerp(playerStateMachine.transform.rotation, Quaternion.LookRotation(movement), playerStateMachine.changeDirectionSpeed * Time.deltaTime);
    }

    public void ChangeDirectionInstantly(Vector3 movement)
    {
        playerStateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public void HandleCameraMovement()
    {
        if (InputReader.Instance.rightStickValue != Vector2.zero)
        {
            CameraController.Instance.RotateCamera(InputReader.Instance.rightStickValue);
        }
        if (InputReader.Instance.isPressingLeftShoulder)
        {
            CameraController.Instance.ZoomOut();
        }
        if (InputReader.Instance.isPressingRightShoulder)
        {
            CameraController.Instance.ZoomIn();
        }
    }
}