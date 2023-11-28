using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuOpen : MonoBehaviour
{
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private GameObject quitButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }
}
