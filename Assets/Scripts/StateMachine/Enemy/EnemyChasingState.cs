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
        float distance = enemyStateMachine.targetManager.GetDistanceToTarget();

        if (distance <= chaseStopRange)
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
