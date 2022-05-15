using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateContentItems : MonoBehaviour
{
    [SerializeField] protected Dropdown managedDropdown = null;

    protected virtual void LoadDataTextsForContentItems() { }

    protected void GenerateDropdownContent(OptionText[] dropdownContentTexts)
    {
        managedDropdown.ClearOptions();
        
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        for (int i = 0; i < dropdownContentTexts.Length; i += 1)
        {
            dropdownOptions.Add(new Dropdown.OptionData(dropdownContentTexts[i].text));
        }

        managedDropdown.AddOptions(dropdownOptions);
    }
}
