using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportKill : MonoBehaviour
{
    public EnemiesToKill manager;
    private void OnDestroy()
    {
        manager.amountToKill -= 1;
        manager.checkQuest();
    }
}
