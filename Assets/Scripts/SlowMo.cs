using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SlowMo : NetworkBehaviour
{
    private Slider slowMoMeter;
    public float slowMoValue;
    private float maxSlowMo = 1.5f;
    private GetPlayerInput playerInput;
    private Coroutine endSlowMo;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        playerInput = GetComponent<GetPlayerInput>();
        slowMoMeter = GameObject.FindWithTag("SlowMo").GetComponent<Slider>();
        slowMoValue = maxSlowMo;
        slowMoMeter.maxValue = maxSlowMo;
        slowMoMeter.value = slowMoValue;
    }

    private void Update()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        if (playerInput != null && playerInput.isAutoMoving && slowMoValue > 0)
        {
            slowMoValue -= Time.deltaTime * 2;
            slowMoMeter.value = slowMoValue;
        }
        else if (playerInput != null && playerInput.isAutoMoving == false && slowMoValue < maxSlowMo && endSlowMo == null)
        {
            endSlowMo = StartCoroutine(StartRegenSlowMo());
        }

        if(slowMoValue <= 0)
        {
            slowMoValue = 0;
            playerInput.isAutoMoving = false;
        }
    }

    public void IncreaseMaxSlowMo()
    {
        maxSlowMo += maxSlowMo * 0.1f;
        slowMoValue = maxSlowMo;
        slowMoMeter.value = slowMoValue;
        Time.timeScale = 1f;
    }

    IEnumerator StartRegenSlowMo()
    {
        yield return new WaitForSeconds(5f);
        while (slowMoValue < maxSlowMo && playerInput.isAutoMoving == false)
        {
            slowMoValue += Time.deltaTime;
            slowMoMeter.value = slowMoValue;
            yield return null;
        }

        endSlowMo = null;
    }

}
