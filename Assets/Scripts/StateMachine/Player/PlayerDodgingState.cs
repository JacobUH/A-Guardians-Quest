using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerState
{
    public PlayerDodgingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int dodgeHash = Animator.StringToHash("Dodge");
    private Vector3 movement;
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        movement = CalculateMovement();
        ChangeDirectionInstantly(movement);
        playerStateMachine.animator.CrossFadeInFixedTime(dodgeHash, crossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        HandleCameraMovement();
        Move(movement * playerStateMachine.dodgeSpeed);
        float normalizedTime = GetNormalizedTime(playerStateMachine.animator, dodgeHash);
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
