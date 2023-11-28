using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public Slider volumeSlider;

    void Start() {
        if (!PlayerPrefs.HasKey("CutsceneSound"))
        {
            PlayerPrefs.SetFloat("CutsceneSound", 0.5f);
        }
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", .5f);
        }
        Load();
    }
    
    public void MuteToggle(bool muted) {
        if (muted) {
            AudioListener.volume = 0;
        } else {
            Load();
        }
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        ChangeVolume();
    }

    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
