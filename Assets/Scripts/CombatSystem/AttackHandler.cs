using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private WeaponCollider weapon;
    [SerializeField] private Attack normalAttack1;
    [SerializeField] private Attack normalAttack2;
    [SerializeField] private Attack normalAttack3;

    public void NormalAttack1Enabled()
    {
        weapon.SetAttack(normalAttack1.damage, normalAttack1.knockBack);
    }

    public void NormalAttack2Enabled()
    {
        weapon.SetAttack(normalAttack2.damage, normalAttack2.knockBack);
    }

    public void NormalAttack3Enabled()
    {
        weapon.SetAttack(normalAttack3.damage, normalAttack3.knockBack);
    }

    public void HitboxDisabled()
    {
        weapon.DisableHitBox();
    }
}
