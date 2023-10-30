using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMain : MonoBehaviour
{
    public void backMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
