using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State
{
    public EnemyStateMachine enemyStateMachine;

    public EnemyState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    public void Move(Vector3 movement)
    {
        enemyStateMachine.controller.Move((movement + enemyStateMachine.forceReceiver.GetForce()) * Time.deltaTime);
    }

    public void FaceTarget(float changeDirectionSpeed)
    {
        GameObject target = GetCurrentTarget();
        if (target == null) return;
        FaceTarget(target, changeDirectionSpeed);
    }

    public void FaceTarget(GameObject target, float changeDirectionSpeed)
    {
        Vector3 lookPos = target.transform.position - enemyStateMachine.transform.position;
        lookPos.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        enemyStateMachine.transform.rotation = Quaternion.Slerp(enemyStateMachine.transform.rotation, lookRotation, enemyStateMachine.changeDirectionSpeed * Time.deltaTime);
    }
    public void FaceTargetInstantly(Vector3 target)
    {
        Vector3 lookPos = target - enemyStateMachine.transform.position;
        lookPos.y = 0f;
        enemyStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void FaceTargetInstantly(GameObject target)
    {
        Vector3 lookPos = target.transform.position - enemyStateMachine.transform.position;
        lookPos.y = 0f;
        enemyStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void ChangeDirection(Vector3 movement)
    {
        enemyStateMachine.transform.rotation = Quaternion.Lerp(enemyStateMachine.transform.rotation, Quaternion.LookRotation(movement), enemyStateMachine.changeDirectionSpeed * Time.deltaTime);
    }

    public void ChangeDirectionInstantly(Vector3 movement)
    {
        enemyStateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public GameObject GetCurrentTarget()
    {
        return enemyStateMachine.targetManager.GetCurrentTarget();
    }

    public void PlayAnimation(int animationHash, float crossFixedDuration)
    {
        enemyStateMachine.animator.CrossFadeInFixedTime(animationHash, crossFixedDuration);
    }
}
