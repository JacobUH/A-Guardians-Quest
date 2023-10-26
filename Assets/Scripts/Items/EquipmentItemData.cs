using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentItemData : ItemData
{
    public EquipmentType equipmentType;
    public float hp;
    public float atk;
    public float def;

    public EquipmentItemData()
    {
        this.type = ItemType.Equipment;
        isStackable = false;
        isDiscardable = true;
    }
}
