using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public bool isStackable;
    public bool isDiscardable;
    public int id;
    public string itemName;
    public Sprite sprite;
    public string shortDescription;
    public string longDescription;
    public int[] cost;
    public int quantityToSell;
}
