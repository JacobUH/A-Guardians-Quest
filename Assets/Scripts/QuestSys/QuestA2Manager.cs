using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestA2Manager : SingletonMonobehaviour<QuestA2Manager>
{
    public List<int> activeQuest;
    public QuestDatabase qB;
    public Animator questNotifComplete;
    public TMP_Text qComplete;
    public Animator questNotifStart;
    public TMP_Text qStart;

    void Start()
    {
        activeQuest = new List<int>();
    }

    public void acceptQuest(int quest)
    {   
        if (!qB.questDatabase[quest].completed)
        {
            
            qStart.text = qB.questDatabase[quest].questName;
            questNotifStart.SetBool("startQuest", true);
            activeQuest.Add(quest);
            Invoke("turnNotifS", 2.0f);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(qB.questDatabase[quest].defaultResponse);
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
        qComplete.text = qB.questDatabase[quest].questName;
        questNotifComplete.SetBool("completeQuest", true);
        qB.questDatabase[quest].questComplete();
        Invoke("turnNotifC", 2.0f);
    }

    void turnNotifC()
    {
        questNotifComplete.SetBool("completeQuest", false);
        qComplete.text = null;
    }
    void turnNotifS()
    {
        questNotifStart.SetBool("startQuest", false);
        qStart.text = null;
    }
}
