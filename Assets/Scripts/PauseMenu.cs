using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject optionsUI;
    public string MainMenu;
    public GameObject CharacterUI;
    public bool videoPlaying;

    private void Start()
    {
        InputReader.Instance.StartButtonPressEvent += StartButtonHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlaying) return;
        if (optionsUI.activeSelf) return;
        if (GameManager.Instance.IsInMenu) return;

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
    
    private void StartButtonHandler()
    {
        if (optionsUI.activeSelf) return;
        if (GameManager.Instance.IsInMenu) return;
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
