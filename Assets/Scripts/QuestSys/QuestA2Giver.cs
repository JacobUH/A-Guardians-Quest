using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestA2Giver : MonoBehaviour
{
    public int questNum;

    public void takeQuest()
    {
        FindObjectOfType<QuestA2Manager>().acceptQuest(questNum);
    }
}
