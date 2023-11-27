using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialState : BossState
{
    public BossSpecialState(BossStateMachine sm) : base(sm) { }

    private int specialHash= Animator.StringToHash("Special");
    private float crossFixedDuration = 0.3f;

    private bool finished;
    public override void Enter()
    {
        FaceTargetInstantly();
        PlayAnimation(specialHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(sm.animator, specialHash);
        if (normalizedTime >= 0.6f)
        {
            SpawnSkill();
        }
        if (normalizedTime >= 1f)
        {
            if (sm.specialUseCount < 3) sm.SwitchState(new BossSpecialState(sm));
            else
            {
                sm.specialUseCount = 0;
                sm.SwitchState(new BossApproachingState(sm));
            }
        }
    }

    private void SpawnSkill()
    {
        if (!finished)
        {
            sm.specialUseCount++;
            GameObject obj = GameObject.Instantiate(sm.firewallPrefab);
            obj.transform.position = sm.transform.position;
            obj.SetActive(true);
            finished = true;
        }
    }
}
