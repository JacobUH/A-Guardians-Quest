using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float targetRange;
    [SerializeField] private SphereCollider sphereCollider;

    private int targetIndex;
    private GameObject currentTarget;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = targetRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            targets.Add(other.gameObject);
            enemy.EnemyDieEvent += Unsubscribe;
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

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }

    public void LockOnTarget()
    {
        currentTarget = GetNearestTarget();
    }

    public void DisableLockOn()
    {
        currentTarget = null;
    }

    public GameObject GetNearestTarget()
    {
        if (targets.Count() == 0) return null;

        GameObject nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        for (int i = 0; i < targets.Count(); i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = targets[i];
                targetIndex = i;
            }
        }
        return nearestTarget;
    }

    public void NextTarget()
    {
        if (targets.Count() <= 1) return;

        targetIndex++;
        if (targetIndex >= targets.Count())
        {
            targetIndex = 0;
        }
        currentTarget =  targets[targetIndex];
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
