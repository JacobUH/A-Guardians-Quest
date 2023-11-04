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
        AttackHandler attackHandler = playerStateMachine.GetComponent<AttackHandler>();
        attackHandler.HitboxDisabled();
        attackHandler.DisabledSwordTrail();

        PlayAnimation(dieHash, crossFixedDuration);
        playerStateMachine.forceReceiver.enabled = false;
        playerStateMachine.controller.enabled = false;
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}
