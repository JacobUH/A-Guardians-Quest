using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestViewer : MonoBehaviour
{
    public GameObject content;
    public GameObject preFab;

   
    private void OnEnable()
    {
        List<int> aQ = QuestA2Manager.Instance.activeQuest;
        
        foreach (int i in aQ)
        {
            QuestA2 currQuest = QuestDatabase.Instance.questDatabase[i];
            GameObject fab = Instantiate<GameObject>(preFab, Vector3.zero,Quaternion.identity,content.transform);
            GameObject image = fab.transform.Find("Image").gameObject;
            GameObject rewards = image.transform.Find("Rewards").gameObject;
            image.transform.Find("qName").gameObject.GetComponent<TMP_Text>().text = currQuest.questName;
            image.transform.Find("Description").gameObject.GetComponent<TMP_Text>().text = currQuest.questDescription;
            rewards.transform.Find("CoinRewardG").gameObject.GetComponent<TMP_Text>().text = currQuest.coinReward[0].ToString();
            rewards.transform.Find("CoinRewardR").gameObject.GetComponent<TMP_Text>().text = currQuest.coinReward[1].ToString();
            rewards.transform.Find("CoinRewardY").gameObject.GetComponent<TMP_Text>().text = currQuest.coinReward[2].ToString();
            
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
