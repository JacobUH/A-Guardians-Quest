using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttackState : BossState
{
    public BossDashAttackState(BossStateMachine sm, int version) : base(sm) 
    { 
        this.version = version;
    }

    private int dashAttackAHash = Animator.StringToHash("DashAttackA");
    private int dashAttackBHash = Animator.StringToHash("DashAttackB");
    private float crossFixedDuration = 0.2f;
    private int version = 0;

    public override void Enter()
    {
        FaceTargetInstantly();
        if (version == 0) PlayAnimation(dashAttackAHash, crossFixedDuration);
        else PlayAnimation(dashAttackBHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = 0f;
        if (version == 0) normalizedTime = GetNormalizedTime(sm.animator, dashAttackAHash);
        else normalizedTime = GetNormalizedTime(sm.animator, dashAttackBHash);

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
            if (version == 0) Move(sm.transform.forward * 3.5f);
        }
    }
}
