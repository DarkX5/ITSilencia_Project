
public class GeneratePackageContentItems : GenerateContentItems
{
    protected override void LoadDataTextsForContentItems()
    {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarPackageOptionsTexts);
        LoadCurrentConfigValue();
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx)
    {
        LoadDataTextsForContentItems();
    }
    protected override void LoadCurrentConfigValue()
    {
        managedDropdown.value = DataLoader.Instance.CurrentConfiguration.packageOption;
    }
    // called from UI
    public override void SetNewOption()
    {
        DataLoader.Instance.SetPackageOption(managedDropdown.value);
    }
}
