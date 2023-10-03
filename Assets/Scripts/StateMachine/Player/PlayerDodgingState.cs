using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerState
{
    public PlayerDodgingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int dodgeHash = Animator.StringToHash("Dodge");
    private Vector3 movement;
    private const float crossFadeDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.character.isInvincible = true;
        movement = CalculateMovement();
        ChangeDirectionInstantly(movement);
        PlayAnimation(dodgeHash, crossFadeDuration);
    }

    public override void Exit()
    {
        playerStateMachine.character.isInvincible = false;
    }

    public override void Tick()
    {
        HandleCameraMovement();
        float normalizedTime = GetNormalizedTime(playerStateMachine.animator, dodgeHash);
        if (normalizedTime <= 0.7f)
        {
            Move(movement * playerStateMachine.dodgeSpeed);
        }
        else
        {
            Move(Vector3.zero);
        }
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
