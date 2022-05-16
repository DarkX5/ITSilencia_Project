
public class GenerateUpholsteryContentItems : GenerateContentItems
{
    protected override void LoadDataTextsForContentItems()
    {
        // get texts for options
        GenerateDropdownContent(DataLoader.Instance.CarUpholsteryOptionsTexts);
        LoadCurrentConfigValue();
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx)
    {
        LoadDataTextsForContentItems();
    }
    protected override void LoadCurrentConfigValue()
    {
        managedDropdown.value = DataLoader.Instance.CurrentConfiguration.upholsteryOption;
    }
    // called from UI
    public override void SetNewOption()
    {
        DataLoader.Instance.SetUpholsteryOption(managedDropdown.value);
    }
}
