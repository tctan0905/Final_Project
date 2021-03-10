﻿using System.Collections;
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
    private float stopcoutdown = 0;

    [Header("Event Timer")]
    public bool useTimerEvent;
    public UnityEvent TimerEvent; 

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;

        minutes = (int)(totalTime / 60);
        second = (int)(totalTime % 60);

        if (useTimerEvent)
        {
            if (minutes <= 0 && second <= 0 && stopcoutdown <=0)
            {
                
                StartCoroutine(linkTimeout());
                stopcoutdown = 1;
                //TimerEvent.Invoke();
                
            }
        }
        text.text = minutes.ToString() + ":" + second.ToString();

      
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
