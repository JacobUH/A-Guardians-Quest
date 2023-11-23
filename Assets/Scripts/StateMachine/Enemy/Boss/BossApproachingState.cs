using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossApproachingState : BossState
{
    public BossApproachingState(BossStateMachine sm) : base(sm) { }

    private int walkHash = Animator.StringToHash("Walk");
    private float crossFixedDuration = 0.3f;
    private float timer;

    public override void Enter()
    {
        timer = 0f;
        PlayAnimation(walkHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            sm.SwitchState(new BossChasingState(sm));
        }
        ChaseTarget(4f);
        if (sm.targetInRange) sm.SwitchState(new BossAttack1State(sm));
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
            Move(sm.transform.forward * 0.1f);
        }
    }
}
