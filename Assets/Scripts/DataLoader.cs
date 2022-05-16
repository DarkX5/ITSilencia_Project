using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameConfigData
{
    public GameConfigData() {}
    public GameConfigData(  int _idx, string _name, string _description, 
                            int _driveOption, int _colorOption, int _upholsteryOption, 
                            int _packageOption) {
        idx = _idx;
        configurationName = _name;
        configurationDescription = _description;
        driveOption = _driveOption;
        colorOption = _colorOption;
        upholsteryOption = _upholsteryOption;
        packageOption = _packageOption;
    }
    public int idx = 0;
    public string configurationName = "";
    public string configurationDescription = "";
    public int driveOption = 0;
    public int colorOption = 0;
    public int upholsteryOption = 0;
    public int packageOption = 0;
}
public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance { get; private set; }
    public static event Action onDataLoaded;

    private float dataLoadCheckFrequency = 0.25f;
    private CarData[] configurationData;
    [SerializeField] private CarOptionTextsData configurationOptionTexts;
    [SerializeField] private float onDataLoadedCallDelay = 0.5f;
    [SerializeField] private GameConfigData gameData = new GameConfigData();
    [SerializeField] private List<string> savedConfigurationLists = null;

    private bool savedConfigurationsLoaded = false;
    private bool carOptionsDataLoaded = false;
    private bool carOptionTextsLoaded = false;
    private bool allDataLoaded = false;

    private int currentConfingIdx = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
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
    public List<string> SavedConfigurationLists { get { return savedConfigurationLists; } }
    public GameConfigData CurrentConfiguration { get { return gameData; } }

    // Start is called before the first frame update
    void Start()
    {
        // get car configuration data
        configurationData = Resources.LoadAll<CarData>("CarOptions");
        carOptionsDataLoaded = true;

        // get option texts
        configurationOptionTexts = Resources.LoadAll<CarOptionTextsData>("CarOptions")?[0];
        carOptionTextsLoaded = true;

        LoadConfigurations();

        StartCoroutine(WaitForDataLoadCO());

        ConfigurationDescriptionHandler.onNewConfigurationSet += ChangeCurrentConfiguration;
    }
    private void OnDestroy()
    {
        ConfigurationDescriptionHandler.onNewConfigurationSet -= ChangeCurrentConfiguration;
    }

    private IEnumerator WaitForDataLoadCO()
    {
        // wait for loaders
        yield return new WaitForSeconds(dataLoadCheckFrequency);

        // check if all data loaded
        if (savedConfigurationsLoaded == true
            && carOptionsDataLoaded == true
            && carOptionTextsLoaded == true)
        {
            allDataLoaded = true;

            // check if any subscribers
            if (onDataLoaded != null)
            {
                // call onDataLoaded with an extra delay to wait for other scene elements to load
                Invoke("OnDataLoaded", onDataLoadedCallDelay);
            }
        }
        else
        {
            allDataLoaded = false;
            // check againg in dataLoadCheckFrequency seconds
            StartCoroutine(WaitForDataLoadCO());
        }
    }
    private void OnDataLoaded()
    {
        onDataLoaded?.Invoke();
    }
    private void ChangeCurrentConfiguration(int newIdx)
    {
        currentConfingIdx = newIdx;
    }
    private OptionText[] GetParsedDriveOptionTexts()
    {
        List<OptionText> returnList = new List<OptionText>();
        bool found;
        // get available options for configuration
        foreach (CarDriveOption option in configurationData[currentConfingIdx].AvailableDriveOptionsList)
        {
            found = false;
            for (int i = 0; !found && i < configurationOptionTexts.DriveOptionsTexts.Length; i += 1)
            {
                if (option == configurationOptionTexts.DriveOptionsTexts[i].carDriveOption)
                {
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

    private void LoadConfigurations()
    {
        // clear saved config list
        savedConfigurationLists = new List<string>();
        // init loading bool
        savedConfigurationsLoaded = false;

        bool allConfigurationsFound = false;
        for (int i = 0; !allConfigurationsFound && (i < 100); i += 1)
        {
            // check if configuration exists
            if (PlayerPrefs.HasKey($"Configuration_{i}_Name"))
            {
                // add config name to saved configs
                savedConfigurationLists.Add(PlayerPrefs.GetString($"Configuration_{i}_Name"));
            }
            else
            {
                // no more configs -> end loading
                allConfigurationsFound = true;
            }
        }

        // check loading as complete
        savedConfigurationsLoaded = true;
    }

    public void SaveCurrentConfig()
    {
        SaveConfig(gameData);
    }
    public void SaveConfigByName(string configName) {
        // set id to las position in list
        gameData.idx = savedConfigurationLists.Count;

        // check if name is found in saved configurations
        for (int i = 0; i < savedConfigurationLists.Count; i += 1)
        {
            if (savedConfigurationLists[i] == configName)
            {
                // set id of found configuration
                gameData.idx = i;
            }
        }

        // save new / overwrite existing configuration
        SaveConfig(gameData);
    }
    private void SaveConfig(GameConfigData newGameData)
    {
        /*v TODO - save to disk (JSON serialize & File.Write) v*/
        PlayerPrefs.SetString($"Configuration_{newGameData.idx}_Name", newGameData.configurationName);
        PlayerPrefs.SetString($"Configuration_{newGameData.idx}_Description", newGameData.configurationDescription);
        PlayerPrefs.SetInt($"Configuration_{newGameData.idx}_Drive", newGameData.driveOption);
        PlayerPrefs.SetInt($"Configuration_{newGameData.idx}_Color", newGameData.colorOption);
        PlayerPrefs.SetInt($"Configuration_{newGameData.idx}_Upholstery", newGameData.upholsteryOption);
        PlayerPrefs.SetInt($"Configuration_{newGameData.idx}_Package", newGameData.packageOption);

        LoadConfig(newGameData.idx);
    }
    public GameConfigData LoadConfig(int idx)
    {
        // update configurations list
        LoadConfigurations();

        // check key exists before loading
        if (!PlayerPrefs.HasKey($"Configuration_{idx}_Name")) { 
            Debug.Log("Config Not found");
            // call data loaded to disable any UI elements
            if (idx < 1) {
                onDataLoaded?.Invoke();
            }
            return null; 
        }

        /*v TODO - load from disk (JSON serialize & File.Read) v*/
        GameConfigData resultData = new GameConfigData();
        resultData.idx = idx;
        resultData.configurationName = PlayerPrefs.GetString($"Configuration_{idx}_Name");
        resultData.configurationDescription = PlayerPrefs.GetString($"Configuration_{idx}_Description");
        resultData.driveOption = PlayerPrefs.GetInt($"Configuration_{idx}_Drive");
        resultData.colorOption = PlayerPrefs.GetInt($"Configuration_{idx}_Color");
        resultData.upholsteryOption = PlayerPrefs.GetInt($"Configuration_{idx}_Upholstery");
        resultData.packageOption = PlayerPrefs.GetInt($"Configuration_{idx}_Package");

        // save loaded config as current one
        gameData = resultData;

        // // wait for data load to update UI
        // StartCoroutine(WaitForDataLoadCO());
        onDataLoaded?.Invoke();

        return resultData;
    }
    
    public bool SetNameOption(string newName)
    {
        gameData.configurationName = newName;
        return true;
    }
    public bool SetDescriptionOption(string newDescription)
    {
        gameData.configurationDescription = newDescription;
        return true;
    }
    public bool SetConfigIdx(int newIdx)
    {
        gameData.idx = newIdx;
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
