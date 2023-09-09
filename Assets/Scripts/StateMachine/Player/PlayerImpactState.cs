using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerState
{
    public PlayerImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int impactHash = Animator.StringToHash("Impact");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.animator.CrossFadeInFixedTime(impactHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(playerStateMachine.animator, impactHash);
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
