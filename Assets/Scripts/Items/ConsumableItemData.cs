using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableItemData : ItemData
{
    public List<ItemEffect> itemEffects;

    public void Use(GameObject target)
    {
        foreach (ItemEffect itemEffect in itemEffects)
        {
            itemEffect.ResolveEffect(target);
        }
    }

    public ConsumableItemData()
    {
        this.type = ItemType.Consumable;
        isStackable = true;
        isDiscardable = true;
    }
}
