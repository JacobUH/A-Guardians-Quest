using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class CutsceneVolManager : MonoBehaviour
{
    public Slider cutsceneSlider;
    private float preMute;

    void Start()
    {
        if (!PlayerPrefs.HasKey("CutsceneVol"))
        {
            PlayerPrefs.SetFloat("CutsceneVol", 0.5f);
            cutsceneSlider.value = 0.5f;
        }
        else
        {
            cutsceneSlider.value = PlayerPrefs.GetFloat("CutsceneVol");
        }
        onVolChange();
        
    }

    public void onVolChange()
    {
        if (GameObject.FindObjectsOfType<VideoPlayer>().Length != 0)
        {
            foreach(VideoPlayer vp in GameObject.FindObjectsOfType<VideoPlayer>())
            {
                vp.SetDirectAudioVolume(0, cutsceneSlider.value);
            }
        }

        PlayerPrefs.SetFloat("CutsceneVol", cutsceneSlider.value);
    }

    public void Mute(bool muted)
    {
        if (muted)
        {
            preMute = cutsceneSlider.value;
            cutsceneSlider.value = 0.0f;
            onVolChange();
        }
        else
        {
            cutsceneSlider.value = preMute;
            onVolChange();
        }
    }
}
