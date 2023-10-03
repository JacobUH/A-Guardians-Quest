using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public bool isDead;
    public bool isUnflinching;
    public bool isInvincible;
    [SerializeField] private GaugeBar healthBar;
    public float maxHp;
    public float attack;
    public float defense;

    public event Action<GameObject> DieEvent;
    public event Action DamageEvent;
    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;
        healthBar.SetBar(Mathf.RoundToInt(maxHp));
        healthBar.ChangeBar(Mathf.RoundToInt(currentHp));
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }

    public void DealDamage(float damage)
    {
        if (isDead || isInvincible) return;

        if (!isUnflinching) DamageEvent?.Invoke();

        currentHp = Mathf.Max(currentHp - damage, 0);
        healthBar.ChangeBar((int)currentHp);
        if (currentHp == 0) Die();
    }

    public void Die()
    {
        isDead = true;
        DieEvent?.Invoke(this.gameObject);
        StartCoroutine(DestroyCoroutine());
    }

    
}
