using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorIdleState : EnemyIdleState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorIdleState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine) 
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private int idleHash = Animator.StringToHash("Idle");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        PlayAnimation(idleHash, crossFixedDuration);
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        if (GetCurrentTarget() != null)
        {
            skeletonWarriorStateMachine.SwitchState(new SkeletonWarriorChasingState(skeletonWarriorStateMachine));
        }

    }
}
