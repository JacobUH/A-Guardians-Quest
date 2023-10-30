using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerState
{
    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int freelookHash = Animator.StringToHash("FreeLookBlendTree");
    private int blendSpeedHash = Animator.StringToHash("FreeLookBlendSpeed");
    private float crossFixedDuration = 0.1f;
    private float blendValue;
    private Vector3 movement;

    public override void Enter()
    {
        InputReader.Instance.EnableFreelookInputReader();
        InputReader.Instance.DpadUpButtonPressEvent += UseItem;
        InputReader.Instance.DpadLeftButtonPressEvent += QuickSwitchWeapon;
        InputReader.Instance.SouthButtonPressEvent += Jump;
        InputReader.Instance.EastButtonPressEvent += Dodge;
        InputReader.Instance.EastButtonLongPressEvent += Dash;
        InputReader.Instance.WestButtonPressEvent += NormalAttack;
        InputReader.Instance.WestButtonLongPressEvent += ChargeAttack;
        InputReader.Instance.RightStickPressEvent += SpanCameraFaceTarget;
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
        //InputReader.Instance.DpadLeftButtonPressEvent += LockOnPreviousTarget;
        //InputReader.Instance.DpadRightButtonPressEvent += LockOnNextTarget;
        PlayAnimation(freelookHash, crossFixedDuration);
    }

    public override void Exit()
    {
        InputReader.Instance.DpadUpButtonPressEvent -= UseItem;
        InputReader.Instance.DpadLeftButtonPressEvent -= QuickSwitchWeapon;
        InputReader.Instance.SouthButtonPressEvent -= Jump;
        InputReader.Instance.EastButtonPressEvent -= Dodge;
        InputReader.Instance.EastButtonLongPressEvent -= Dash;
        InputReader.Instance.WestButtonPressEvent -= NormalAttack;
        InputReader.Instance.WestButtonLongPressEvent -= ChargeAttack;
        InputReader.Instance.RightStickPressEvent -= SpanCameraFaceTarget;
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
        //InputReader.Instance.DpadLeftButtonPressEvent -= LockOnPreviousTarget;
        //InputReader.Instance.DpadRightButtonPressEvent -= LockOnNextTarget;
    }

    public override void Tick()
    {
        UpdateAnimator();
        HandleCameraMovement();
        HandlePlayerMovement();
        if (!playerStateMachine.controller.isGrounded) playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));
    }

    private void UpdateAnimator()
    {
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            playerStateMachine.animator.SetFloat(blendSpeedHash, 0f, 0.1f, Time.deltaTime);
            return;
        }
        if (playerStateMachine.walkMode)
        {
            blendValue = 0.5f;
        }
        else
        {
            blendValue = Mathf.Max(Mathf.Abs(InputReader.Instance.leftStickValue.x), Mathf.Abs(InputReader.Instance.leftStickValue.y));
            if (blendValue > 0.7f) blendValue = 1f;
            else blendValue = 0.5f;
        }

        playerStateMachine.animator.SetFloat(blendSpeedHash, blendValue, 0.1f, Time.deltaTime);
    }

    private void NormalAttack()
    {
        playerStateMachine.isDashing = false;
        WeaponType weaponType = playerStateMachine.character.GetCurrentWeaponData().weaponType;
        if (weaponType == WeaponType.Sword)
        {
            playerStateMachine.swordMainHand.SetActive(true);
            playerStateMachine.bowBack.SetActive(true);
            playerStateMachine.swordBack.SetActive(false);
            playerStateMachine.bowMainHand.SetActive(false);
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalSwordCombo, 0));
        }
        else if (weaponType == WeaponType.Bow)
        {
            playerStateMachine.swordMainHand.SetActive(false);
            playerStateMachine.bowBack.SetActive(false);
            playerStateMachine.swordBack.SetActive(true);
            playerStateMachine.bowMainHand.SetActive(true);
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalBowCombo, 0));
        }
        else return;
    }

    private void ChargeAttack()
    {
        playerStateMachine.isDashing = false;
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

    private void Jump()
    {
        playerStateMachine.SwitchState(new PlayerJumpState(playerStateMachine));
    }

    private void Dodge()
    {
        if (CalculateMovement() == Vector3.zero) return;
        playerStateMachine.isDashing = false;
        playerStateMachine.SwitchState(new PlayerDodgingState(playerStateMachine));
    }

    private void Dash()
    {
        playerStateMachine.isDashing = true;
    }

    private void UseItem()
    {
        string itemGuid = "1001"; //RedPotion
        if (InventoryBox.Instance.RemoveItem(itemGuid, 1))
        {
            ConsumableItemData consumableItem = (ConsumableItemData)ItemDatabase.Instance.GetItemData(itemGuid);
            consumableItem.Use(playerStateMachine.gameObject);
            EventHandler.OnUseItemEvent(itemGuid);
        }
    }
}
