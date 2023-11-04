using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAttackingState : EnemyAttackingState
{
    private SkeletonWarriorStateMachine skeletonWarriorStateMachine;
    public SkeletonWarriorAttackingState(SkeletonWarriorStateMachine skeletonWarriorStateMachine, int attackIndex) : base(skeletonWarriorStateMachine)
    {
        this.skeletonWarriorStateMachine = skeletonWarriorStateMachine;
        this.attackIndex = attackIndex;
    }

    private int attackIndex;
    private int attackHash;
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        if (attackIndex == 0) attackHash = Animator.StringToHash("Attack1");
        else if (attackIndex == 1)
        {
            attackHash = Animator.StringToHash("Attack2");
            skeletonWarriorStateMachine.character.isUnflinching = true;
        }
        else if (attackIndex == 2)
        {
            attackHash = Animator.StringToHash("JumpAttack");
            skeletonWarriorStateMachine.character.isUnflinching = true;
        }
        PlayAnimation(attackHash, crossFixedDuration);
    }

    public override void Exit()
    {
        skeletonWarriorStateMachine.character.isUnflinching = false;
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(enemyStateMachine.animator, attackHash);
        if (attackIndex == 0)
        {
            Move(Vector3.zero);
            if (normalizedTime <= 0.2f)
            {
                FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.05f);
            }
            if (normalizedTime >= 1f)
            {
                enemyStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
            }
        }
        if (attackIndex == 1)
        {
            if (normalizedTime > 0.14f && normalizedTime < 0.2f)
            {
                Move(skeletonWarriorStateMachine.transform.forward * 2f); 
                FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.2f);
            }
            if (normalizedTime > 0.37f && normalizedTime < 0.43f)
            {
                Move(skeletonWarriorStateMachine.transform.forward * 2f);
                FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.1f);
            }
            if (normalizedTime > 0.57f && normalizedTime < 0.65f)
            {
                Move(skeletonWarriorStateMachine.transform.forward * 2f);
                FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.05f);
            }
        }
        if (attackIndex == 2)
        {
            if (normalizedTime > 0.15f && normalizedTime < 0.42f)
            {
                Move(skeletonWarriorStateMachine.transform.forward * 4f);
            }
        }

        if (normalizedTime <= 0.2f)
        {
            FaceTarget(skeletonWarriorStateMachine.changeDirectionSpeed * 0.05f);
        }
        if (normalizedTime >= 1f)
        {
            enemyStateMachine.SwitchState(new SkeletonWarriorIdleState(skeletonWarriorStateMachine));
        }
    }
}
