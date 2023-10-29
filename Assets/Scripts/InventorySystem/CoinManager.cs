using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : SingletonMonobehaviour<CoinManager>
{
    [SerializeField] private Image greenCoinImage;
    [SerializeField] private TextMeshProUGUI greenCoinQuantityTextMesh;

    private InventorySlot greenCoinSlot;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        UpdateCoinInfo();
    }

    private void UpdateUI()
    {
        if (greenCoinSlot != null)
        {
            greenCoinQuantityTextMesh.text = $"x{greenCoinSlot.quantity}";
        }
        else
        {
            greenCoinQuantityTextMesh.text = "x0";
        }
    }

    public void UpdateCoinInfo()
    {
        if (greenCoinSlot == null)
        {
            greenCoinSlot = InventoryBox.Instance.GetInventoryInfo("9999");
        }
        else greenCoinSlot = InventoryBox.Instance.GetInventoryInfo(greenCoinSlot.itemGuid);
        UpdateUI();
    }
}
