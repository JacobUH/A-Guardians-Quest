using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectQuests : MonoBehaviour
{
    public EnemiesToKill target;
    public QuestTriggerStart trigger;

    
    public void triggerNext()
    {
        trigger.onCompletePrev();
    }
}
