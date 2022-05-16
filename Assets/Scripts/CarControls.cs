using System;
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
    [SerializeField] private float initDelay = 0.75f;
    [SerializeField] private CarColor carColorControl = null;

    private void Start() {
        GenerateColorContentItems.onColorChange += SetColorByIdx;
        Invoke("InitColor", initDelay);
    }
    private void OnDestroy() {
        GenerateColorContentItems.onColorChange -= SetColorByIdx;
    }

    private void InitColor() {
        carColorControl = FindObjectOfType<CarColor>();
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
        // onNextColor?.Invoke();
    }
    // called from UI
    public void SetPreviousColor()
    {
        // onPreviousColor?.Invoke();
    }
    // called from UI 
    public void SetDrive(Dropdown valuesDropdown) {
        onSetDrive?.Invoke(valuesDropdown.value);
    }
    // called from UI 
    public void SetColor(Dropdown valuesDropdown)
    {
        SetColorByIdx(valuesDropdown.value);
        // onSetColor?.Invoke(valuesDropdown.value);
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
        ColorOptionText[] allColorTexts = DataLoader.Instance.ConfigurationOptionTexts.ColorOptionsTexts;
        CarColorOption colorOption;
        if (idx >= DataLoader.Instance.CurrentCarData.AvailableColorOptionsList.Length) {
            idx -= DataLoader.Instance.CurrentCarData.AvailableColorOptionsList.Length;
            colorOption = DataLoader.Instance.CurrentCarData.ExtraColorOptionsList[idx];
        } else {
            colorOption = DataLoader.Instance.CurrentCarData.AvailableColorOptionsList[idx];
        }

        for(int i = 0; i < allColorTexts.Length; i += 1) {
            if (colorOption == allColorTexts[i].carColorOption) {
                carColorControl?.SetColourByIndex(i);
            }
        }
    }
}
