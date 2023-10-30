using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public static class EventHandler
{
    public static event Action<string> PickUpItemEvent;
    public static event Action<string> UseItemEvent;
    public static event Action SwitchWeaponEvent;

    public static void OnPickUpItemEvent(string itemGuid)
    {
        PickUpItemEvent?.Invoke(itemGuid);
    }

    public static void OnUseItemEvent(string itemGuid)
    {
        UseItemEvent?.Invoke(itemGuid);
    }

    public static void OnSwitchWeaponEvent()
    {
        SwitchWeaponEvent?.Invoke();
    }
}
