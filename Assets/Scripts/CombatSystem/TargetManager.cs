using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> targets = new List<GameObject>();
    [SerializeField] protected float targetRange;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected GameObject targetUI;

    protected int targetIndex;
    [SerializeField] protected GameObject currentTarget;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = targetRange;
    }

    protected virtual void OnTriggerEnter(Collider other) { }
    protected virtual void OnTriggerExit(Collider other) { }

    protected void TargetOnDie(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
        }
        if (currentTarget == target) DisableLockOn();
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

    public void ClearTargetList()
    {
        targets.Clear(); ;
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
        if (currentTarget == null) return Mathf.Infinity;
        return Vector3.Distance(transform.position, currentTarget.transform.position);
    }

    public bool TryLockOn()
    {
        if (targets.Count() == 0) return false;
        if (targets.Count() == 1) currentTarget = targets[0];
        else currentTarget = GetNearestTarget();

        currentTarget.GetComponent<StateMachine>().targetManager.EnabledTargetedMark();
        return true;
    }

    public void DisableLockOn()
    {
        currentTarget.GetComponent<StateMachine>().targetManager.DisableTargetedMark();
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
        currentTarget.GetComponent<StateMachine>().targetManager.DisableTargetedMark();
        currentTarget = targets[targetIndex];
        currentTarget.GetComponent<StateMachine>().targetManager.EnabledTargetedMark();
    }

    public void PreviouTarget()
    {
        if (targets.Count() <= 1) return;

        targetIndex--;
        if (targetIndex < 0)
        {
            targetIndex = targets.Count() - 1;
        }
        currentTarget.GetComponent<StateMachine>().targetManager.DisableTargetedMark();
        currentTarget = targets[targetIndex];
        currentTarget.GetComponent<StateMachine>().targetManager.EnabledTargetedMark();
    }

    public void EnabledTargetedMark()
    {
        targetUI.SetActive(true);
    }

    public void DisableTargetedMark()
    {
        targetUI.SetActive(false);
    }
}

