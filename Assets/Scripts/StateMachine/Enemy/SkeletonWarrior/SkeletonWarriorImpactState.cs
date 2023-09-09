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
        skeletonWarriorStateMachine.animator.CrossFadeInFixedTime(impactHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(skeletonWarriorStateMachine.animator, impactHash);
        if (normalizedTime >= 1f)
        {
            skeletonWarriorStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
    }
}
