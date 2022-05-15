using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car Data")]
public class CarData : ScriptableObject
{
    [SerializeField] private GameObject carPrefab = null;
    [SerializeField] private string configurationName = "";
    [TextArea]
    [SerializeField] private string configurationDescription = "";
    [Header("Option Lists")]
    [SerializeField] private CarDriveOption[] availableDriveOptionsList;
    [SerializeField] private CarDriveOption[] extraDriveOptionsList;
    [SerializeField] private CarColorOption[] availableColorsList;
    [SerializeField] private CarColorOption[] extraColorOptionsList;
    [SerializeField] private CarUpholsteryOption[] availableCarUpholsteryOptionsList;
    [SerializeField] private CarUpholsteryOption[] extraCarUpholsteryOptionsList;
    [SerializeField] private CarPackagesOption[] availableCarPackageOptionsList;
    [SerializeField] private CarPackagesOption[] extraCarPackageOptionsList;

    public string ConfigurationName { get { return configurationName; } }
    public string ConfigurationDescription { get { return configurationDescription; } }
    public CarDriveOption[] AvailableDriveOptionsList { get { return availableDriveOptionsList; } }
    public CarDriveOption[] ExtraDriveOptionsList { get { return extraDriveOptionsList; } }
    public CarColorOption[] AvailableColorOptionsList { get { return availableColorsList; } }
    public CarColorOption[] ExtraColorOptionsList { get { return extraColorOptionsList; } }
    public CarUpholsteryOption[] AvailableCarUpholsteryOptionsList { get { return availableCarUpholsteryOptionsList; } }
    public CarUpholsteryOption[] ExtraCarUpholsteryOptionsList { get { return extraCarUpholsteryOptionsList; } }
    public CarPackagesOption[] AvailableCarPackageOptionsList { get { return availableCarPackageOptionsList; } }
    public CarPackagesOption[] ExtraCarPackageOptionsList { get { return extraCarPackageOptionsList; } }
}
