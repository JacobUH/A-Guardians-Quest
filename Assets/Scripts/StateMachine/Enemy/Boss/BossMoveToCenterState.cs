using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveToCenterState : BossState
{
    public BossMoveToCenterState(BossStateMachine sm) : base(sm) { }

    private int runHash = Animator.StringToHash("Run");
    private float crossFixedDuration = 0.3f;

    private GameObject target;
    private Vector3 direction;
    public override void Enter()
    {
        target = GameObject.FindGameObjectWithTag("Center");
        PlayAnimation(runHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        direction = target.transform.position - sm.transform.position;
        float distance = Vector3.Distance(sm.transform.position, target.transform.position);
        if (distance > 0.1f)
        {
            FaceTargetInstantly(target);
            Move(direction.normalized * sm.chaseSpeed);
        }
        else sm.SwitchState(new BossSpecialState(sm));
    }
}
