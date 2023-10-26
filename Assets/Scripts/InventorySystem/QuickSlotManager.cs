using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : SingletonMonobehaviour<QuickSlotManager>
{
    [SerializeField] private int quickItemSlotCount = 5;
    [SerializeField] private int quickWeaponSlotCount = 3;
    [SerializeField] private List<InventorySlot> quickItemSlots = new List<InventorySlot>();
    [SerializeField] private List<InventorySlot> quickWeaponSlots = new List<InventorySlot>();
    [SerializeField] private Image quickItemSlotImage;
    [SerializeField] private Image quickWeaponSlotImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTextMesh;
    [SerializeField] private TextMeshProUGUI weaponNameTextMesh;

    private int currentActiveIndex;
    private InventorySlot currentItemSlot;
    private WeaponItemData currentWeaponSlot;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UpdateCurrentItemInfo();
        UpdateCurrentWeaponInfo();
    }

    private void UpdateUI()
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

    public void UpdateCurrentItemInfo()
    {
        if (currentItemSlot == null)
        {
            currentItemSlot = InventoryBox.Instance.GetInventoryInfo("1001");
        }
        else currentItemSlot = InventoryBox.Instance.GetInventoryInfo(currentItemSlot.itemGuid);
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
