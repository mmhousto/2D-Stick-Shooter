using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private PauseHandler pauseHandler;
    private GetPlayerInput playerInput;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseHandler = GameObject.FindWithTag("PauseHandler").GetComponent<PauseHandler>();
        playerInput = GetComponent<GetPlayerInput>();
        isPaused = false;
        pauseHandler.resumeButton.GetComponent<Button>().onClick.AddListener(PauseResume);
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
            pauseHandler.pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(pauseHandler.resumeButton);
        }
        else
        {
            Time.timeScale = 1;
            pauseHandler.pauseMenu.SetActive(false);
        }
    }

    public void GoHome()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
