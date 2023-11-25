using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    DialogueTrigger trigger;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void clickBuy(GameObject button)
    {
        ItemData Item = button.GetComponent<ShopItem>().item4Sale;
        if (canBuy(Item))
        {
            InventoryBox.Instance.AddItem(Item.id.ToString());
            InventoryBox.Instance.RemoveItem("9999", Item.cost[0]);
            InventoryBox.Instance.RemoveItem("9998", Item.cost[1]);
            InventoryBox.Instance.RemoveItem("9997", Item.cost[2]);
        }
        else
        {
            trigger.triggerDialogue();
        }

    }

    private bool canBuy(ItemData Item)
    {
        int[] prices = Item.cost;

        if (prices[0] > InventoryBox.Instance.CheckInventory("9999").quantity)
        {
            return false;
        }
        if (prices[1] > InventoryBox.Instance.CheckInventory("9998").quantity)
        {
            return false;
        }
        if (prices[2] > InventoryBox.Instance.CheckInventory("9997").quantity)
        {
            return false;
        }
        return true;
    }
    
}
