using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button btn_P;
    public Button btn_E;


    public void OnButtonPlayClick()
    {
        SceneManager.LoadScene(sceneName: "Game");
    }

    public void OnButtonExitClick()
    {
        Application.Quit();   
    }
}
