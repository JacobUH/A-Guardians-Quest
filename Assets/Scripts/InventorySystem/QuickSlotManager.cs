using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : MonoBehaviour
{
    [SerializeField] private Image quickItemSlotImage;
    [SerializeField] private Image quickWeaponSlotImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTextMesh;
    [SerializeField] private TextMeshProUGUI weaponNameTextMesh;

    private InventorySlot currentItemSlot;
    private WeaponItemData currentWeaponSlot;

    private void Start()
    {
        UpdateCurrentItemInfo("1001");
        UpdateCurrentWeaponInfo();
        EventHandler.UseItemEvent += UpdateCurrentItemInfo;
        EventHandler.PickUpItemEvent += UpdateCurrentItemInfo;
        EventHandler.SwitchWeaponEvent += UpdateCurrentWeaponInfo;
    }

    private void OnApplicationQuit()
    {
        EventHandler.UseItemEvent -= UpdateCurrentItemInfo;
        EventHandler.PickUpItemEvent -= UpdateCurrentItemInfo;
        EventHandler.SwitchWeaponEvent -= UpdateCurrentWeaponInfo;
    }

    public void UpdateUI()
    {
        if (currentItemSlot != null)
        {
            ItemData itemData = ItemDatabase.Instance.GetItemData(currentItemSlot.itemGuid);
            quickItemSlotImage.sprite = itemData.sprite;
            quickItemSlotImage.color = Color.white;
            itemQuantityTextMesh.text = $"x{currentItemSlot.quantity}";
        }
        else
        {
            quickItemSlotImage.color = Color.gray;
            itemQuantityTextMesh.text = "x0";
        }
    }

    private void UpdateWeaponUI()
    {
        if (currentWeaponSlot != null)
        {
            quickWeaponSlotImage.sprite = currentWeaponSlot.sprite;
        }
        else
        {
            quickItemSlotImage.color = Color.gray;
        }
    }

    public void UpdateCurrentItemInfo(string itemGuid)
    {
        if (itemGuid == "1001" || itemGuid == "1002")
        {
            currentItemSlot = InventoryBox.Instance.CheckInventory(itemGuid);
        }
        UpdateUI();
    }

    public void SwitchItem(InventorySlot slot)
    {
        currentItemSlot = slot;
        UpdateUI();
    }

    public InventorySlot GetQuickItemInfo()
    {
        return currentItemSlot;
    }

    public void UpdateCurrentWeaponInfo()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        currentWeaponSlot = player.GetComponent<Character>().GetCurrentWeaponData();
        UpdateWeaponUI();
    }
}
