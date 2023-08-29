using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();

    protected float GetNormalizedTime(Animator animator, int tagHash)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.tagHash == tagHash)
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.tagHash == tagHash)
        {
            return currentInfo.normalizedTime;
        }
        else return 0f;
    }

}
