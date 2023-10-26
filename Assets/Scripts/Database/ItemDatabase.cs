using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : SingletonMonobehaviour<ItemDatabase>
{
    [SerializeField] private List<ItemData> itemList;
    private Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

    protected override void Awake()
    {
        base.Awake();
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        foreach (ItemData item in itemList)
        {
            if (!itemDictionary.TryGetValue(item.id, out _))
            {
                itemDictionary.Add(item.id, item);
            }
        }
    }

    public ItemData GetItemData(int itemID)
    {
        if (itemDictionary.TryGetValue(itemID, out ItemData itemData))
        {
            return itemData;
        }
        else
        {
            Debug.LogWarning($"Item ID {itemID} is not existing in database.");
            return null;
        }
    }

    public ItemData GetItemData(string itemGuid)
    {
        if (itemGuid.Length >= 4 && int.TryParse(itemGuid.Substring(0, 4), out int itemID))
        {
            return GetItemData(itemID);
        }
        else
        {
            Debug.Log("Incorrect format: ItemGuid");
            return null;
        }
    }
}

