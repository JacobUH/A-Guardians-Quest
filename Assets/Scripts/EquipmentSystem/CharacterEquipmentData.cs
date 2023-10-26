using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterEquipmentData
{
    public string[] equippingItemGuids = new string[(int)EquipmentType.Count];

    public EquipmentItemData GetEquipmentData(EquipmentType equipmentType)
    {
        int index = (int)equipmentType;
        return GetEquipmentData(index);
    }

    public EquipmentItemData GetEquipmentData(int index)
    {
        if (string.IsNullOrEmpty(equippingItemGuids[index])) return null;
        else return (EquipmentItemData)ItemDatabase.Instance.GetItemData(equippingItemGuids[index]);
    }

    public float TotalHp()
    {
        float totalHp = 0;
        for (int i = 0; i < equippingItemGuids.Length; i++)
        {
            EquipmentItemData equipmentData = GetEquipmentData(i);
            if (equipmentData == null) continue;
            totalHp += equipmentData.hp;
        }
        return totalHp;
    }

    public float TotalAttack()
    {
        float totalAttack = 0;
        for (int i = 0; i < equippingItemGuids.Length; i++)
        {
            EquipmentItemData equipmentData = GetEquipmentData(i);
            if (equipmentData == null) continue;
            totalAttack += equipmentData.atk;
        }
        return totalAttack;
    }

    public float TotalDefense()
    {
        float totalDefense = 0;
        for (int i = 0; i < equippingItemGuids.Length; i++)
        {
            EquipmentItemData equipmentData = GetEquipmentData(i);
            if (equipmentData == null) continue;
            totalDefense += equipmentData.def;
        }
        return totalDefense;
    }

    public void Equip(string itemGuid)
    {
        EquipmentItemData euqipmentData = (EquipmentItemData)ItemDatabase.Instance.GetItemData(itemGuid);
        int index = (int)euqipmentData.equipmentType;

        if (!String.IsNullOrEmpty(equippingItemGuids[index]))
        {
            //InventoryBox.Instance.ChangeEquipmment(equippingItemGuids[index], -1);
            equippingItemGuids[index] = null;
        }
        equippingItemGuids[index] = itemGuid;
        //InventoryBox.Instance.ChangeEquipmment(itemGuid, this.id);
    }

    public void Unequip(string itemGuid)
    {
        EquipmentItemData euqipmentData = (EquipmentItemData)ItemDatabase.Instance.GetItemData(itemGuid);
        equippingItemGuids[(int)euqipmentData.equipmentType] = null;
        //InventoryBox.Instance.ChangeEquipmment(itemGuid, -1);
    }
}
