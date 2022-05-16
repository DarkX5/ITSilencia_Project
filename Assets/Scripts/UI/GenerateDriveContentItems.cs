
public class GenerateDriveContentItems : GenerateContentItems
{
    protected override void LoadDataTextsForContentItems()
    {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarDriveOptionsTexts);
        LoadCurrentConfigValue();
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx) {
        LoadDataTextsForContentItems();
    }
    protected override void LoadCurrentConfigValue()
    {
        managedDropdown.value = DataLoader.Instance.CurrentConfiguration.driveOption;
    }
    // called from UI
    public override void SetNewOption()
    {
        DataLoader.Instance.SetDriveOption(managedDropdown.value);
    }
}
