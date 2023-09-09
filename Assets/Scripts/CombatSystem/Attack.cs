using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/New Attack")]
public class Attack: ScriptableObject
{
    public string animationName;
    public float transitionDuration;
    public int nextComboIndex = -1;
    public float nextComboEnableTime;
    public int damage;
    public float knockBack;
    public float hitLagDuration;
    [Range(0f, 1f)] public float hitLagStrength;
}
