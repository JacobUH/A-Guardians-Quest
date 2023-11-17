using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestA2Manager : MonoBehaviour
{
    private List<int> activeQuest;
    public QuestDatabase qB;

    public void acceptQuest(int quest)
    {
        if (!qB.questDatabase[quest].completed)
        {
            activeQuest.Add(quest);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(qB.questDatabase[quest].defaultResponse);
        }
        
    }

    public void finishQuest(int quest)
    {
        activeQuest.Remove(quest);
        InventoryBox.Instance.AddItem("9999", qB.questDatabase[quest].coinReward[0]);
        EventHandler.OnPickUpItemEvent("9999");
        InventoryBox.Instance.AddItem("9998", qB.questDatabase[quest].coinReward[1]);
        EventHandler.OnPickUpItemEvent("9998");
        InventoryBox.Instance.AddItem("9997", qB.questDatabase[quest].coinReward[2]);
        EventHandler.OnPickUpItemEvent("9997");
        qB.questDatabase[quest].questComplete();
    }
}
