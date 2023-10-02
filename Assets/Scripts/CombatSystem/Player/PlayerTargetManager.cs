using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTargetManager : TargetManager
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyCharacter>(out EnemyCharacter enemy))
        {
            targets.Add(other.gameObject);
            enemy.DieEvent += TargetOnDie;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        TargetOnDie(other.gameObject);
    }
}
   
