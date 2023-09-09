using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDieState : EnemyState
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
}
