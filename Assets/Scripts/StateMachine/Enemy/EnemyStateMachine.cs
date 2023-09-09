using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyTargetManager enemyTargetManager;
    public EnemyCharacter enemyCharacter;

    public override void Start()
    {
        base.Start();
        enemyCharacter = GetComponent<EnemyCharacter>();
        enemyTargetManager = GetComponent<EnemyTargetManager>();
    }

    public override void OnDamage()
    {
        base.OnDamage();
    }

    public override void OnDie(GameObject dieCharacter)
    {
        base.OnDie(dieCharacter);
    }
}