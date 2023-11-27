using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttackBState : BossState
{
    public BossDashAttackBState(BossStateMachine sm) : base(sm) { }

    private int dashAttackBHash = Animator.StringToHash("DashAttackB");
    private float crossFixedDuration = 0.2f;

    public override void Enter()
    {
        FaceTargetInstantly();
        PlayAnimation(dashAttackBHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = 0f;
        normalizedTime = GetNormalizedTime(sm.animator, dashAttackBHash);

        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossIdleState(sm));
        }
        else if (normalizedTime <= 0.5f)
        {
            FaceTarget();
        }
    }
}
