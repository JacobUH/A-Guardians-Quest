using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGamge()
    {
        SceneManager.LoadScene(1);
    }
    public void Option()
    {
        SceneManager.LoadScene(3);
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
