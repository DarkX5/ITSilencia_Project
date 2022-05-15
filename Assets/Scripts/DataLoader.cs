using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameConfigData {
    public int idx = 0;
    public string configurationName = "";
    public int driveOption = 0;
    public int colorOption = 0;
    public int upholsteryOption = 0;
    public int packageOption = 0;
}
public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance { get; private set;}
    public static event Action onDataLoaded;

    private CarData[] configurationData;
    [SerializeField] private CarOptionTextsData configurationOptionTexts;
    [SerializeField] private float onDataLoadedCallDelay = 0.5f;
    [SerializeField] private GameConfigData gameData = new GameConfigData();

    private int currentConfingIdx = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public OptionText[] CarDriveOptionsTexts { get { return GetParsedDriveOptionTexts(); } } 
    public OptionText[] CarColorOptionsTexts { get { return GetParsedColorOptionTexts(); } }
    public OptionText[] CarUpholsteryOptionsTexts { get { return GetParsedUpholsteryOptionTexts(); } }
    public OptionText[] CarPackageOptionsTexts { get { return GetParsedPackageOptionTexts(); } }
    public CarData[] CarData { get { return configurationData; } }
    public CarData CurrentCarData { get { return configurationData[currentConfingIdx]; } }
    public CarOptionTextsData ConfigurationOptionTexts { get { return configurationOptionTexts; } }

    // Start is called before the first frame update
    void Start()
    {
        // get car configuration data
        configurationData = Resources.LoadAll<CarData>("CarOptions");

        // get option texts
        configurationOptionTexts = Resources.LoadAll<CarOptionTextsData>("CarOptions")?[0];
        Debug.Log($"Data Loaded: {(configurationOptionTexts != null ? configurationOptionTexts.DriveOptionsTexts.Length.ToString() : "null")} entries found");

        if (onDataLoaded != null) {
            Invoke("OnDataLoaded", onDataLoadedCallDelay);
        }

        ConfigurationDescriptionHandler.onNewConfigurationSet += ChangeCurrentConfiguration;
    }
    private void OnDestroy() {
        ConfigurationDescriptionHandler.onNewConfigurationSet -= ChangeCurrentConfiguration;
    }

    private void OnDataLoaded(){
        onDataLoaded?.Invoke();
    }
    private void ChangeCurrentConfiguration(int newIdx) {
        currentConfingIdx = newIdx;
    }
    private OptionText[] GetParsedDriveOptionTexts() {
        List<OptionText> returnList = new List<OptionText>();
        bool found;
        // get available options for configuration
        foreach (CarDriveOption option in configurationData[currentConfingIdx].AvailableDriveOptionsList) {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.DriveOptionsTexts.Length; i += 1) {
                if (option == configurationOptionTexts.DriveOptionsTexts[i].carDriveOption) {
                    returnList.Add(new OptionText(configurationOptionTexts.DriveOptionsTexts[i].text));
                    found = true;
                }
            }
        }
        // get extra options for configuration
        foreach (CarDriveOption option in configurationData[currentConfingIdx].ExtraDriveOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.DriveOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.DriveOptionsTexts[i].carDriveOption)
                {
                    returnList.Add(new OptionText($"{configurationOptionTexts.DriveOptionsTexts[i].text}*"));
                    found = true;
                }
            }
        }


        return returnList.ToArray();
    }
    private OptionText[] GetParsedColorOptionTexts()
    {
        List<OptionText> returnList = new List<OptionText>();
        bool found;
        // get available options for configuration
        foreach (CarColorOption option in configurationData[currentConfingIdx].AvailableColorOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.ColorOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.ColorOptionsTexts[i].carColorOption)
                {
                    returnList.Add(new OptionText(configurationOptionTexts.ColorOptionsTexts[i].text));
                    found = true;
                }
            }
        }
        // get extra options for configuration
        foreach (CarColorOption option in configurationData[currentConfingIdx].ExtraColorOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.ColorOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.ColorOptionsTexts[i].carColorOption)
                {
                    returnList.Add(new OptionText($"{configurationOptionTexts.ColorOptionsTexts[i].text}*"));
                    found = true;
                }
            }
        }


        return returnList.ToArray();
    }
    private OptionText[] GetParsedUpholsteryOptionTexts()
    {
        List<OptionText> returnList = new List<OptionText>();
        bool found;
        // get available options for configuration
        foreach (CarUpholsteryOption option in configurationData[currentConfingIdx].AvailableCarUpholsteryOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.UpholsteryOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.UpholsteryOptionsTexts[i].carUpholsteryOption)
                {
                    returnList.Add(new OptionText(configurationOptionTexts.UpholsteryOptionsTexts[i].text));
                    found = true;
                }
            }
        }
        // get extra options for configuration
        foreach (CarUpholsteryOption option in configurationData[currentConfingIdx].ExtraCarUpholsteryOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.UpholsteryOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.UpholsteryOptionsTexts[i].carUpholsteryOption)
                {
                    returnList.Add(new OptionText($"{configurationOptionTexts.UpholsteryOptionsTexts[i].text}*"));
                    found = true;
                }
            }
        }


        return returnList.ToArray();
    }
    private OptionText[] GetParsedPackageOptionTexts()
    {
        List<OptionText> returnList = new List<OptionText>();
        bool found;
        // get available options for configuration
        foreach (CarPackagesOption option in configurationData[currentConfingIdx].AvailableCarPackageOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.PackageOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.PackageOptionsTexts[i].carPackagesOption)
                {
                    returnList.Add(new OptionText(configurationOptionTexts.PackageOptionsTexts[i].text));
                    found = true;
                }
            }
        }
        // get extra options for configuration
        foreach (CarPackagesOption option in configurationData[currentConfingIdx].ExtraCarPackageOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.PackageOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.PackageOptionsTexts[i].carPackagesOption)
                {
                    returnList.Add(new OptionText($"{configurationOptionTexts.PackageOptionsTexts[i].text}*"));
                    found = true;
                }
            }
        }


        return returnList.ToArray();
    }

    private void SaveConfig() {
        /*v TODO - save to disk v*/
        PlayerPrefs.SetString($"Configuration_{gameData.idx}", gameData.configurationName);
        PlayerPrefs.SetInt($"Configuration_{gameData.idx}_drive", gameData.driveOption);
        PlayerPrefs.SetInt($"Configuration_{gameData.idx}_color", gameData.colorOption);
        PlayerPrefs.SetInt($"Configuration_{gameData.idx}_upholstery", gameData.upholsteryOption);
        PlayerPrefs.SetInt($"Configuration_{gameData.idx}_package", gameData.packageOption);

    }
    private void LoadConfig()
    {
        /*v TODO - load from disk v*/
        gameData = new GameConfigData();
        gameData.configurationName = PlayerPrefs.GetString($"Configuration_{gameData.idx}");
        gameData.driveOption = PlayerPrefs.GetInt($"Configuration_{gameData.idx}_drive");
        gameData.colorOption = PlayerPrefs.GetInt($"Configuration_{gameData.idx}_color");
        gameData.upholsteryOption = PlayerPrefs.GetInt($"Configuration_{gameData.idx}_upholstery");
        gameData.packageOption = PlayerPrefs.GetInt($"Configuration_{gameData.idx}_package");
    }

    public bool SetNameOption(string newOption)
    {
        gameData.configurationName = newOption;
        return true;
    }
    public bool SetDriveOption(int newOption)
    {
        gameData.driveOption = newOption;
        return true;
    }
    public bool SetColorOption(int newOption)
    {
        gameData.colorOption = newOption;
        return true;
    }
    public bool SetUpholsteryOption(int newOption)
    {
        gameData.upholsteryOption = newOption;
        return true;
    }
    public bool SetPackageOption(int newOption)
    {
        gameData.packageOption = newOption;
        return true;
    }
}
