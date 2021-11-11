using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer levelTimer; // to store a single instance
    public Text timerText; // for UI

    private TimeSpan timePlaying; // from System, for formatting time info
    private float elapsedTime;
    private bool isTimerActive;

    private void Awake()
    {
        levelTimer = this;
    }

    private void Start()
    {
        timerText.text = "Time: 00:00.00";
        elapsedTime = 0f;
        isTimerActive = false;
        BeginTimer(); // can be moved to LevelManager.cs later
    }

    public void BeginTimer()
    {
        isTimerActive = true;
        StartCoroutine("UpdateTimer");
    }

    // Can later be used to end timer once target point reached
    private void EndTimer()
    {
        isTimerActive = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (isTimerActive)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timerText.text = "Time: " + timePlaying.ToString("mm':'ss'.'ff"); 

            yield return null;
        }
    }
}
