using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    private QuickSlotManager qms;
    public DialogueTrigger defaultResponse;
    public Dialogue OutOfStock;
    public Dialogue NotEnoughMoney;
    
    
    void Start()
    {
        
    }
    

    void Update()
    {
        
    }

    public void ClickBuy(Button button)
    {
        ItemData Item = button.gameObject.GetComponent<ShopItem>().item4Sale;
        qms = FindObjectOfType<QuickSlotManager>();
        if (canBuy(Item, button))
        {
            InventoryBox.Instance.AddItem(Item.id.ToString(),1);
            InventoryBox.Instance.RemoveItem("9999", Item.cost[0]);
            InventoryBox.Instance.RemoveItem("9998", Item.cost[1]);
            InventoryBox.Instance.RemoveItem("9997", Item.cost[2]);
            qms.UpdateUI();
            CoinManager.Instance.UpdateUI();

            if (Item.id == 5002)
            {
                if (button.FindSelectableOnRight().gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(button.FindSelectableOnRight().gameObject);
                }
                else
                if (button.FindSelectableOnLeft().gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(button.FindSelectableOnLeft().gameObject);
                }
                button.interactable = false;
                button.transform.Find("SoldOut").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>().bowBack.SetActive(true);
            }
        }
        /*else
        {
            defaultResponse.triggerDialogue();
        }*/
    }


    

    private bool canBuy(ItemData Item, Button button)
    {
        int[] prices = Item.cost;

        /*if(Item.quantityToSell == 0)
        {
            defaultResponse.dialogue = OutOfStock;
            button.interactable = false;
            button.transform.Find("SoldOut").gameObject.SetActive(true);
            return false;
        }*/
        
        if (prices[0] > InventoryBox.Instance.CheckInventory("9999").quantity)
        {
            defaultResponse.dialogue = NotEnoughMoney;
            return false;
        }
        if (prices[1] > InventoryBox.Instance.CheckInventory("9998").quantity)
        {
            defaultResponse.dialogue = NotEnoughMoney;
            return false;
        }
        if (prices[2] > InventoryBox.Instance.CheckInventory("9997").quantity)
        {
            defaultResponse.dialogue = NotEnoughMoney;
            return false;
        }

        return true;
        
    }
    
}
