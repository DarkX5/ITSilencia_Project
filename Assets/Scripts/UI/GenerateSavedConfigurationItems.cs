
public class GenerateSavedConfigurationItems : GenerateContentItems
{
    string[] savedConfigs = null;

    protected override void LoadDataTextsForContentItems()
    {
        savedConfigs = DataLoader.Instance.SavedConfigurationLists.ToArray();
        // only reload list when changed (first run, saving new items)
        if (managedDropdown.options.Count != savedConfigs.Length) {
            // get texts for options
            GenerateDropdownContent(savedConfigs);
            LoadCurrentConfigValue();
        }
    }
    protected override void LoadDataTextsForContentItemsAfterConfig(int configurationIdx)
    {
        LoadDataTextsForContentItems();
    }
    protected override void LoadCurrentConfigValue()
    {
        // managedDropdown.value = DataLoader.Instance.CurrentConfiguration.idx;
    }
    // called from UI
    public override void SetNewOption()
    {
        DataLoader.Instance.SetConfigIdx(managedDropdown.value);
    }
}
