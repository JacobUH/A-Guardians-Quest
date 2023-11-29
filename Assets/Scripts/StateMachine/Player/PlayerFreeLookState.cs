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
    private float footstepInterval;

    public override void Enter()
    {
        InputReader.Instance.EnableFreelookInputReader();
        InputReader.Instance.DpadUpButtonPressEvent += UseItem;
        InputReader.Instance.DpadRightButtonPressEvent += SwitchItem;
        InputReader.Instance.DpadLeftButtonPressEvent += QuickSwitchWeapon;
        InputReader.Instance.SouthButtonPressEvent += Jump;
        InputReader.Instance.EastButtonPressEvent += Dodge;
        InputReader.Instance.EastButtonLongPressEvent += Dash;
        InputReader.Instance.WestButtonPressEvent += NormalAttack;
        InputReader.Instance.NorthButtonPressEvent += StrongAttack;
        InputReader.Instance.WestButtonLongPressEvent += ChargeAttack;
        InputReader.Instance.RightStickPressEvent += SpanCameraFaceTarget;
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
        PlayAnimation(freelookHash, crossFixedDuration);
    }

    public override void Exit()
    {
        InputReader.Instance.DpadUpButtonPressEvent -= UseItem;
        InputReader.Instance.DpadRightButtonPressEvent -= SwitchItem;
        InputReader.Instance.DpadLeftButtonPressEvent -= QuickSwitchWeapon;
        InputReader.Instance.SouthButtonPressEvent -= Jump;
        InputReader.Instance.EastButtonPressEvent -= Dodge;
        InputReader.Instance.EastButtonLongPressEvent -= Dash;
        InputReader.Instance.WestButtonPressEvent -= NormalAttack;
        InputReader.Instance.NorthButtonPressEvent -= StrongAttack;
        InputReader.Instance.WestButtonLongPressEvent -= ChargeAttack;
        InputReader.Instance.RightStickPressEvent -= SpanCameraFaceTarget;
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cheat();
        }
        UpdateAnimator();
        HandleCameraMovement();
        HandlePlayerMovement();
        if (!playerStateMachine.groundChecker.IsGrounded) playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));

        footstepInterval += Time.deltaTime;
        if (InputReader.Instance.leftStickValue == Vector2.zero) return;
        if (playerStateMachine.walkMode || Mathf.Max(Mathf.Abs(InputReader.Instance.leftStickValue.x), Mathf.Abs(InputReader.Instance.leftStickValue.y)) < 0.7f)
        {
            if (footstepInterval > 0.9f)
            {
                if (!playerStateMachine.footstepSource.isPlaying) playerStateMachine.footstepSource.Play();
                footstepInterval -= 0.9f;
            }
        }
        else if (playerStateMachine.isDashing)
        {
            if (footstepInterval > 0.1f)
            {
                if (!playerStateMachine.footstepSource.isPlaying) playerStateMachine.footstepSource.Play();
                footstepInterval -= 0.1f;
            }
        }
        else
        {
            if (footstepInterval > 0.2f)
            {
                if (!playerStateMachine.footstepSource.isPlaying) playerStateMachine.footstepSource.Play();
                footstepInterval -= 0.2f;
            }
        }
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
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalSwordCombo, 0));
        }
        else if (weaponType == WeaponType.Bow)
        {
            InventorySlot arrowSlot = InventoryBox.Instance.CheckInventory("5003");
            if (arrowSlot == null || arrowSlot.quantity < 1)
            {
                Debug.Log($"Out of arrow.");
                return;
            }
            InventoryBox.Instance.RemoveItem("5003", 1);
            EventHandler.OnUseItemEvent("5003");
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

    private void StrongAttack()
    {
        playerStateMachine.isDashing = false;
        WeaponType weaponType = playerStateMachine.character.GetCurrentWeaponData().weaponType;
        if (weaponType == WeaponType.Sword)
        {
            playerStateMachine.swordMainHand.SetActive(true);
            playerStateMachine.bowBack.SetActive(true);
            playerStateMachine.swordBack.SetActive(false);
            playerStateMachine.bowMainHand.SetActive(false);
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.strongSwordAttackCombo, 0));
        }
        else return;
    }

    private void Dash()
    {
        playerStateMachine.isDashing = true;
    }

    private void UseItem()
    {
        string itemGuid = playerStateMachine.currentItemGuid;
        Debug.Log($"Try using item {itemGuid}");
        if (InventoryBox.Instance.RemoveItem(itemGuid, 1))
        {
            ConsumableItemData consumableItem = (ConsumableItemData)ItemDatabase.Instance.GetItemData(itemGuid);
            consumableItem.Use(playerStateMachine.gameObject);
            EventHandler.OnUseItemEvent(itemGuid);
        }
    }

    private void SwitchItem()
    {
        if (playerStateMachine.currentItemGuid == "1001")
        {
            playerStateMachine.currentItemGuid = "1002";
            GameObject.FindObjectOfType<QuickSlotManager>().UpdateCurrentItemInfo("1002");

        }
        else if (playerStateMachine.currentItemGuid == "1002")
        {
            playerStateMachine.currentItemGuid = "1001";
            GameObject.FindObjectOfType<QuickSlotManager>().UpdateCurrentItemInfo("1001");

        }
        
        Debug.Log($"Current Item {playerStateMachine.currentItemGuid}");
    }

    private void Cheat()
    {
        InventoryBox.Instance.AddItem("9999", 10);
        InventoryBox.Instance.AddItem("9998", 10);
        InventoryBox.Instance.AddItem("9997", 10);
        GameObject.FindObjectOfType<QuickSlotManager>().UpdateUI();
        CoinManager.Instance.UpdateUI();
    }
}
