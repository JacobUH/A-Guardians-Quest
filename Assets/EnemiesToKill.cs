using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesToKill : MonoBehaviour
{
    public int amountToKill;
    public int questID = 0;

    public void checkQuest() { 
        if(amountToKill == 0)
        {
            QuestA2Manager.Instance.finishQuest(questID);
            if(FindObjectOfType<ConnectQuests>() != null)
            {
                FindObjectOfType<ConnectQuests>().triggerNext();
            }
        }
        
    }
}
