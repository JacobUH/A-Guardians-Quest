using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerEnd : MonoBehaviour
{
    public int questEnd;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestA2Manager.Instance.finishQuest(questEnd);
            
        }
    }
    
}
