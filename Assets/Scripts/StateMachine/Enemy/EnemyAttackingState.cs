using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private int attackHash = Animator.StringToHash("Attack1");
    private float crossFixedDuration = 0.1f;

    private GameObject currentTarget;
    public override void Enter()
    {
        currentTarget = enemyStateMachine.enemyTargetManager.GetCurrentTarget();
        if (currentTarget == null)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }
        enemyStateMachine.animator.CrossFadeInFixedTime(attackHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {

        float normalizedTime = GetNormalizedTime(enemyStateMachine.animator, attackHash);
        if (normalizedTime >= 1f)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
        }
    }
}
