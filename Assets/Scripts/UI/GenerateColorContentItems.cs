
// using UnityEngine;
// using UnityEngine.UI;

public class GenerateColorContentItems : GenerateContentItems
{
    protected override void LoadDataTextsForContentItems() {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarColorOptionsTexts);
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx)
    {
        LoadDataTextsForContentItems();
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
