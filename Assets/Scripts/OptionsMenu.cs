using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{

    public GameObject optionsUI;
    public GameObject pauseUI;
    private bool muteOn;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            optionsUI.SetActive(false);
            pauseUI.SetActive(true);
        } 
    }
    

    public void back()
    {
        optionsUI.SetActive(false);
        pauseUI.SetActive(true);
    }
    

    public void backTitle()
    {
        SceneManager.LoadScene(0);
    }


}
