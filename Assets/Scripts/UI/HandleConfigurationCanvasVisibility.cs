using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleConfigurationCanvasVisibility : MonoBehaviour
{
    private Canvas[] configCanvasList;

    // Start is called before the first frame update
    void Start()
    {
        configCanvasList = GetComponentsInChildren<Canvas>();        
    }
    
    // called from UI
    public void ToggleCanvasVisibility() {
        // get visibility by first item in list so that we avoid cases where some items are visible while others are not
        bool newVisibilityValue = !configCanvasList[0].enabled;

        // toggle canvas visibility (let's scripts run while disabling UI content)
        for(int i = 0; i < configCanvasList.Length; i += 1) {
            configCanvasList[i].enabled = newVisibilityValue; 
        }
    }
}
