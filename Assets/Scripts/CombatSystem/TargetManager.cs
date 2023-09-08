using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> targets;
    [SerializeField] protected float targetRange;
    [SerializeField] protected SphereCollider sphereCollider;

    protected int targetIndex;
    protected GameObject currentTarget;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = targetRange;
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
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

    public bool IsTargetInFront()
    {
        if (currentTarget == null) return false;

        Vector3 directionToTarget = currentTarget.transform.position - transform.position;
        float dotProduct = Vector3.Dot(directionToTarget.normalized, transform.forward);
        if (dotProduct >= 0.95f)
        {
            return true;
        }
        else return false;
    }

    public float GetDistanceToTarget()
    {
        if (currentTarget == null) return -1f;
        return Vector3.Distance(transform.position, currentTarget.transform.position);
    }
}
