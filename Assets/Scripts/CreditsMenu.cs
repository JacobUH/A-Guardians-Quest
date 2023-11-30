using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private GameObject backButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(backButton);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
