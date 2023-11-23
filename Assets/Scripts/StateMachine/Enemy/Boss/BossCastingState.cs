using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastingState : BossState
{
    public BossCastingState(BossStateMachine sm) : base(sm) { }

    private int castHash = Animator.StringToHash("Cast");
    private float crossFixedDuration = 0.2f;

    public override void Enter()
    {
        FaceTargetInstantly();
        GameObject fireballA = GameObject.Instantiate(sm.fireballPrefab);
        GameObject fireballB = GameObject.Instantiate(sm.fireballPrefab);
        fireballA.transform.position = sm.transform.position + sm.transform.up * 2f + sm.transform.right * 1.5f;
        fireballA.transform.rotation = Quaternion.LookRotation(sm.transform.right);
        fireballB.transform.position = sm.transform.position + sm.transform.up * 2f + sm.transform.right * -1.5f;
        fireballB.transform.rotation = Quaternion.LookRotation(sm.transform.right * -1f);
        fireballA.SetActive(true);
        fireballB.SetActive(true);
        PlayAnimation(castHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, castHash);
        if (normalizedTime >= 1f)
        {
            sm.SwitchState(new BossIdleState(sm));
        }
    }
}
