using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyChasingState : EnemyState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    protected bool targetInRange;
    protected float targetDistance;

    protected void ChaseTarget(float chaseStopRange)
    {
        targetDistance = enemyStateMachine.targetManager.GetDistanceToTarget();

        if (targetDistance <= chaseStopRange)
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
