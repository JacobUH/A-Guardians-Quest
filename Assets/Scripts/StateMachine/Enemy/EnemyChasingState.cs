using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private int runHash = Animator.StringToHash("Run");
    private float crossFixedDuration = 0.3f;

    private GameObject currentTarget;
    private float distance;
    public override void Enter()
    {
        enemyStateMachine.animator.CrossFadeInFixedTime(runHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        currentTarget = enemyStateMachine.enemyTargetManager.GetCurrentTarget();
        if (currentTarget == null)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }

        distance = enemyStateMachine.enemyTargetManager.GetDistanceToTarget();

        if (distance > enemyStateMachine.attackRange)
        {
            FaceTarget(currentTarget);
            Move(enemyStateMachine.transform.forward * enemyStateMachine.runSpeed);
        }
        else if (!enemyStateMachine.enemyTargetManager.IsTargetInFront())
        {
            FaceTarget(currentTarget);
        }
        else
        {
            enemyStateMachine.SwitchState(new EnemyAttackingState(enemyStateMachine));
        }
    }
}
