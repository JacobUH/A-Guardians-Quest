using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ButtonGuideUI : MonoBehaviour
{
    public GameObject controllerButtonGuideUI;
    public GameObject pcButtonGuideUI;

    private void Update()
    {
        //Mouse.current.delta.value != Vector2.zero 
        if (Input.anyKeyDown || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
        {
            if (!pcButtonGuideUI.activeSelf)
            {
                controllerButtonGuideUI.SetActive(false);
                pcButtonGuideUI.SetActive(true);
            }
        }
        if (Gamepad.current != null)
        {
            foreach (InputControl button in Gamepad.current.allControls)
            {
                if (button is ButtonControl && button.IsPressed())
                {
                    if (!controllerButtonGuideUI.activeSelf)
                    {
                        pcButtonGuideUI.SetActive(false);
                        controllerButtonGuideUI.SetActive(true);
                    }
                }
            }
        }
    }
}
