using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackingState : EnemyState
{
    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
}
