using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectQuests : MonoBehaviour
{
    public EnemiesToKill target;
    public QuestTriggerStart trigger;

    private void Update()
    {
        if(target.amountToKill == 0)
        {
            Invoke("triggerNext", 2.0f);
        }
    }
    private void triggerNext()
    {
        trigger.onCompletePrev();
    }
}
