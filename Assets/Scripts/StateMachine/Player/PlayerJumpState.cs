using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int jumpHash = Animator.StringToHash("Jump");
    private Vector3 momentumn;
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.forceReceiver.Jump();
        momentumn = playerStateMachine.controller.velocity;
        momentumn.y = 0f;
        playerStateMachine.animator.CrossFadeInFixedTime(jumpHash, crossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        Move(momentumn);
        if (playerStateMachine.controller.velocity.y < 0)
        {
            playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));
            return;
        }
    }
}