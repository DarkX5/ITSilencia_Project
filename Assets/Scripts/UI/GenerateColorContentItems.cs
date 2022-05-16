
using System;

public class GenerateColorContentItems : GenerateContentItems
{
    public static event Action<int> onColorChange = null;
    protected override void LoadDataTextsForContentItems() {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarColorOptionsTexts);
        LoadCurrentConfigValue();
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx)
    {
        LoadDataTextsForContentItems();
    }
    protected override void LoadCurrentConfigValue()
    {
        managedDropdown.value = DataLoader.Instance.CurrentConfiguration.colorOption;
        onColorChange?.Invoke(managedDropdown.value);
    }
    // called from UI
    public override void SetNewOption()
    {
        DataLoader.Instance.SetColorOption(managedDropdown.value);
    }
}
