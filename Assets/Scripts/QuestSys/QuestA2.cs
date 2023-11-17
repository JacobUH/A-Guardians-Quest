using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestA2
{
    public string questName;
    public string questDescription;
    public int[] coinReward;
    public bool completed = false;
    public Dialogue defaultResponse;

    public void questComplete()
    {
        completed = true;
    }
}
