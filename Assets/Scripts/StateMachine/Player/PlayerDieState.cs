using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int dieHash = Animator.StringToHash("Die");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        playerStateMachine.character.isDead = true;
        playerStateMachine.animator.CrossFadeInFixedTime(dieHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}
