using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private WeaponCollider weapon;
    [SerializeField] private List<Attack> attacks;

    public void EnabledAttack(int index)
    {
        if (index >= attacks.Count())
        {
            Debug.Log($"No attack information at index {index}");
            return;
        }
        weapon.gameObject.SetActive(true);
        weapon.SetAttack(attacks[index].damage, attacks[index].knockBack, attacks[index].launchForce, 
                         attacks[index].hitLagDuration, attacks[index].hitLagStrength, attacks[index].hitEffectPrefab);
    }

    public void HitboxDisabled()
    {
        weapon.gameObject.SetActive(false);
    }
}
