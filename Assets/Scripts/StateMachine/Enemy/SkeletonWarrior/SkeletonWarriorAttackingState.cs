using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAttackingState : EnemyAttackingState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorAttackingState(SkeletonWarriorStateMachine skeletonWarriorStateMachine) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
    }

    private int attackHash = Animator.StringToHash("Attack1");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        enemyStateMachine.animator.CrossFadeInFixedTime(attackHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(enemyStateMachine.animator, attackHash);
        if (normalizedTime <= 0.2f)
        {
            FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.05f);
        }
        if (normalizedTime >= 1f)
        {
            enemyStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
    }
}
