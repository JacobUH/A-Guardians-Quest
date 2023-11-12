using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkeletonWarriorStateMachine : EnemyStateMachine
{
    public List<GameObject> patrolPath = new List<GameObject>();
    public bool isAggro;
    public bool isPatrol;

    public override void Start()
    {
        base.Start();
        SwitchState(new SkeletonWarriorIdleState(this));
    }

    public override void OnDamage()
    {
        base.OnDamage();
        SwitchState(new SkeletonWarriorImpactState(this));
    }

    public override void OnDie(GameObject dieCharacter)
    {
        base.OnDie(dieCharacter);
        SwitchState(new SkeletonWarriorDieState(this));
    }
}
