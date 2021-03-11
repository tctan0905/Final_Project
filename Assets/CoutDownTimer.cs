using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoutDownTimer : MonoBehaviour
{
    public float totalTime;
    public Text text;
    private float minutes;
    private float second;
    private float stopcountdown = 0;
    private string test;

    [Header("Event Timer")]
    public bool useTimerEvent;
    public UnityEvent TimerEvent; 

    // Update is called once per frame
    void Update()
    {
        if(totalTime >0)
        {
            totalTime -= Time.deltaTime;

            minutes = (int)(totalTime / 60);
            second = (int)(totalTime % 60);
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
                    totalTime = 0;
                }
            }
            text.text = minutes.ToString() + ":" + test;

        }


    }
    void Timeout()
    {
        Debug.Log("Time out");
    }
    IEnumerator linkTimeout()
    {
        yield return new WaitForSeconds(1f);
        Timeout();
    }
}
