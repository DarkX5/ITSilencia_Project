using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] private UnitInfoData infoData  = null;
    [SerializeField] private GameObject infoContainer = null;

    TMP_Text info = null;

    private void Start()
    {
        info = infoContainer.gameObject.GetComponent<TMP_Text>();
        info.text = info.text.Replace("{name}", infoData.unitName).Replace("{description}", infoData.unitDescription);
    }

    public void ToggleShowInfo() {
        infoContainer.SetActive(!infoContainer.activeSelf);
    }
}
