using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttackAState : BossState
{
    public BossDashAttackAState(BossStateMachine sm) : base(sm) { }

    private int dashAttackAHash = Animator.StringToHash("DashAttackA");
    private float crossFixedDuration = 0.2f;

    public override void Enter()
    {
        FaceTargetInstantly();
        PlayAnimation(dashAttackAHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = 0f;
        normalizedTime = GetNormalizedTime(sm.animator, dashAttackAHash);

        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossIdleState(sm));
        }
        else if (normalizedTime <= 0.5f)
        {
            FaceTarget();
        }
        else
        {
            Move(sm.transform.forward * 6f);
        }
    }
}
