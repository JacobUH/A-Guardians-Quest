using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class SlotData : MonoBehaviour
{
    public string itemGuid;
    public int quantity;
    public TextMeshProUGUI quantityText;
    public Image image;

    public void Initialize(string itemGuid, int quantity)
    {
        this.itemGuid = itemGuid;
        this.quantity = quantity;
        quantityText.text = $"x{quantity}";
        image.sprite = ItemDatabase.Instance.GetItemData(itemGuid).sprite;
    }
}
