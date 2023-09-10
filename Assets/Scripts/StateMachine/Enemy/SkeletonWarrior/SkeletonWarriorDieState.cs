using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorDieState : EnemyDieState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorDieState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private int dieHash = Animator.StringToHash("Die");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        PlayAnimation(dieHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}
