using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyImpactState : EnemyState
{
    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public void ApplyForce()
    {
        Move(Vector3.zero);
    }
}
