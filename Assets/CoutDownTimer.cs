using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoutDownTimer : MonoBehaviour
{
    public float totalTimer;
    public float currentTimer;
    public Text text;
    private float minutes;
    private float second;
    private float stopcountdown = 0;
    private string test;

    [Header("Event Timer")]
    public bool useTimerEvent;
    public UnityEvent TimerEvent;
    public GameObject checkgameScreen;
    public Text tittLose;

    private void Awake()
    {
        currentTimer = totalTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if(totalTimer >0)
        {
            totalTimer -= Time.deltaTime;

            minutes = (int)(totalTimer / 60);
            second = (int)(totalTimer % 60);
            if(second < 10)
            {
                test = "0" + second.ToString();
            }
            else
            {
                test = second.ToString();
            }
            if (useTimerEvent)
            {
                if (minutes == 0 && second == 0)
                {

                    StartCoroutine(linkTimeout());
                    //stopcountdown = 1;
                    //TimerEvent.Invoke();
                    totalTimer = 0;
                }
            }
            text.text = minutes.ToString() + ":" + test;

        }


    }
    void Timeout()
    {
        Debug.Log("Time out");
        checkgameScreen.SetActive(true);
        tittLose.text = "YOU LOSE";
    }
    IEnumerator linkTimeout()
    {
        yield return new WaitForSeconds(1f);
        Timeout();
    }
    public void resetTimer()
    {
        totalTimer = currentTimer;
        checkgameScreen.SetActive(false);

    }
}
