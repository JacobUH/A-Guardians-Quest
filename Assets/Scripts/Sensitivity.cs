using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    public Slider slider;
    public 

    void Start()
    {
        if (!PlayerPrefs.HasKey("Sens"))
        {
            PlayerPrefs.SetFloat("Sens",0.4f);
        }
        slider.value = PlayerPrefs.GetFloat("Sens");
        try
        {
            setSens();
        }
        catch { }
    }


    public void setSens()
    {
        FindObjectOfType<CameraController>().cameraSensitive = PlayerPrefs.GetFloat("Sens");
    }

    public void changeSens()
    {
        PlayerPrefs.SetFloat("Sens", slider.value);
        try
        {
            setSens();
        }
        catch { }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
