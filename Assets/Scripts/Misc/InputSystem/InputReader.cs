using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : SingletonMonobehaviour<InputReader>, PlayerInput.IFreeLookActions
{
    public float longPressDuration = 0.2f;

    public bool isPressingLTRT;
    public bool isPressingLTRS;

    public bool isPressingSouthButton;
    public event Action SouthButtonPressEvent;
    public event Action SouthButtonLongPressEvent;

    public bool isPressingNorthButton;
    public event Action NorthButtonPressEvent;
    public event Action NorthButtonLongPressEvent;

    public bool isPressingEastButton;
    public event Action EastButtonPressEvent;
    public event Action EastButtonLongPressEvent;

    public bool isPressingWestButton;
    public event Action WestButtonPressEvent;
    public event Action WestButtonLongPressEvent;

    public bool isPressingLeftTrigger;
    public event Action LeftTriggerPressEvent;
    public event Action LeftTriggerLongPressEvent;

    public bool isPressingRightTrigger;
    public event Action RightTriggerPressEvent;
    public event Action RightTriggerLongPressEvent;

    public bool isPressingLeftShoulder;
    public event Action LeftShoulderPressEvent;
    public event Action LeftShoulderLongPressEvent;

    public bool isPressingRightShoulder;
    public event Action RightShoulderPressEvent;
    public event Action RightShoulderLongPressEvent;

    public Vector2 rightStickValue;
    public event Action RightStickPressEvent;
    public Vector2 leftStickValue;
    public event Action LeftStickPressEvent;

    public bool isPressingDpadUp;
    public event Action DpadUpButtonPressEvent;
    public bool isPressingDpadDown;
    public event Action DpadDownButtonPressEvent;
    public bool isPressingDpadLeft;
    public event Action DpadLeftButtonPressEvent;
    public bool isPressingDpadRight;
    public event Action DpadRightButtonPressEvent;

    public event Action StartButtonPressEvent;
    public event Action StartButtonLongPressEvent;
    public event Action SelectButtonPressEvent;
    public event Action SelectButtonLongPressEvent;

    private PlayerInput playerInput;
    private bool isPressing = false;
    private float pressTime = 0f;

    private Action longPressAction;

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
        if (context.started)
        {
            isPressing = true;
            isPressingNorthButton = true;
            longPressAction = NorthButtonLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                NorthButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingNorthButton = false;
        }
    }

    public void OnSouthButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingSouthButton = true;
            longPressAction = SouthButtonLongPressEvent;
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
        if (context.started)
        {
            isPressing = true;
            isPressingEastButton = true;
            longPressAction = EastButtonLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                EastButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingEastButton = false;
        }
    }

    public void OnWestButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingWestButton = true;
            longPressAction = WestButtonLongPressEvent;
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
        if (context.started)
        {
            isPressing = true;
            isPressingLeftTrigger = true;
            longPressAction = LeftTriggerLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                LeftTriggerPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingLeftTrigger = false;
        }
    }

    public void OnRightTrigger(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingRightTrigger = true;
            longPressAction = RightTriggerLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                RightTriggerPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingRightTrigger = false;
        }
    }

    public void OnLeftShoulder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingLeftShoulder = true;
            longPressAction = LeftShoulderLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                LeftShoulderPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingLeftShoulder = false;
        }
    }

    public void OnRightShoulder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingRightShoulder = true;
            longPressAction = RightShoulderLongPressEvent;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                RightShoulderPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingRightShoulder = false;
        }
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            longPressAction = StartButtonLongPressEvent;
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
            longPressAction = SelectButtonLongPressEvent;
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

    public void OnDpadUpButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingDpadUp = true;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                DpadUpButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingDpadUp = false;
        }
    }

    public void OnDpadDownButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            isPressingDpadDown = true;
            pressTime = 0f;
        }
        else if (context.canceled)
        {
            if (pressTime < longPressDuration && isPressing)
            {
                DpadDownButtonPressEvent?.Invoke();
            }
            isPressing = false;
            isPressingDpadDown = false;
        }
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

    public void OnLTRT(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressingLTRT = true;
        }
        else if (context.canceled)
        {
            isPressingLTRT = false;
        }
    }

    public void OnLTRS(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressingLTRS = true;
        }
        else if (context.canceled)
        {
            isPressingLTRS = false;
        }
    }
}
