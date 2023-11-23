using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2State : BossState
{
    public BossAttack2State(BossStateMachine sm) : base(sm) { }

    private int attackHash = Animator.StringToHash("Attack2");
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
            if (sm.targetManager.GetDistanceToTarget() < 5f)
            {
                sm.SwitchState(new BossBackStepState(sm));
            }
            else
            {
                sm.SwitchState(new BossAttack3State(sm));
            }
        }
    }
}
