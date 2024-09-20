using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static GameObject upgradeMenu;

    private void Start()
    {
        upgradeMenu = GameObject.FindWithTag("UpgradeManager");
        upgradeMenu.SetActive(false);
    }

    public static void ShowUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradeMenu.SetActive(true);
        
    }

}
