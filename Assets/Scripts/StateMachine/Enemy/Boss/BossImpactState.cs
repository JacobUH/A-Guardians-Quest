using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImpactState : BossState
{
    public BossImpactState(BossStateMachine sm) : base(sm) { }

    private int impactHash = Animator.StringToHash("Impact");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        PlayAnimation(impactHash, crossFixedDuration);
        sm.GetComponent<AttackHandler>().HitboxDisabled();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        ApplyForce();
        float normalizedTime = GetNormalizedTime(sm.animator, impactHash);
        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossVulnerableState(sm));
        }
    }

    private void ApplyForce()
    {
        Move(Vector3.zero);
    }
}
