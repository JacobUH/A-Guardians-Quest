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
        InputReader.Instance.NorthButtonPressEvent += StrongAttack;
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
        InputReader.Instance.EastButtonPressEvent += Dodge;
        InputReader.Instance.SouthButtonPressEvent += Jump;
        InputReader.Instance.DpadLeftButtonPressEvent += QuickSwitchWeapon;
    }

    public override void Exit()
    {
        InputReader.Instance.WestButtonPressEvent -= TryComboNormalAttack;
        InputReader.Instance.WestButtonLongPressEvent -= ChargeAttack;
        InputReader.Instance.NorthButtonPressEvent -= StrongAttack;
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
        InputReader.Instance.EastButtonPressEvent -= Dodge;
        InputReader.Instance.SouthButtonPressEvent -= Jump;
        InputReader.Instance.DpadLeftButtonPressEvent -= QuickSwitchWeapon;
    }

    public override void Tick()
    {
        HandleCameraMovement();
        GameObject target = playerStateMachine.targetManager.GetCurrentTarget();
        if (target == null)
        {
            target = playerStateMachine.targetManager.GetNearestTarget();
        }
        if (target != null)
        {
            if (playerStateMachine.character.GetCurrentWeaponData().weaponType == WeaponType.Sword)
            {
                if (Vector3.Distance(target.transform.position, playerStateMachine.transform.position) > 1.5f)
                {
                    Move(playerStateMachine.transform.forward);
                }
            }
            FaceTargetInstantly(target);
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

        WeaponType weaponType = playerStateMachine.character.GetCurrentWeaponData().weaponType;
        if (weaponType == WeaponType.Sword)
        {
            if (combo == playerStateMachine.comboManager.normalSwordCombo)
            {
                playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, combo, attack.nextComboIndex));
            }
            else 
            { 
                playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalSwordCombo, 0)); 
            }
                   
        }
        else
        if (weaponType == WeaponType.Bow)
        {
            InventorySlot arrowSlot = InventoryBox.Instance.CheckInventory("5003");
            if (arrowSlot == null || arrowSlot.quantity < 1)
            {
                Debug.Log($"Out of arrow.");
                return;
            }
            InventoryBox.Instance.RemoveItem("5003", 1);
            EventHandler.OnUseItemEvent("5003");
            if (combo == playerStateMachine.comboManager.normalBowCombo)
            {
                playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, combo, attack.nextComboIndex));
            }
            else
            {
                playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalBowCombo, 0));
            }
        }
    }

    private void ChargeAttack()
    {
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

    private void StrongAttack()
    {
        if (attack.nextStrongComboIndex == -1) return;
        if (normalizedTime < attack.nextComboEnableTime) return;

        WeaponType weaponType = playerStateMachine.character.GetCurrentWeaponData().weaponType;
        if (weaponType == WeaponType.Sword)
        {
            playerStateMachine.swordMainHand.SetActive(true);
            playerStateMachine.bowBack.SetActive(true);
            playerStateMachine.swordBack.SetActive(false);
            playerStateMachine.bowMainHand.SetActive(false);
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.strongSwordAttackCombo, attack.nextStrongComboIndex));
        }
        else return;
    }
}
