using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    void Start() {
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
    }
    
    public void MuteToggle(bool muted) {
        if (muted) {
            AudioListener.volume = 0;
        } else {
            AudioListener.volume = 1;
        }
    }

    public void ChangeVolume(float value) {
        AudioListener.volume = value;
        Save(value);
    }

    private void Load() {
        float value = PlayerPrefs.GetFloat("musicVolume");
        ChangeVolume(value);
    }

    private void Save(float value) {
        PlayerPrefs.SetFloat("volume", value);
    }
}
