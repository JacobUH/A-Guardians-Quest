using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerFallingState : PlayerState
{
    public PlayerFallingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int fallHash = Animator.StringToHash("Fall");
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.isFalling = true;
        PlayAnimation(fallHash, crossFadeDuration);
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
    }

    public override void Exit()
    {
        playerStateMachine.isFalling = false;
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
    }

    public override void Tick()
    {
        HandlePlayerMovement();
        HandleCameraMovement();
        if (playerStateMachine.groundChecker.IsGrounded)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
