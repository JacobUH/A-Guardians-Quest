using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour {

    public Slider volumeSlider;
    public Slider cutsceneSlider;

    private float preMute;
    private float cutscenePreMute;

    void Start() {
        
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", .5f);
        }
        if (!PlayerPrefs.HasKey("CutsceneVol"))
        {
            PlayerPrefs.SetFloat("CutsceneVol", .5f);
        }
        Load();
    }
    /*
    public void MuteToggle(bool muted) {
        if (muted) {
            AudioListener.volume = 0;
        } else {
            Load();
        }
    }*/


    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        cutsceneSlider.value = PlayerPrefs.GetFloat("CutsceneVol");
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    /*
    public void muteCutscene(bool muted)
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
    }*/

    public void onVolChange()
    {
        if (GameObject.FindObjectsOfType<VideoPlayer>().Length != 0)
        {
            foreach (VideoPlayer vp in GameObject.FindObjectsOfType<VideoPlayer>())
            {
                vp.SetDirectAudioVolume(0, cutsceneSlider.value);
            }
        }

        PlayerPrefs.SetFloat("CutsceneVol", cutsceneSlider.value);
    }
}
