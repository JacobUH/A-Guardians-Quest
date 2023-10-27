using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{

    public GameObject optionsUI;
    public GameObject pauseUI;
    public AudioMixer mixer;
    public Slider sliderVol;
    public Toggle mute;
    private float lastVol;

    // Start is called before the first frame update
    void Start()
    {
        lastVol = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            mute.value = true;
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));
        }
        else
        {
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));
            mute.value = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void back()
    {
        optionsUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void volumeChange()
    {
        mixer.SetFloat("MusicVol", sliderVol.value);
        PlayerPrefs.SetFloat("MusicVolume", sliderVol.value);
    }

    public void backTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void muteVol()
    {
        if (mute.value)
        {
            lastVol = sliderVol.value;
            mixer.SetFloat("MusicVol", 0);
            PlayerPrefs.SetFloat("MusicVolume", 0);
        }
        else
        {
            mixer.SetFloat("MusicVol", lastVol);
            PlayerPrefs.SetFloat("MusicVolume", lastVol);
        }
    }
    


}
