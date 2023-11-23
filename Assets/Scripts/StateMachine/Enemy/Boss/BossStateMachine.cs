using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    private Coroutine actionCoroutine;
    public bool targetInRange;
    public float targetDistance;
    public GameObject fireballPrefab;

    public override void Start()
    {
        base.Start();
        SwitchState(new BossIdleState(this));
    }

    public override void OnDamage()
    {
        base.OnDamage();
        SwitchState(new BossImpactState(this));
    }

    public override void OnDie(GameObject dieCharacter)
    {
        base.OnDie(dieCharacter);
        SwitchState(new BossDieState(this));
    }
}
