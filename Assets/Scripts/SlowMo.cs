using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMo : MonoBehaviour
{
    public Slider slowMoMeter;
    public float slowMoValue;
    private float maxSlowMo = 5;
    private GetPlayerInput playerInput;
    private Coroutine endSlowMo;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<GetPlayerInput>();
        slowMoValue = maxSlowMo;
        slowMoMeter.value = slowMoValue;
    }

    private void Update()
    {
        if (playerInput != null && playerInput.isAutoMoving && slowMoValue > 0)
        {
            slowMoValue -= Time.deltaTime;
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
