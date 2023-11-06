using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorImpactState : EnemyImpactState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorImpactState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private int impactHash = Animator.StringToHash("Impact");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        PlayAnimation(impactHash, crossFixedDuration);
        skeletonWarriorStateMachine.GetComponent<AttackHandler>().HitboxDisabled();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        ApplyForce();
        float normalizedTime = GetNormalizedTime(skeletonWarriorStateMachine.animator, impactHash);
        if (normalizedTime >= 1f)
        {
            skeletonWarriorStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
    }
}
