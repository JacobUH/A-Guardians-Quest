using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponItemData : EquipmentItemData
{
    public WeaponType weaponType;

    public WeaponItemData()
    {
        this.equipmentType = EquipmentType.Weapon;
    }
}
