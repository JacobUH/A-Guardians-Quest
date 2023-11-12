using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int jumpHash = Animator.StringToHash("Jump");
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        AttackHandler attackHandler = playerStateMachine.GetComponent<AttackHandler>();
        attackHandler.HitboxDisabled();
        attackHandler.DisabledSwordTrail();

        playerStateMachine.forceReceiver.Jump();
        PlayAnimation(jumpHash, crossFadeDuration);
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
    }

    public override void Exit()
    {
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
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