using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonWarriorChasingState : EnemyChasingState
{
    public SkeletonWarriorChasingState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    private int runHash = Animator.StringToHash("Run");
    private float crossFixedDuration = 0.3f;
    private float intervalTimer;

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
        if (skeletonWarriorStateMachine.targetManager.GetCurrentTarget() == null) 
        { 
            enemyStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
        if (targetDistance <= 3f)
        {
            intervalTimer += Time.deltaTime;
            if (intervalTimer > 0.2f)
            {
                intervalTimer -= 0.2f;
                if (Random.Range(0, 100) < 30)
                {
                    enemyStateMachine.SwitchState(new SkeletonWarriorAttackingState(skeletonWarriorStateMachine, 2));
                }
            }
        }
        if (targetInRange)
        {
            if (Random.Range(0, 100) < 30)
            {
                enemyStateMachine.SwitchState(new SkeletonWarriorAttackingState(skeletonWarriorStateMachine, 1));
            }
            else
            {
                enemyStateMachine.SwitchState(new SkeletonWarriorAttackingState(skeletonWarriorStateMachine, 0));
            }
        }
    }
}
