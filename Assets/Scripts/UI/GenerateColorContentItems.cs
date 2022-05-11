using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateColorContentItems : MonoBehaviour
{
    [SerializeField] private Dropdown optionDropdown = null;

    // Start is called before the first frame update
    void Start()
    {
        if (optionDropdown == null) {
            optionDropdown = GetComponentInChildren<Dropdown>();
        }
        DataLoader.onDataLoaded += GenerateContentItems;
    }
    private void OnDestroy() {
        DataLoader.onDataLoaded -= GenerateContentItems;
    }
    
    private void GenerateContentItems() {
        // get texts for options
        var driveOptionTexts = DataLoader.Instance.CarColorOptionsTexts;

        List<Dropdown.OptionData>  driveDropdownOptions = new List<Dropdown.OptionData>();
        for (int i = 0; i < driveOptionTexts.Length; i += 1) {
            driveDropdownOptions.Add(new Dropdown.OptionData(driveOptionTexts[i].text));
        }

        optionDropdown.ClearOptions();
        optionDropdown.AddOptions(driveDropdownOptions);
    }
}
