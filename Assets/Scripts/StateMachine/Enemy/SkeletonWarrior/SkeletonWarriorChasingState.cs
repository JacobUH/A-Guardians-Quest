using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonWarriorChasingState : EnemyChasingState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorChasingState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private int runHash = Animator.StringToHash("Run");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        PlayAnimation(runHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        ChaseTarget(1.5f);
        if (skeletonWarriorStateMachine.enemyTargetManager.GetCurrentTarget() == null) 
        { 
            enemyStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
        if (targetInRange)
        {
            enemyStateMachine.SwitchState(new SkeletonWarriorAttackingState(skeletonWarriorStateMachine));
        }
    }
}
