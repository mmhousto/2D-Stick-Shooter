using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeButtonManager : MonoBehaviour
{
    public GameObject[] upgradeButtons;

    private void Update()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject;
        if (currentObject != upgradeButtons[0] && currentObject != upgradeButtons[1] && currentObject != upgradeButtons[2] && currentObject != upgradeButtons[3])
            EventSystem.current.SetSelectedGameObject(upgradeButtons[0]);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(upgradeButtons[0]);
    }
}
