using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryBox : SingletonMonobehaviour<InventoryBox>
{
    [SerializeField] private List<InventorySlot> inventoryList = new List<InventorySlot>();
    private Dictionary<string, InventorySlot> inventoryDictionary = new Dictionary<string, InventorySlot>();

    protected override void Awake()
    {
        base.Awake();
        CreateInventoryDictionary();
    }

    private void CreateInventoryDictionary()
    {
        foreach (InventorySlot slot in inventoryList)
        {
            inventoryDictionary.Add(slot.itemGuid, slot);
        }
    }

    public void AddItem(string itemGuid, int quantity = 1)
    {
        if (inventoryDictionary.TryGetValue(itemGuid, out InventorySlot slot))
        {
            Debug.Log($"Got Item {itemGuid}");
            slot.Add(quantity);
        }
        else
        {
            Debug.Log($"Got New Item {itemGuid}");
            InventorySlot newSlot = new InventorySlot(itemGuid, quantity);
            inventoryList.Add(newSlot);
            inventoryDictionary.Add(itemGuid, newSlot);
        }
    }

    public bool RemoveItem(string itemGuid, int quantity)
    {
        if (inventoryDictionary.TryGetValue(itemGuid, out InventorySlot slot))
        {
            if (quantity > slot.quantity)
            {
                Debug.Log("You don't have enough.");
                return false;
            }
            slot.Remove(quantity);
            /*if (slot.quantity <= 0)
            {
                inventoryList.Remove(slot);
                inventoryDictionary.Remove(itemGuid);
            }*/
            return true;
        }
        else
        {
            Debug.Log("Item doesn't exist in inventory. Failed to remove item.");
            return false;
        }
    }

    public InventorySlot CheckInventory(string itemGuid)
    {
        if (inventoryDictionary.TryGetValue(itemGuid, out InventorySlot slot))
        {
            return slot;
        }
        else return null;  
    }
}
