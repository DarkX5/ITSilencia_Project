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
}
