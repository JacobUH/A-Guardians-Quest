using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsMain : MonoBehaviour
{
    [SerializeField] private GameObject backButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void backMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
