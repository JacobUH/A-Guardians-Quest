using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour

{
    public TextMeshPro sentenceObj;
    public TextMeshPro nameBox;

    public Animator textAnim;

    private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue)
    {
        textAnim.SetBool("IsOpen", true);
        sentences.Clear();
        nameBox.text = dialogue.NPC;

        foreach(string sentence in dialogue.dialogue)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        sentenceObj.text = sentence;
    }

    void EndDialogue()
    {
        textAnim.SetBool("IsOpen", false);
        sentenceObj.text=null;
        nameBox.text = null;
        return;
    }
}
