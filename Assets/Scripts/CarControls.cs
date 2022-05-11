using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public static event Action onOpenDoorsCmd;
    public static event Action onCloseDoorsCmd;
    public static event Action onLightsTurnOn;
    public static event Action onLightsTurnOff;
    public static event Action onNextColor;
    public static event Action onPreviousColor;

    // called from UI
    public void OpenDoors() {
        onOpenDoorsCmd?.Invoke();
    }
    // called from UI
    public void CloseDoors() {
        onCloseDoorsCmd?.Invoke();
    }

    // called from UI
    public void TurnOnLightsCmd()
    {
        onLightsTurnOn?.Invoke();
    }
    // called from UI
    public void TurnOffLightsCmd()
    {
        onLightsTurnOff?.Invoke();
    }
    // called from UI
    public void SetNextColor()
    {
        onNextColor?.Invoke();
    }
    // called from UI
    public void SetPreviousColor()
    {
        onPreviousColor?.Invoke();
    }
}
