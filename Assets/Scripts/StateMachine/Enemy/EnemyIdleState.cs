using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private int idleHash = Animator.StringToHash("Idle");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        enemyStateMachine.animator.CrossFadeInFixedTime(idleHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        if (enemyStateMachine.enemyTargetManager.GetCurrentTarget() != null)
        {
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
        }
    }
}
