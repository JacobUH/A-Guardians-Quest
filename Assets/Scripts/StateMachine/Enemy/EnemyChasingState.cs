using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyChasingState : EnemyState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    protected bool targetInRange;

    protected void ChaseTarget(float chaseStopRange)
    {
        float distance = enemyStateMachine.enemyTargetManager.GetDistanceToTarget();

        if (distance <= chaseStopRange && enemyStateMachine.enemyTargetManager.IsTargetInFront())
        {
            targetInRange = true;
        }
        else
        {
            targetInRange = false;
            FaceTarget(enemyStateMachine.changeDirectionSpeed);
            Move(enemyStateMachine.transform.forward * enemyStateMachine.chaseSpeed);
        }
    }
}
