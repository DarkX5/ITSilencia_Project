using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance { get; private set;}
    public static event Action onDataLoaded;
    private CarData[] carData;
    private CarOptionTextsData carOptionTexts;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public DriveOptionText[] CarDriveOptionsTexts { get { return carOptionTexts?.DriveOptionsTexts; } }
    public ColorOptionText[] CarColorOptionsTexts { get { return carOptionTexts?.ColorOptionsTexts; } }
    public UpholsteryOptionText[] CarUpholsteryOptionsTexts { get { return carOptionTexts?.UpholsteryOptionsTexts; } }
    public PackagesOptionText[] CarPackageOptionsTexts { get { return carOptionTexts?.PackageOptionsTexts; } }

    // Start is called before the first frame update
    void Start()
    {
        // get car configuration data
        carData = Resources.LoadAll<CarData>("CarOptions");

        // get option texts
        carOptionTexts = Resources.LoadAll<CarOptionTextsData>("CarOptions")?[0];
        Debug.Log($"Data Loaded: {(carOptionTexts != null ? carOptionTexts.DriveOptionsTexts.Length.ToString() : "null")} entries found");
        onDataLoaded?.Invoke();
    }
}
