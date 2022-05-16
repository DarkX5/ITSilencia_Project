using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateContentItems : MonoBehaviour
{
    [SerializeField] protected Dropdown managedDropdown = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (managedDropdown == null)
        {
            managedDropdown = GetComponentInChildren<Dropdown>();
        }

        DataLoader.onDataLoaded += LoadDataTextsForContentItems;
        ConfigurationDescriptionHandler.onNewConfigurationSet += LoadDataTextsForContentItemsAfterConfig;
    }
    private void OnDestroy()
    {
        DataLoader.onDataLoaded -= LoadDataTextsForContentItems;
        ConfigurationDescriptionHandler.onNewConfigurationSet -= LoadDataTextsForContentItemsAfterConfig;
    }

    protected virtual void LoadDataTextsForContentItems() { }
    protected virtual void LoadDataTextsForContentItemsAfterConfig(int configurationIdx) { }
    protected virtual void LoadCurrentConfigValue() {}
    // called from UI
    public virtual void SetNewOption() { }

    protected void GenerateDropdownContent(OptionText[] dropdownContentTexts)
    {
        managedDropdown.ClearOptions();
        
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        for (int i = 0; i < dropdownContentTexts.Length; i += 1)
        {
            dropdownOptions.Add(new Dropdown.OptionData(dropdownContentTexts[i].text));
        }

        managedDropdown.AddOptions(dropdownOptions);

        if (managedDropdown.options.Count > 0) {
            managedDropdown.enabled = true;
        } else {
            managedDropdown.enabled = false;
        }
    }
    protected void GenerateDropdownContent(string[] dropdownContentTexts)
    {
        managedDropdown.ClearOptions();

        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        for (int i = 0; i < dropdownContentTexts.Length; i += 1)
        {
            dropdownOptions.Add(new Dropdown.OptionData(dropdownContentTexts[i]));
        }

        managedDropdown.AddOptions(dropdownOptions);

        if (managedDropdown.options.Count > 0)
        {
            managedDropdown.enabled = true;
        }
        else
        {
            // disable dropdown if it has not options 
            managedDropdown.enabled = false;
        }
    }
}
