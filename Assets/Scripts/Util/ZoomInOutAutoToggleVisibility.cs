using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOutAutoToggleVisibility : MonoBehaviour
{
    [SerializeField] private GameObject[] toggledGameObjects = null;
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
        // if hovering UI -> don't zoom
        if (PlayerController.Instance.isMouseOverUI) { return; }
        
        // not hovering UI -> zoom away
        if (zoomLevel < zoomLevelToggle) {
            ToggleObjects(true);
        } else {
            ToggleObjects(false);
        }
    }

    private void ToggleObjects(bool newActiveValue) {
        for (int i = 0; i < toggledGameObjects.Length; i += 1) {
            toggledGameObjects[i].SetActive(newActiveValue);
        }
    }
}
