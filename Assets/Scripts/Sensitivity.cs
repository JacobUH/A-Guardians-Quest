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
            PlayerPrefs.SetInt("Sens",0);
        }
        slider.value = PlayerPrefs.GetInt("Sens");
    }


    public void setSens()
    {
        
    }
}
