using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private WeaponCollider weapon;
    [SerializeField] private List<Attack> attacks;

    public void EnabledAttack(int index)
    {
        weapon.SetAttack(attacks[index].damage, attacks[index].knockBack, attacks[index].hitLagDuration, attacks[index].hitLagStrength);
    }

    public void HitboxDisabled()
    {
        weapon.DisableHitBox();
    }
}
