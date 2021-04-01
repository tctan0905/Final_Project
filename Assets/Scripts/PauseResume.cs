using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void PauseGame()
    {
        isPause = true;
        pauseScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        isPause = false;
        pauseScreen.SetActive(false);
    }
}
