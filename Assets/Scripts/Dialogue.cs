using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    public string NPC;
    [TextArea(3, 10)]
    public string[] dialogue;
}
