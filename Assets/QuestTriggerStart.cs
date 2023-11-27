using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerStart : MonoBehaviour
{
    public int questStartID;

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestA2Manager.Instance.acceptQuest(questStartID);
            transform.gameObject.SetActive(false);
        }
    }
}
