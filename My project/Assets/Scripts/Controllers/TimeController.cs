using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float currentLevelSeconds;
    public int currentLevelTime = 0;
    
    private void FixedUpdate()
    {
        if(UiController.Instance.SomeViewIsActive()) return;
        CalculateLevelTime();
    }
    
    private void CalculateLevelTime()
    {
        currentLevelSeconds += 0.02f;
        if (currentLevelSeconds >= 1)
        {
            currentLevelTime++;
            currentLevelSeconds = 0;
            timerText.text = ConvertSecondsToHours(currentLevelTime);
        }
    }
    
    private string ConvertSecondsToHours(int _seconds)
    {
        var hours   = Math.Floor((double)(_seconds / 3600)); // get hours
        var minutes = Math.Floor((_seconds - (hours * 3600)) / 60); // get minutes
        var seconds = _seconds - (hours * 3600) - (minutes * 60); //  get seconds
        return hours.ToString("00")+":"+minutes.ToString("00")+":"+seconds.ToString("00");
    }
}
