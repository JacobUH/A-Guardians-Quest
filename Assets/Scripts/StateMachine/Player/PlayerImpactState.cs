using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerState
{
    public PlayerImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int impactHash = Animator.StringToHash("Impact");
    private float crossFixedDuration = 0.1f;

    public override void Enter()
    {
        AttackHandler attackHandler = playerStateMachine.GetComponent<AttackHandler>();
        attackHandler.HitboxDisabled();
        attackHandler.DisabledSwordTrail();

        PlayAnimation(impactHash, crossFixedDuration);
        InputReader.Instance.DpadDownButtonPressEvent += LockOnMode;
    }

    public override void Exit()
    {
        InputReader.Instance.DpadDownButtonPressEvent -= LockOnMode;
    }

    public override void Tick()
    {
        Move(Vector3.zero); //Apply Force
        float normalizedTime = GetNormalizedTime(playerStateMachine.animator, impactHash);
        if (normalizedTime >= 1f)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
