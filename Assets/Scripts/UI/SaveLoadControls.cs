using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveLoadControls : GenerateContentItems
{
    [Header("SAVE / LOAD configuration")]
    [SerializeField] private InputField nameInputField = null;
    [SerializeField] private InputField descriptionInputField = null;
    [SerializeField] private Color normalTextBgColor = Color.white;
    [SerializeField] private Color errorTextBgColor = Color.red;
    private Image nameInputImage = null;
    private Image descriptionInputImage = null;

    private List<string> savedConfigs = null;
    private void Start()
    {
        nameInputField.text = DataLoader.Instance.CurrentConfiguration.configurationName;
        descriptionInputField.text = DataLoader.Instance.CurrentConfiguration.configurationDescription;
        nameInputImage = nameInputField.GetComponent<Image>();
        descriptionInputImage = nameInputField.GetComponent<Image>();
        nameInputImage.color = normalTextBgColor;
        descriptionInputImage.color = normalTextBgColor;
    }

    public void SaveConfig()
    {
        // check name text box has value
        if (nameInputField.text.Trim() == string.Empty)
        {
            nameInputImage.color = errorTextBgColor;
            return;
        }

        DataLoader.Instance.SaveConfigByName(nameInputField.text);
    }
    // called from UI
    public void LoadConfig(Dropdown configDropdown)
    {
        DataLoader.Instance.LoadConfig(configDropdown.value);
    }
    public void ClearAllConfigs() {
        /*v TODO - change to delete saved files v*/
        PlayerPrefs.DeleteAll();
    }

    // called from UI
    public void UpdateCurrentGameConfigName()
    {
        if (nameInputField?.text.Trim() == string.Empty)
        {
            // text empty - show error
            nameInputImage.color = errorTextBgColor;
            return;
        }

        // modify current config name to new value
        DataLoader.Instance.SetNameOption(nameInputField.text);
        // hide error text, if any
        nameInputImage.color = normalTextBgColor;
    }
    // called from UI
    public void UpdateCurrentGameConfigDescription()
    {
        // modify current config description to new value
        DataLoader.Instance.SetDescriptionOption(descriptionInputField.text);
        // hide error text, if any
        descriptionInputImage.color = normalTextBgColor;
    }
}
