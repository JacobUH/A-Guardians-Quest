using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/New Combo")]
public class Combo: ScriptableObject
{
    public Attack[] attack;
}
