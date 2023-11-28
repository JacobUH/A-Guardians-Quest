using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : SingletonMonobehaviour<CoinManager>
{
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image greenCoinImage;
    [SerializeField] private Image redCoinImage;
    [SerializeField] private Image yellowCoinImage;
    [SerializeField] private TextMeshProUGUI arrowQuantityTextMesh;
    [SerializeField] private TextMeshProUGUI greenCoinQuantityTextMesh;
    [SerializeField] private TextMeshProUGUI redCoinQuantityTextMesh;
    [SerializeField] private TextMeshProUGUI yellowCoinQuantityTextMesh;

    private InventorySlot greenCoinSlot;
    private InventorySlot redCoinSlot;
    private InventorySlot yellowCoinSlot;
    private InventorySlot arrowSlot;

    private void Start()
    {
        UpdateCoinInfo("");
        EventHandler.UseItemEvent += UpdateCoinInfo;
        EventHandler.PickUpItemEvent += UpdateCoinInfo;
    }

    private void OnApplicationQuit()
    {
        EventHandler.UseItemEvent -= UpdateCoinInfo;
        EventHandler.PickUpItemEvent -= UpdateCoinInfo;
    }

    public void UpdateUI()
    {
        if (greenCoinSlot != null)
        {
            greenCoinQuantityTextMesh.text = $"{greenCoinSlot.quantity}";
        }
        else
        {
            greenCoinQuantityTextMesh.text = "0";
        }

        if (redCoinSlot != null)
        {
            redCoinQuantityTextMesh.text = $"{redCoinSlot.quantity}";
        }
        else
        {
            redCoinQuantityTextMesh.text = "0";
        }

        if (yellowCoinSlot != null)
        {
            yellowCoinQuantityTextMesh.text = $"{yellowCoinSlot.quantity}";
        }
        else
        {
            yellowCoinQuantityTextMesh.text = "0";
        }

        if (arrowSlot != null)
        {
            arrowQuantityTextMesh.text = $"{arrowSlot.quantity}";
        }
        else
        {
            arrowQuantityTextMesh.text = "0";
        }
    }

    public void UpdateCoinInfo(string itemGuid)
    {
        greenCoinSlot = InventoryBox.Instance.CheckInventory("9999");
        redCoinSlot = InventoryBox.Instance.CheckInventory("9998");
        yellowCoinSlot = InventoryBox.Instance.CheckInventory("9997");
        arrowSlot = InventoryBox.Instance.CheckInventory("5003");
        UpdateUI();
    }
}
