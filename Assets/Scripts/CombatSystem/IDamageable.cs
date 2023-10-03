using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public bool TryDealDamage(float damage);
    public void Die();
}
