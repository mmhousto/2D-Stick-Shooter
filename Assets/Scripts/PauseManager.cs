using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private GetPlayerInput playerInput;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<GetPlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.isPausing)
        {
            isPaused = !isPaused;
            PauseResume();
            playerInput.isPausing = false;
        }
    }

    private void PauseResume()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
