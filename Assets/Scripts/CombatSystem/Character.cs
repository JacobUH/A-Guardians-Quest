using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private GaugeBar healthBar;
    public float maxHp;
    public float attack;
    public float defense;

    public event Action<GameObject> DieEvent;
    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;
        healthBar.SetBar(Mathf.RoundToInt(maxHp));
        healthBar.ChangeBar(Mathf.RoundToInt(currentHp));
    }

    public void DealDamage(float damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        healthBar.ChangeBar((int)currentHp);
        if (currentHp == 0) Die();
    }

    public void Die()
    {
        DieEvent?.Invoke(this.gameObject);
        Destroy(this.gameObject);
    }
}
