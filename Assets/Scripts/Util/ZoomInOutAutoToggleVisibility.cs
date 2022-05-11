using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOutAutoToggleVisibility : MonoBehaviour
{
    [SerializeField] private GameObject toggledGameObject = null;
    [SerializeField] private float zoomLevelToggle = 3f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onZoomIn += ToggleObjectVisibility;
        PlayerController.onZoomOut += ToggleObjectVisibility;
    }
    private void OnDestroy() {
        PlayerController.onZoomIn -= ToggleObjectVisibility;
        PlayerController.onZoomOut -= ToggleObjectVisibility;
    }

    private void ToggleObjectVisibility(float zoomLevel) {
        if (zoomLevel < zoomLevelToggle) {
            toggledGameObject.SetActive(true);
        } else {
            toggledGameObject.SetActive(false);
        }
    }
}
