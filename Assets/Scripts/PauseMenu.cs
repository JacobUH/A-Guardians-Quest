using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject optionsUI;
    public string MainMenu;
    public GameObject CharacterUI;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    void pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        GameManager.Instance.SetInMenuBool(true);
        CharacterUI.SetActive(false);
    }

    public void resume()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        GameManager.Instance.SetInMenuBool(false);
        CharacterUI.SetActive(true);
    }


    public void menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
    public void quitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void save()
    {
        //Save function;
    }
    public void options()
    {
        optionsUI.SetActive(true);
    }
    
}
