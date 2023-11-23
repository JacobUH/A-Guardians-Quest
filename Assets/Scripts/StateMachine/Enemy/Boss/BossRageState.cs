using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRageState : BossState
{
    public BossRageState(BossStateMachine sm) : base(sm) { }

    private int rageHash = Animator.StringToHash("Rage");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        PlayAnimation(rageHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, rageHash);
        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossIdleState(sm));
        }
    }
}
