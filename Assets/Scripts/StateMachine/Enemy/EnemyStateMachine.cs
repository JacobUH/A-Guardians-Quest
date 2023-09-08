using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyTargetManager enemyTargetManager;

    public override void Start()
    {
        base.Start();
        enemyTargetManager = GetComponent<EnemyTargetManager>();
        SwitchState(new EnemyIdleState(this));
    }
}
