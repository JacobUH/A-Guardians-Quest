using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBackStepState : BossState
{
    public BossBackStepState(BossStateMachine sm) : base(sm) { }

    private int backstepHash = Animator.StringToHash("BackStep");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        FaceTargetInstantly();
        PlayAnimation(backstepHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, backstepHash);
        if (normalizedTime >= 1f)
        {
            float r = Random.Range(0f, 100f);
            if (r < 50)
            {
                sm.SwitchState(new BossAttack3State(sm));
                return;
            }
            else
            {
                sm.SwitchState(new BossDashAttackState(sm, 0));
                return;
            }
        }
    }
}
