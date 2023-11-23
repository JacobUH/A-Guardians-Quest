using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossChasingState : BossState
{
    public BossChasingState(BossStateMachine sm) : base(sm) { }

    private int runHash = Animator.StringToHash("Run");
    private float crossFixedDuration = 0.3f;

    public override void Enter()
    {
        PlayAnimation(runHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        ChaseTarget(4f);
        if (sm.targetInRange)
        {
            int r = Random.Range(0, 100);
            if (r < 75) sm.SwitchState(new BossDashAttackState(sm, 1));
            else sm.SwitchState(new BossBackStepState(sm));
        }
    }

    private void ChaseTarget(float chaseStopRange)
    {
        sm.targetDistance = sm.targetManager.GetDistanceToTarget();

        if (sm.targetDistance <= chaseStopRange)
        {
            sm.targetInRange = true;
        }
        else
        {
            sm.targetInRange = false;
            FaceTarget();
            Move(sm.transform.forward * sm.chaseSpeed);
        }
    }
}
