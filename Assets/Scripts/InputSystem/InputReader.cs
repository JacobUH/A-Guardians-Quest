using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : SingletonMonobehaviour<InputReader>, PlayerInput.IFreeLookActions
{
    public float longPressDuration = 0.5f;
    public bool isPressingWestButton;
    public bool isPressingLeftTrigger;
    public bool isPressingRightTrigger;
    public bool isPressingDpadLeft;
    public bool isPressingDpadRight;
    public Vector2 leftStickValue;
    public Vector2 rightStickValue;
    public event Action SouthButtonPressEvent;
    public event Action EastButtonPressEvent;
    public event Action NorthButtonPressEvent;
    public event Action LockOnEvent;
    public event Action StartButtonPressEvent;
    public event Action SelectButtonPressEvent;
    public event Action DpadUpButtonPressEvent;
    public event Action DpadDownButtonPressEvent;
    public event Action DpadLeftButtonPressEvent;
    public event Action DpadRightButtonPressEvent;
    public bool isPressingRightShoulder = false;
    public bool isPressingLeftShoulder = false;
    private PlayerInput playerInput;

    private bool isPressing = false;
    private float timer = 0f;

    private UnityAction longPressAction;
    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        playerInput.FreeLook.SetCallbacks(this);
    }
    private void Update()
    {
        if (isPressing)
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= longPressDuration)
            {
                longPressAction.Invoke();
                isPressing = false;
            }
        }
    }

    public void EnableFreelookInputReader()
    {
        playerInput.FreeLook.Enable();
    }

    public void DisableFreelookInputReader()
    {
        playerInput.FreeLook.Disable();
    }

    public void OnNorthButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        NorthButtonPressEvent?.Invoke();
    }

    public void OnSouthButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            longPressAction = OnSouthButtonLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                SouthButtonPressEvent?.Invoke();
            }
            isPressing = false;
        }
    }

    public void OnEastButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        EastButtonPressEvent?.Invoke();
    }

    public void OnWestButton(InputAction.CallbackContext context)
    {
        if (context.performed) isPressingWestButton = true;
        else if (context.canceled) isPressingWestButton = false;
    }


    public void OnLeftTrigger(InputAction.CallbackContext context)
    {
        if (context.performed) isPressingLeftTrigger = true;
        else if (context.canceled) isPressingLeftTrigger = false;
    }

    public void OnRightTrigger(InputAction.CallbackContext context)
    {
        if (context.performed) isPressingRightTrigger = true;
        else if (context.canceled) isPressingRightTrigger = false;
    }

    public void OnLeftShoulder(InputAction.CallbackContext context)
    {
        if (context.performed) isPressingLeftShoulder = true;
        else if (context.canceled) isPressingLeftShoulder = false;
    }

    public void OnRightShoulder(InputAction.CallbackContext context)
    {
        if (context.performed) isPressingRightShoulder = true;
        else if (context.canceled) isPressingRightShoulder = false;
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            longPressAction = OnStartLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                StartButtonPressEvent?.Invoke();
            }
            isPressing = false;
        }
    }

    public void OnSelectButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            longPressAction = OnStartLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                SelectButtonPressEvent?.Invoke();
            }
            isPressing = false;
        }
    }

    public void OnLeftStick(InputAction.CallbackContext context)
    {
        leftStickValue = context.ReadValue<Vector2>();
    }

    public void OnLeftStickPress(InputAction.CallbackContext context)
    {
    }

    public void OnRightStick(InputAction.CallbackContext context)
    {
        rightStickValue = context.ReadValue<Vector2>();
    }

    public void OnRightStickPress(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        LockOnEvent?.Invoke();
    }

    public void OnSouthButtonLongPress()
    {
    }

    public void OnStartLongPress()
    {
    }

    public void OnSelectLongPress()
    {
    }

    public void OnDpadUpButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            //longPressAction = OnStartLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                DpadUpButtonPressEvent?.Invoke();
            }
            isPressing = false;
        }
    }

    public void OnDpadDownButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            //longPressAction = OnStartLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                DpadDownButtonPressEvent?.Invoke();
            }
            isPressing = false;
        }
    }

    public void OnDpadLeftButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            //longPressAction = OnStartLongPress;
            timer = 0f;
            isPressingDpadLeft = true;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                DpadLeftButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingDpadLeft = false;
        }
    }

    public void OnDpadRightButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingDpadRight = true;
            //longPressAction = OnStartLongPress;
            timer = 0f;
        }
        else if (context.canceled)
        {
            if (timer < longPressDuration && isPressing)
            {
                DpadRightButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingDpadRight = false;
        }
    }
}
