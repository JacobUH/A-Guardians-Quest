using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : TargetManager
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentTarget != null)
        {
            currentTarget = null;
        }
    }
}
