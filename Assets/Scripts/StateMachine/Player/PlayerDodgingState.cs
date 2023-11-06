using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerState
{
    public PlayerDodgingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int dodgeHash = Animator.StringToHash("Dodge");
    private Vector3 movement;
    private const float crossFadeDuration = 0.1f;
    private float invincibleTimer;

    public override void Enter()
    {
        AttackHandler attackHandler = playerStateMachine.GetComponent<AttackHandler>();
        attackHandler.HitboxDisabled();
        attackHandler.DisabledSwordTrail();

        playerStateMachine.character.isInvincible = true;
        invincibleTimer = 0f;

        movement = CalculateMovement();
        if (movement != Vector3.zero) ChangeDirectionInstantly(movement);

        PlayAnimation(dodgeHash, crossFadeDuration);
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
        InputReader.Instance.SouthButtonPressEvent += Jump;
        InputReader.Instance.DpadLeftButtonPressEvent += QuickSwitchWeapon;
    }

    public override void Exit()
    {
        playerStateMachine.character.isInvincible = false;
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
        InputReader.Instance.SouthButtonPressEvent -= Jump;
        InputReader.Instance.DpadLeftButtonPressEvent -= QuickSwitchWeapon;
    }

    public override void Tick()
    {
        HandleCameraMovement();

        if (invincibleTimer < 0.2f && playerStateMachine.character.isInvincible == true)
        {
            invincibleTimer += Time.deltaTime;
        }
        else if (playerStateMachine.character.isInvincible == true)
        {
            playerStateMachine.character.isInvincible = false;
        }

        float normalizedTime = GetNormalizedTime(playerStateMachine.animator, dodgeHash);
        if (normalizedTime <= 0.7f)
        {
            Move(playerStateMachine.transform.forward * playerStateMachine.dodgeSpeed);
        }
        else
        {
            Move(playerStateMachine.transform.forward * playerStateMachine.dodgeSpeed * 0.2f);
        }
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
