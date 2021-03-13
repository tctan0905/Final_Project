using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnButtonPlayClick()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }

    public void OnButtonExitClick()
    {
        Application.Quit();
    }
}
