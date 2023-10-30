using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/New Attack")]
public class Attack: ScriptableObject
{
    public GameObject hitEffectPrefab;
    public GameObject projectilePrefab;
    public string animationName;
    public float transitionDuration;
    public float moveForwardDistance;
    public float moveStartTime;
    public float moveEndTime;
    public int nextComboIndex = -1;
    public int nextStrongComboIndex = -1;
    public float nextComboEnableTime;
    public int damage;
    public float knockBack;
    public float launchForce;
    public float hitLagDuration;
    public float projectileSpeed;
    public float projectileLifetime;
    [Range(0f, 1f)] public float hitLagStrength;
}
