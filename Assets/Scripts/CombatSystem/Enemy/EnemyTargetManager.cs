using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : TargetManager
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTarget = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentTarget != null)
        {
            currentTarget = null;
        }
    }
}
