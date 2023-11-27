using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVulnerableState : BossState
{
    public BossVulnerableState(BossStateMachine sm) : base(sm) { }

    private int inVulnerableHash = Animator.StringToHash("InVulnerable");
    private int vulnerableHash = Animator.StringToHash("Vulnerable");
    private int outVulnerableHash = Animator.StringToHash("OutVulnerable");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        PlayAnimation(inVulnerableHash, crossFixedDuration);
        sm.GetComponent<AttackHandler>().HitboxDisabled();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, outVulnerableHash);
        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossRageState(sm));
        }
    }
}
