using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Transform mainCameraTransform;

    public override void Start()
    {
        base.Start();
        SwitchState(new PlayerFreeLookState(this));
    }

    public override void OnDamage()
    {
        base.OnDamage();
        SwitchState(new PlayerImpactState(this));
    }

    public override void OnDie(GameObject dieCharacter)
    {
        if (dieCharacter != this.gameObject) return;
        SwitchState(new PlayerDieState(this));
    }
}
