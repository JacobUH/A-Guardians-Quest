using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effect/Heal")]
public class Heal : ItemEffect
{
    [SerializeField] private int healAmount;
    [SerializeField] private GameObject healEffect;

    public override void ResolveEffect(GameObject target)
    {
        if(target.TryGetComponent<Character>(out Character character))
        {
            character.RecoverHp(healAmount);
            Instantiate(healEffect, target.transform);
            Debug.Log($"Heal {target.name} for {healAmount} Hp");
        }
    }
}
