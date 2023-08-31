using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : SingletonMonobehaviour<InputReader>, PlayerInput.IFreeLookActions
{
    public float longPressDuration = 0.5f;
    public bool isPressingSouthButton;
    public bool isPressingWestButton;
    public bool isPressingLeftTrigger;
    public bool isPressingRightTrigger;
    public bool isPressingDpadLeft;
    public bool isPressingDpadRight;

    public event Action SouthButtonPressEvent;
    public event Action NorthButtonPressEvent;
    public event Action EastButtonPressEvent;
    public event Action WestButtonPressEvent
        ;
    public event Action RightStickPressEvent;
    public Vector2 leftStickValue;
    public event Action LeftStickPressEvent;
    public Vector2 rightStickValue;

    public event Action DpadUpButtonPressEvent;
    public event Action DpadDownButtonPressEvent;
    public event Action DpadLeftButtonPressEvent;
    public event Action DpadRightButtonPressEvent;

    public event Action StartButtonPressEvent;
    public event Action SelectButtonPressEvent;

    public bool isPressingRightShoulder = false;
    public bool isPressingLeftShoulder = false;

    private PlayerInput playerInput;
    private bool isPressing = false;
    private float pressTime = 0f;

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
            pressTime += Time.unscaledDeltaTime;
            if (pressTime >= longPressDuration)
            {
                longPressAction?.Invoke();
                longPressAction = null;
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
            isPressingSouthButton = true;
            longPressAction = OnSouthButtonLongPress;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                SouthButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingSouthButton = false;
        }
    }

    public void OnEastButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        EastButtonPressEvent?.Invoke();
    }

    public void OnWestButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingWestButton = true;
            longPressAction = OnWestButtonLongPress;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                WestButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingWestButton = false;
        }
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
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
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
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
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
        if (!context.performed) return;
        LeftStickPressEvent?.Invoke();
    }

    public void OnRightStick(InputAction.CallbackContext context)
    {
        rightStickValue = context.ReadValue<Vector2>();
    }

    public void OnRightStickPress(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        RightStickPressEvent?.Invoke();
    }

    public void OnSouthButtonLongPress()
    {
    }

    public void OnWestButtonLongPress()
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
    }

    public void OnDpadDownButton(InputAction.CallbackContext context)
    {
    }

    public void OnDpadLeftButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingDpadLeft = true;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
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
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                DpadRightButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingDpadRight = false;
        }
    }
}
