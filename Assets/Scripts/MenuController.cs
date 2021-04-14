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
    public GameObject settingScreen;
    public GameObject[] selectionScreen;
    public GameObject playScreen;
    public GameObject selectionScreen2;
    bool isPause;
    

    void Start()
    {
        selectionScreen = new GameObject[3];
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
        //SceneManager.LoadScene(sceneName: "GameScene");

        playScreen.SetActive(false);
        //for(int i =0; i< selectionScreen.Length;i++)
        //{
        //    selectionScreen[i].SetActive(true);
        //}
        selectionScreen2.SetActive(true);
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
    public void OnButtonSettingClick()
    {
        settingScreen.SetActive(true);
        pauseScreen.SetActive(false);
        
    }
    

}
