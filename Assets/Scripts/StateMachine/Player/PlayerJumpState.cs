using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int jumpHash = Animator.StringToHash("Jump");
    private Vector3 movement;
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.forceReceiver.Jump();
        movement = CalculateMovement();
        if (movement != Vector3.zero) ChangeDirectionInstantly(movement);
        playerStateMachine.animator.CrossFadeInFixedTime(jumpHash, crossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        HandleCameraMovement();
        Move(movement * playerStateMachine.movementSpeed);
        if (playerStateMachine.controller.velocity.y < 0)
        {
            playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));
            return;
        }
    }
}