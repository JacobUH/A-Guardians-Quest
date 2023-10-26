using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public string itemGuid;
    public int quantity;

    public InventorySlot(string itemGuid, int quantity)
    {
        this.itemGuid = itemGuid;
        this.quantity = quantity;
    }

    public void Add(int value)
    {
        quantity += value;
    }

    public void Remove(int value)
    {
        if (value > quantity)
        {
            Debug.Log("Can't remove.");
            return;
        }
        quantity -= value;
    }
}
