using System;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float elapsedTime { get; private set; }
    private bool isTimeRunning;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (isTimeRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void UpdateTimerText(float timeToDisplay)
    {
        float minutes = Mathf.Min(elapsedTime, timeToDisplay / 60);
        float seconds = Mathf.Max(elapsedTime, timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isTimeRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isTimeRunning = false;
    }

    public void ResetTimer()
    {
        isTimeRunning=false;
        elapsedTime = 0f;
    }
}