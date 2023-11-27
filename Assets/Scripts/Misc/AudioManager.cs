using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    public AudioSource audioSource;
    public List<AudioClip> bgms = new List<AudioClip>();

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        
    }

    public void MuteBGM()
    {
        audioSource.mute = true;
    }

    public void UnmuteBGM()
    {
        audioSource.mute = false;
    }

    public void ChangeBMG(int index)
    {
        if (index < 0 || index >= bgms.Count())
        {
            Debug.Log($"No audioclip at index {index}");
            return;
        }
        audioSource.clip = bgms[index];
        audioSource.Play();
    }
}
