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
            slot.Add(quantity);
        }
        else
        {
            InventorySlot newSlot = new InventorySlot(itemGuid, quantity);
            inventoryList.Add(newSlot);
            inventoryDictionary.Add(itemGuid, newSlot);
        }
    }

    public void RemoveItem(string itemGuid, int quantity)
    {
        if (inventoryDictionary.TryGetValue(itemGuid, out InventorySlot slot))
        {
            if (quantity > slot.quantity)
            {
                Debug.Log("You don't have enough.");
                return;
            }
            slot.Remove(quantity);
            if (slot.quantity <= 0)
            {
                inventoryList.Remove(slot);
                inventoryDictionary.Remove(itemGuid);
            }
        }
        else
        {
            Debug.Log("Item doesn't exist in inventory. Failed to remove item.");
        }
    }

    public List<InventorySlot> GetConsumableList()
    {
        List<InventorySlot> list = new List<InventorySlot>();
        foreach (InventorySlot slot in inventoryList)
        {
            if (ItemDatabase.Instance.GetItemData(slot.itemGuid).type == ItemType.Consumable)
            {
                list.Add(slot);
            }
        }
        return list;
    }

    public InventorySlot GetInventoryInfo(string itemGuid)
    {
        if (inventoryDictionary.TryGetValue(itemGuid, out InventorySlot slot))
        {
            return slot;
        }
        else return null;  
    }
}
