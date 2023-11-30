using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestA2
{
    public string questName;
    public string questDescription;
    public bool completed = false;
    [Space]
    public int[] coinReward;
    [Space]
    public Dialogue defaultResponse;
    

    public void questComplete()
    {
        completed = true;
    }
}
