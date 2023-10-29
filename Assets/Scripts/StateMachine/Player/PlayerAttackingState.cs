using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    public PlayerAttackingState(PlayerStateMachine playerStateMachine, Combo combo, int attackIndex) : base(playerStateMachine)
    {
        this.attack = combo.attack[attackIndex];
        this.combo = combo;
        attackHash = Animator.StringToHash(attack.animationName);
    }

    private Attack attack;
    private Combo combo;
    private float normalizedTime;
    private int attackHash;

    public override void Enter()
    {
        PlayAnimation(attackHash, attack.transitionDuration);
        InputReader.Instance.WestButtonPressEvent += TryComboNormalAttack;
        InputReader.Instance.WestButtonLongPressEvent += ChargeAttack;
    }

    public override void Exit()
    {
        InputReader.Instance.WestButtonPressEvent -= TryComboNormalAttack;
        InputReader.Instance.WestButtonLongPressEvent -= ChargeAttack;
    }

    public override void Tick()
    {
        HandleCameraMovement();
        GameObject target = playerStateMachine.targetManager.GetCurrentTarget();
        if (target != null)
        {
            FaceTarget(target);
        }

        normalizedTime = GetNormalizedTime(playerStateMachine.animator, attackHash);
        if (normalizedTime >= attack.moveStartTime && normalizedTime < attack.moveEndTime)
        {
            Move(playerStateMachine.transform.forward * attack.moveForwardDistance);
        }

        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }

    private void TryComboNormalAttack()
    {
        if (attack.nextComboIndex == -1) return;
        if (normalizedTime < attack.nextComboEnableTime) return;

        playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, combo, attack.nextComboIndex));
    }

    private void ChargeAttack()
    {
        if (normalizedTime < attack.nextComboEnableTime) return;
        WeaponType weaponType = playerStateMachine.character.GetCurrentWeaponData().weaponType;
        if (weaponType == WeaponType.Sword)
        {
            playerStateMachine.swordMainHand.SetActive(true);
            playerStateMachine.bowBack.SetActive(true);
            playerStateMachine.swordBack.SetActive(false);
            playerStateMachine.bowMainHand.SetActive(false);
            playerStateMachine.SwitchState(new PlayerChargeAttackingState(playerStateMachine));
        }
        else return;
    }
}
