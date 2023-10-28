using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int jumpHash = Animator.StringToHash("Jump");
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.forceReceiver.Jump();
        PlayAnimation(jumpHash, crossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        HandlePlayerMovement();
        HandleCameraMovement();
        if (playerStateMachine.controller.velocity.y <= 0)
        {
            playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));
            return;
        }
    }
}