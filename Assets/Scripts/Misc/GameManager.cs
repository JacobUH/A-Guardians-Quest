using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    private bool isInMenu;
    private float cursorHideDelay = 3.0f; 
    private float lastMouseActivityTime;

    public bool IsInMenu => isInMenu;
    protected override void Awake()
    {
        base.Awake();
        LockCursor();
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        if (isInMenu && mouseDelta != Vector2.zero)
        {
            UnlockCursor();
            lastMouseActivityTime = Time.time;
        }
        else if (Time.time - lastMouseActivityTime >= cursorHideDelay)
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInMenuBool(bool b)
    {
        isInMenu = b;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
