using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerFallingState : PlayerState
{
    public PlayerFallingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int fallHash = Animator.StringToHash("Fall");
    private Vector3 momentumn;
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        momentumn = playerStateMachine.controller.velocity;
        momentumn.y = 0f;
        playerStateMachine.animator.CrossFadeInFixedTime(fallHash, crossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        HandleCameraMovement();
        Move(momentumn);
        if (playerStateMachine.controller.isGrounded)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
