using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btn_play : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn;
    void Start()
    {	
	    
    }
	public void OnBtnClick()
    {
        SceneManager.LoadScene(sceneName: "Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
