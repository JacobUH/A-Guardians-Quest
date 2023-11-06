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
        skeletonWarriorStateMachine.forceReceiver.enabled = false;
        skeletonWarriorStateMachine.controller.enabled = false;
        skeletonWarriorStateMachine.GetComponent<AttackHandler>().HitboxDisabled();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}
