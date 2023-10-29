using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHover : MonoBehaviour
{
    public Button button;
    public Color wantedColor;
    private Color originalColor;
    private ColorBlock cd;

    // Start is called before the first frame update
    void Start()
    {
        cd = button.colors;
        originalColor = cd.selectedColor;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeWhenHover()
    {
        cd.selectedColor = wantedColor;
        button.colors = cd;
    }
    public void changeWhenLeaves()
    {
        cd.selectedColor = originalColor;
        button.colors = cd;
    }
}
