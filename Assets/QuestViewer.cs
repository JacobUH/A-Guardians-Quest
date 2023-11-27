using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestViewer : MonoBehaviour
{
    public GameObject content;

   
    private void Awake()
    {
        List<int> aQ = QuestA2Manager.Instance.activeQuest;
        foreach (int i in aQ)
        {

        }
    }
    private void OnDisable()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
