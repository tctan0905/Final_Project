using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    PauseResume pause_resume_game;
    WaveSpawn bt_replaygame;
    public GameObject pauseScreen;
    bool isPause;

    void Start()
    {
        isPause = false;
    }

    void Update()
    {

        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void OnButtonPlayClick()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }

    public void OnButtonExitClick()
    {
        Application.Quit();
    }

    public void OnButtonPauseClick()
    {
        isPause = true;
        pauseScreen.SetActive(true);
    }
    public void OnButtonGameExitCLick()
    {
        SceneManager.LoadScene(sceneName: "MenuScene");
    }

    public void OnButtonReplayClick()
    {
        //bt_replaygame.GetComponent<WaveSpawn>().Replay();
        isPause = false;
        pauseScreen.SetActive(false);
        Debug.Log("REPLAY");
    }

    public void OnButtonResumeClick()
    {
        isPause = false;
        pauseScreen.SetActive(false);
        Debug.Log("RESUME");
    }


}
