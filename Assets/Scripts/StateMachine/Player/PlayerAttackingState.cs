using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    public PlayerAttackingState(PlayerStateMachine playerStateMachine, Attack attack) : base(playerStateMachine)
    {
        this.attack = attack;
        attackHash = Animator.StringToHash(attack.animationName);
    }

    private Attack attack;
    private float normalizedTime;
    private int attackHash;

    public override void Enter()
    {
        PlayAnimation(attackHash, attack.transitionDuration);
        InputReader.Instance.WestButtonPressEvent += TryComboNormalAttack;
    }

    public override void Exit()
    {
        InputReader.Instance.WestButtonPressEvent -= TryComboNormalAttack;
    }

    public override void Tick()
    {
        HandleCameraMovement();
        Move(Vector3.zero);
        GameObject target = playerStateMachine.targetManager.GetCurrentTarget();
        if (target != null)
        {
            FaceTarget(target);
        }

        normalizedTime = GetNormalizedTime(playerStateMachine.animator, attackHash);
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
        else
        {
            if (InputReader.Instance.isPressingWestButton) TryComboNormalAttack();
        }
    }

    private void TryComboNormalAttack()
    {
        if (attack.nextComboIndex == -1) return;
        if (normalizedTime < attack.nextComboEnableTime) return;

        Attack nextAttack = playerStateMachine.comboManager.normalCombo.attack[attack.nextComboIndex];
        playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, nextAttack));
    }
}
