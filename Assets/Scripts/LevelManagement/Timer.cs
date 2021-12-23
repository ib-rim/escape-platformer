using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    public Text timer;
    private static String timerText = "Timer"; // for UI

    private static TimeSpan timePlaying; // from System, for formatting time info
    private static float elapsedTime;
    private bool isTimerActive;

    private void Awake()
    {  
        if(timerText == "Timer") {
            timerText = "00:00.00";
            elapsedTime = 0f;
        }
        BeginTimer(); // can be moved to LevelManager.cs later
        timer.text = timerText;

    }

    public void BeginTimer()
    {   
        isTimerActive = true;
        StartCoroutine("UpdateTimer");
    }

    // Can later be used to end timer once target point reached
    public void EndTimer()
    {   
        isTimerActive = false;
        timerText = "Timer";
    }

    private IEnumerator UpdateTimer()
    {
        while (isTimerActive)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timerText = timePlaying.ToString("mm':'ss'.'ff"); 
            timer.text = timerText;
            yield return null;
        }
    }
}
