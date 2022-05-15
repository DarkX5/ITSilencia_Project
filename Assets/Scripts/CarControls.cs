using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControls : MonoBehaviour
{
#region Static Event Actions
    public static event Action onOpenDoorsCmd;
    public static event Action onCloseDoorsCmd;
    public static event Action onLightsTurnOn;
    public static event Action onLightsTurnOff;
    public static event Action onNextColor;
    public static event Action onPreviousColor;

    public static event Action<int> onSetDrive;
    public static event Action<int> onSetColor;
    public static event Action<int> onSetUpholstery;
    public static event Action<int> onSetPackage;
#endregion
    [SerializeField] private float initDelay = 0.25f;

    private void Start() {
        Invoke("InitColor", initDelay);
    }

    private void InitColor() {
        SetColorByIdx(1);
        SetColorByIdx(0);
    }

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
    // called from UI 
    public void SetDrive(Dropdown valuesDropdown) {
        onSetDrive?.Invoke(valuesDropdown.value);
    }
    // called from UI 
    public void SetColor(Dropdown valuesDropdown)
    {
        onSetColor?.Invoke(valuesDropdown.value);
    }
    // called from UI 
    public void SetUpholstery(Dropdown valuesDropdown)
    {
        onSetUpholstery?.Invoke(valuesDropdown.value);
    }
    // called from UI 
    public void SetPackage(Dropdown valuesDropdown)
    {
        onSetPackage?.Invoke(valuesDropdown.value);
    }


    public void SetColorByIdx(int idx)
    {
        onSetColor?.Invoke(idx);
    }
}
