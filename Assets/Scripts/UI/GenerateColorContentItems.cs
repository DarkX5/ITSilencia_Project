
using UnityEngine;
using UnityEngine.UI;

public class GenerateColorContentItems : GenerateContentItems
{
    // Start is called before the first frame update
    void Start()
    {
        if (managedDropdown == null)
        {
            managedDropdown = GetComponentInChildren<Dropdown>();
        }

        DataLoader.onDataLoaded += LoadDataTextsForContentItems;
    }
    private void OnDestroy()
    {
        DataLoader.onDataLoaded -= LoadDataTextsForContentItems;
    }
    
    protected override void LoadDataTextsForContentItems() {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarColorOptionsTexts);
    }


    // [SerializeField] private Dropdown optionDropdown = null;

    // private void GenerateContentItems() {
    //     // get texts for options
    //     var optionTexts = DataLoader.Instance.CarColorOptionsTexts;

    //     List<Dropdown.OptionData>  driveDropdownOptions = new List<Dropdown.OptionData>();
    //     for (int i = 0; i < optionTexts.Length; i += 1) {
    //         driveDropdownOptions.Add(new Dropdown.OptionData(optionTexts[i].text));
    //     }

    //     optionDropdown.ClearOptions();
    //     optionDropdown.AddOptions(driveDropdownOptions);
    // }

    // // called from UI
    // public void ChangeColor(Dropdown colorDropdown) {

    // }
}
