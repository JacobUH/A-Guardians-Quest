using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTargetManager : TargetManager
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            targets.Add(other.gameObject);
            enemy.DieEvent += Unsubscribe;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Unsubscribe(other.gameObject);
    }

    private void Unsubscribe(GameObject target)
    {
        if (targets.Contains(target)) targets.Remove(target);
    }

    public void LockOnTarget()
    {
        currentTarget = GetNearestTarget();
    }

    public void DisableLockOn()
    {
        currentTarget = null;
    }

    public void NextTarget()
    {
        if (targets.Count() <= 1) return;

        targetIndex++;
        if (targetIndex >= targets.Count())
        {
            targetIndex = 0;
        }
        currentTarget = targets[targetIndex];
    }

    public void PreviouTarget()
    {
        if (targets.Count() <= 1) return;

        targetIndex--;
        if (targetIndex < 0)
        {
            targetIndex = targets.Count() - 1;
        }
        currentTarget = targets[targetIndex];
    }
}
