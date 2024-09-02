using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public Slider xpBar;
    public TextMeshProUGUI levelLabel;
    public static int level;
    public static int xp;
    private static int minXP;
    private static int maxXP;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        levelLabel.text = level.ToString();
        xp = 0;
        minXP = 0;
        maxXP = (int)(100 * Mathf.Pow(1.9f, level - 1));
        xpBar.minValue = minXP;
        xpBar.maxValue = maxXP;
        xpBar.value = xp;
    }

    // Update is called once per frame
    void Update()
    {
        SetLevelAndXP();
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
            level++;
            xp = minXP;
            maxXP = (int)(100 * Mathf.Pow(1.9f, level - 1));
        }
    }
}
