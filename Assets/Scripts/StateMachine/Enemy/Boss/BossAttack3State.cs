using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3State : BossState
{
    public BossAttack3State(BossStateMachine sm) : base(sm) { }

    private int attackHash = Animator.StringToHash("Attack3");
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
        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossIdleState(sm));
        }
        else if (normalizedTime <= 0.4f)
        {
            FaceTarget();
        }
    }
}
