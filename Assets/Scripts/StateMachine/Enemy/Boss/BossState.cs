using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : State
{
    public BossStateMachine sm;

    public BossState(BossStateMachine sm)
    {
        this.sm = sm;
    }

    public void Move(Vector3 movement)
    {
        sm.controller.Move((movement + sm.forceReceiver.GetForce()) * Time.deltaTime);
    }

    public void FaceTarget()
    {
        GameObject target = GetCurrentTarget();
        if (target == null) return;

        Vector3 lookPos = target.transform.position - sm.transform.position;
        lookPos.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        sm.transform.rotation = Quaternion.Slerp(sm.transform.rotation, lookRotation, sm.changeDirectionSpeed * Time.deltaTime);
    }

    public void FaceTargetInstantly()
    {
        GameObject target = GetCurrentTarget();
        if (target == null) return;

        Vector3 lookPos = target.transform.position - sm.transform.position;
        lookPos.y = 0f;
        sm.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void FaceTargetInstantly(GameObject target)
    {
        Vector3 lookPos = target.transform.position - sm.transform.position;
        lookPos.y = 0f;
        sm.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void ChangeDirection(Vector3 movement)
    {
        sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, Quaternion.LookRotation(movement), sm.changeDirectionSpeed * Time.deltaTime);
    }

    public void ChangeDirectionInstantly(Vector3 movement)
    {
        sm.transform.rotation = Quaternion.LookRotation(movement);
    }

    public GameObject GetCurrentTarget()
    {
        return sm.targetManager.GetCurrentTarget();
    }

    public void PlayAnimation(int animationHash, float crossFixedDuration)
    {
        sm.animator.CrossFadeInFixedTime(animationHash, crossFixedDuration);
    }
}
