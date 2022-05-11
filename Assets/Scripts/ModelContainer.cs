using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelContainer : MonoBehaviour
{
    [SerializeField] private Text textModelNo;
    [SerializeField] private GameObject[] modelList;
    [SerializeField] private List<GameObject> createdObjectList;

    private int idx = 0;
    private GameObject newModel;

    private void Start()
    {
        if (modelList.Length < 1)
            return;

        foreach (var item in modelList)
        {
            newModel = Instantiate(item, transform);
            newModel.SetActive(false);
            createdObjectList.Add(newModel);
        }

        createdObjectList[idx].SetActive(true);
        SetModelNo();
    }

    public void PreviousModel()
    {
        if (modelList.Length < 1)
            return;

        HideObjectList();
        idx -= 1;
        if (idx < 0)
            idx = 0;
        createdObjectList[idx].SetActive(true);
        SetModelNo();
    }

    public void NextModel()
    {
        if (modelList.Length < 1)
            return;

        HideObjectList();
        idx += 1;
        if (idx >= createdObjectList.Count)
            idx = createdObjectList.Count - 1;
        createdObjectList[idx].SetActive(true);
        SetModelNo();
    }

    private void HideObjectList()
    {
        if (createdObjectList == null)
            return;
        foreach (var item in createdObjectList)
        {
            if (item.activeSelf)
                item.SetActive(false);
        }
    }

    private void SetModelNo()
    {
        if (textModelNo == null)
            return;

        textModelNo.text = $"Model No: [{ (idx + 1).ToString() }]";
    }
}
