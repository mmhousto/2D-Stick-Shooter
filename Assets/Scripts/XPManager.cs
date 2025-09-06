using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : NetworkBehaviour
{
    public Slider xpBar;
    public TextMeshProUGUI levelLabel;
    public static Health health;
    public static int level;
    public static int xp;
    private static int minXP;
    private static int maxXP;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        level = 1;
        levelLabel.text = level.ToString();
        xp = 0;
        minXP = 0;
        maxXP = (int)(100 * Mathf.Pow(1.9f, level - 1));
        xpBar.minValue = minXP;
        xpBar.maxValue = maxXP;
        xpBar.value = xp;
        StartCoroutine(GetHealthBar());
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        SetLevelAndXP();
    }

    IEnumerator GetHealthBar()
    {
        while(GameObject.FindWithTag("Player") == null)
        {
            yield return null;
        }

        while(health == null)
        {
            if(MainManager.players == MainManager.Players.Solo)
                health = GameObject.FindWithTag("Player").GetComponent<Health>();
            else
            {
                foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if(player.GetComponent<NetworkObject>().IsOwner)
                    {
                        health = player.GetComponent<Health>();
                        break;
                    }
                }
            }
        }

        
    }

    private void SetLevelAndXP()
    {
        if (levelLabel.text != level.ToString())
            levelLabel.text = level.ToString();

        if (xpBar.maxValue != maxXP)
            xpBar.maxValue = maxXP;

        if (xpBar.value != xp)
            xpBar.value = xp;
    }

    public static void IncreaseXP(int xpToIncrease)
    {
        xp += xpToIncrease;
        if (xp > maxXP)
        {
            if(health != null && health.IsOwner)
                health.GetFullHP();
            level++;
            UpgradeManager.ShowUpgradeMenu();
            xp = minXP;
            maxXP = (int)(100 * Mathf.Pow(1.9f, level - 1));
        }
    }
}
