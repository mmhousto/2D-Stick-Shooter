using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainManager : NetworkBehaviour
{

    public GameObject mainMenu, modeSelect, playersGroup, modesGroup, multiplayerLobby;

    public GameObject backButtonMode, backButtonOptions, playButton, optionsButton, lobbyBack;

    public GameObject createJoin, sessions, playerList, modesGroupCoop, storyButton, endlessButton, readyButton, leaveButton;

    public enum Players { Solo, Coop }
    public Players players;
    public enum Mode { Story, Endless }
    public Mode mode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void BackToMainFromModeSelect()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
        mainMenu.SetActive(true);
        modeSelect.SetActive(false);
        playersGroup.SetActive(true);
        modesGroup.SetActive(false);
        multiplayerLobby.SetActive(false);
        sessions.SetActive(false);
        createJoin.SetActive(true);
    }

    public void ToModeSelect()
    {
        EventSystem.current.SetSelectedGameObject(backButtonMode);
        mainMenu.SetActive(false);
        modeSelect.SetActive(true);
        playersGroup.SetActive(true);
        modesGroup.SetActive(false);
    }

    public void PlaySolo()
    {
        players = Players.Solo;
        playersGroup.SetActive(false);
        modesGroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButtonMode);
    }

    public void PlayCoop()
    {
        players = Players.Coop;
        modeSelect.SetActive(false);
        playersGroup.SetActive(false);
        multiplayerLobby.SetActive(true);
        //modesGroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(lobbyBack);
    }

    public void SessionJoined()
    {
        createJoin.SetActive(false);
        sessions.SetActive(false);
        playerList.SetActive(true);
        modesGroupCoop.SetActive(true);
        lobbyBack.SetActive(false);

        if (IsHost)
        {
            storyButton.SetActive(true);
            endlessButton.SetActive(true);
            readyButton.SetActive(false);
            EventSystem.current.SetSelectedGameObject(leaveButton);
        }
        else
        {
            storyButton.SetActive(false);
            endlessButton.SetActive(false);
            readyButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(readyButton);
        }
    }

    public void LeaveLobby()
    {
        playerList.SetActive(false);
        createJoin.SetActive(true);
        sessions.SetActive(false);
        storyButton.SetActive(false);
        endlessButton.SetActive(false);
        readyButton.SetActive(true);
    }

    public void PlayStory()
    {
        mode = Mode.Story;
        SceneManager.LoadScene(1);
    }

    public void PlayEndless()
    {
        mode = Mode.Endless;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
