using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu, resumeButton;
    private GetPlayerInput playerInput;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<GetPlayerInput>();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.isPausing)
        {
            PauseResume();
            playerInput.isPausing = false;
        }
    }

    public void PauseResume()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void GoHome()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
