using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effect/DamageIncrease")]
public class DamageBoost : ItemEffect
{
    [SerializeField] private int increaseAmount;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private int duration;
    

    public override void ResolveEffect(GameObject target)
    {
        if (target.TryGetComponent<Character>(out Character character))
        {
            character.increaseDamage(duration, increaseAmount);
            Instantiate(damageEffect, target.transform);
            Debug.Log($"Increased {target.name} Damage for {increaseAmount} Hp");
        }
    }

    
}

