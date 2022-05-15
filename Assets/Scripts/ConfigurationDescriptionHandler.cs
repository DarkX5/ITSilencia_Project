using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationDescriptionHandler : MonoBehaviour
{
    public static event Action<int> onNewConfigurationSet;

    [SerializeField] private Text configurationTitleText = null;
    [SerializeField] private Text configurationDescriptionText = null;

    [SerializeField] private CarData[] carData;
    private int currentConfigIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        DataLoader.onDataLoaded += Init;
    }
    private void OnDestroy() {
        DataLoader.onDataLoaded -= Init;
    }

    void Init() {
        carData = DataLoader.Instance.CarData;
        LoadConfig(0);
    }
    private void LoadConfig(int idx) {
        configurationTitleText.text = carData[currentConfigIdx].ConfigurationName;
        configurationDescriptionText.text = carData[currentConfigIdx].ConfigurationDescription;

        onNewConfigurationSet?.Invoke(idx);
    }

    public void NextConfiguration() {
        currentConfigIdx += 1;
        if (currentConfigIdx >= carData.Length) {
            currentConfigIdx = 0;
        }
        LoadConfig(currentConfigIdx);
    }
    public void PreviousConfiguration() {
        currentConfigIdx -= 1;
        if (currentConfigIdx < 0) {
            currentConfigIdx = carData.Length - 1;
        }
        LoadConfig(currentConfigIdx);
    }
}
