using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1State : BossState
{
    public BossAttack1State(BossStateMachine sm) : base(sm) { }

    private int attackHash = Animator.StringToHash("Attack1");
    private float crossFixedDuration = 0.2f;

    public override void Enter()
    {
        FaceTargetInstantly();
        PlayAnimation(attackHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, attackHash);
        if (normalizedTime >= 0.7f)
        {
            sm.SwitchState(new BossAttack2State(sm));
        }
    }
}
