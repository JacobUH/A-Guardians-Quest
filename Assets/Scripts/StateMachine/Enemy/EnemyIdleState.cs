using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

}
