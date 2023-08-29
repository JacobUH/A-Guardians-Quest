using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerState
{
    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int freelookHash = Animator.StringToHash("FreeLookBlendTree");
    private int blendSpeedHash = Animator.StringToHash("FreeLookBlendSpeed");
    private float crossFixedDuration = 0.1f;
    private float blendValue;
    private Vector3 movement;

    public override void Enter()
    {
        InputReader.Instance.EnableFreelookInputReader();
        playerStateMachine.animator.CrossFadeInFixedTime(freelookHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        UpdateAnimator();
        HandleCameraMovement();
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        movement = CalculateMovement();
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            Move(Vector3.zero);
        }
        else
        {
            Move(movement * playerStateMachine.movementSpeed);
            ChangeDirection(movement);
        }
    }

    private void HandleCameraMovement()
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

    private void UpdateAnimator()
    {
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            blendValue = 0f;
        }
        else blendValue = Mathf.Max(Mathf.Abs(InputReader.Instance.leftStickValue.x), Mathf.Abs(InputReader.Instance.leftStickValue.y));

        playerStateMachine.animator.SetFloat(blendSpeedHash, blendValue, 0.1f, Time.deltaTime);
    }
}
