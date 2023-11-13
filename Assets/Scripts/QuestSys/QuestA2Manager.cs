using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestA2Manager : MonoBehaviour
{
    private List<QuestA2> activeQuest;

    public void acceptQuest(QuestA2 quest)
    {
        if (!quest.completed)
        {
            activeQuest.Add(quest);
        }
        else
        {

            FindObjectOfType<DialogueManager>().StartDialogue(quest.defaultResponse);
        }
        
    }

    public void finishQuest(QuestA2 quest)
    {
        activeQuest.Remove(quest);
        InventoryBox.Instance.AddItem("9999", quest.coinReward[0]);
        EventHandler.OnPickUpItemEvent("9999");
        InventoryBox.Instance.AddItem("9998", quest.coinReward[1]);
        EventHandler.OnPickUpItemEvent("9998");
        InventoryBox.Instance.AddItem("9997", quest.coinReward[2]);
        EventHandler.OnPickUpItemEvent("9997");
        quest.questComplete();
    }
}
