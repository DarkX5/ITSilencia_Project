using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car Data")]
public class CarData : ScriptableObject
{
    [SerializeField] private GameObject carPrefab = null;
    [SerializeField] private string carName = "";
    [TextArea]
    [SerializeField] private string carDescription = "";
    [SerializeField] private CarDriveOption[] availableDriveOptionsList;
    [SerializeField] private CarDriveOption[] extraDriveOptionsList;
    [SerializeField] private CarColorOption[] availableColorsList;
    [SerializeField] private CarColorOption[] extraColorOptionsList;
    [SerializeField] private CarUpholsteryOption[] availableCarUpholsteryOptionsList;
    [SerializeField] private CarUpholsteryOption[] extraCarUpholsteryOptionsList;
    [SerializeField] private CarPackagesOption[] availableCarPackageOptionsList;
    [SerializeField] private CarPackagesOption[] extraCarPackageOptionsList;
}
