using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    public PlayerStateMachine playerStateMachine;

    public PlayerState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public void Move(Vector3 movement)
    {
        playerStateMachine.controller.Move((movement + playerStateMachine.forceReceiver.GetForce() + playerStateMachine.slideDirection) * Time.deltaTime);
    }

    public Vector3 CalculateMovement()
    {
        Vector2 movementInput = InputReader.Instance.leftStickValue;
        Vector3 forward = playerStateMachine.mainCameraTransform.forward;
        Vector3 right = playerStateMachine.mainCameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return forward * movementInput.y + right * movementInput.x;
    }

    public void HandlePlayerMovement()
    {
        Vector3 movement = CalculateMovement();
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            playerStateMachine.isDashing = false;
            Move(Vector3.zero);
        }
        else
        {
            if (playerStateMachine.walkMode) Move(movement * playerStateMachine.walkSpeed);
            else Move(movement * (playerStateMachine.isDashing ? playerStateMachine.dashSpeed : playerStateMachine.movementSpeed));
            ChangeDirection(movement);
        }
    }

    public void FaceTarget(GameObject target)
    {
        Vector3 lookPos = target.transform.position - playerStateMachine.transform.position;
        lookPos.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        playerStateMachine.transform.rotation = Quaternion.Slerp(playerStateMachine.transform.rotation, lookRotation, playerStateMachine.changeDirectionSpeed * Time.deltaTime);
    }

    public void FaceTargetInstantly(GameObject target)
    {
        Vector3 lookPos = target.transform.position - playerStateMachine.transform.position;
        lookPos.y = 0f;
        playerStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void ChangeDirection(Vector3 movement)
    {
        playerStateMachine.transform.rotation = Quaternion.Lerp(playerStateMachine.transform.rotation, Quaternion.LookRotation(movement), playerStateMachine.changeDirectionSpeed * Time.deltaTime);
    }

    public void ChangeDirectionInstantly(Vector3 movement)
    {
        playerStateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public void HandleCameraMovement()
    {
        if (InputReader.Instance.rightStickValue != Vector2.zero)
        {
            CameraController.Instance.RotateCamera();
        }
        if (InputReader.Instance.isPressingLeftTrigger)
        {
            CameraController.Instance.ZoomOut();
        }
        if (InputReader.Instance.isPressingRightTrigger)
        {
            CameraController.Instance.ZoomIn();
        }
    }
    public void Dodge()
    {
        playerStateMachine.SwitchState(new PlayerDodgingState(playerStateMachine));
    }

    public void Jump()
    {
        playerStateMachine.SwitchState(new PlayerJumpState(playerStateMachine));
    }

    public void PlayAnimation(int animationHash, float crossFixedDuration)
    {
        playerStateMachine.animator.CrossFadeInFixedTime(animationHash, crossFixedDuration);
    }

    public void LockOnMode()
    {
        if (playerStateMachine.targetManager.GetCurrentTarget() == null)
        {
            if (playerStateMachine.targetManager.TryLockOn())
                SpanCameraFaceTarget();
        }
        else
        {
            //playerStateMachine.targetManager.DisableLockOn();
            LockOnNextTarget();
        }
    }

    public void LockOnNextTarget()
    {
        if (playerStateMachine.targetManager.GetCurrentTarget() == null) return;
        playerStateMachine.targetManager.NextTarget();
        SpanCameraFaceTarget();
    }

    public void LockOnPreviousTarget()
    {
        if (playerStateMachine.targetManager.GetCurrentTarget() == null) return;
        playerStateMachine.targetManager.PreviouTarget();
        SpanCameraFaceTarget();
    }

    public void SpanCameraFaceTarget()
    {
        if (playerStateMachine.targetManager.GetCurrentTarget() == null) return;
        Vector3 direction = playerStateMachine.targetManager.GetCurrentTarget().transform.position - playerStateMachine.transform.position;
        float cameraAngle = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles.y;
        CameraController.Instance.SpanCamera(cameraAngle);
    }

    public void QuickSwitchWeapon()
    {
        WeaponItemData currentWeapon = playerStateMachine.character.GetCurrentWeaponData();
        if (currentWeapon.weaponType == WeaponType.Sword)
        {
            playerStateMachine.character.ChangeWeapon("5002");
            playerStateMachine.swordMainHand.SetActive(false);
            playerStateMachine.bowBack.SetActive(false);
            playerStateMachine.swordBack.SetActive(true);
            playerStateMachine.bowMainHand.SetActive(true);
        }
        else if (currentWeapon.weaponType == WeaponType.Bow)
        {
            playerStateMachine.character.ChangeWeapon("5001");
            playerStateMachine.swordMainHand.SetActive(true);
            playerStateMachine.bowBack.SetActive(true);
            playerStateMachine.swordBack.SetActive(false);
            playerStateMachine.bowMainHand.SetActive(false);
        }
        EventHandler.OnSwitchWeaponEvent();
    }
}
