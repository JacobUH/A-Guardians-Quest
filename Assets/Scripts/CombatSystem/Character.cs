using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public bool isDead;
    public bool isUnflinching;
    public bool isInvincible;
    public event Action<GameObject> DieEvent;
    public event Action DamageEvent;
    private int dmgBst;
    [SerializeField]public Attack[] attacks;
    

    [SerializeField] private GaugeBar healthBar;
    [SerializeField] private TextMeshProUGUI healthBarTextMesh;
    [SerializeField] private CharacterBaseStats baseStats;
    [SerializeField] private CharacterEquipmentData equipmentData;

    private float currentHp;
    public float CurrentHpPercent => currentHp / GetMaxHp();

    private void Start()
    {
        int maxHp = GetMaxHp();
        currentHp = maxHp;
        healthBar.SetBar(maxHp);
        UpdateHPUI();
        
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    public bool TryDealDamage(float damage)
    {
        if (isDead || isInvincible) return false;

        if (!isUnflinching) DamageEvent?.Invoke();

        currentHp = Mathf.Max(currentHp - damage, 0);
        UpdateHPUI();
        if (currentHp == 0) Die();
        return true;
    }

    public void Die()
    {
        isDead = true;
        DieEvent?.Invoke(this.gameObject);
        StartCoroutine(DestroyCoroutine());
    }

    public void RecoverHp(int amount)
    {
        int maxHp = Mathf.RoundToInt(GetMaxHp());
        currentHp = Mathf.Min(currentHp + amount, maxHp);
        UpdateHPUI();
    }

    public void UpdateHPUI()
    {
        int maxHp = Mathf.RoundToInt(GetMaxHp());
        healthBar.ChangeBar(Mathf.RoundToInt(currentHp));
        if (healthBarTextMesh != null) healthBarTextMesh.text = $"HP {currentHp}/{maxHp}";
    }

    public int GetMaxHp()
    {
        return Mathf.RoundToInt(baseStats.hp + equipmentData.TotalHp());
    }

    public float GetAttack()
    {
        return baseStats.attack + equipmentData.TotalAttack();
    }

    public float GetDefense()
    {
        return baseStats.defense + equipmentData.TotalDefense();
    }

    public WeaponItemData GetCurrentWeaponData()
    {
        string itemGuid = equipmentData.equippingItemGuids[0];
        if (!string.IsNullOrEmpty(itemGuid))
        {
            WeaponItemData weapon = (WeaponItemData)ItemDatabase.Instance.GetItemData(equipmentData.equippingItemGuids[0]);
            return weapon;
        }
        else return null;
    }

    public void ChangeWeapon(string itemGuid)
    {
        equipmentData.Equip(itemGuid);
    }

    public void increaseDamage(int amount, float duration = 5)
    {
        dmgBst = amount;
        foreach (Attack attack in attacks)
        {
            attack.damage += amount;
        }
        Invoke("endEffectWeapon", duration);
    }
    private void endEffectWeapon()
    {
        foreach (Attack attack in attacks)
        {
            attack.damage -= dmgBst;
        }
    }
}
