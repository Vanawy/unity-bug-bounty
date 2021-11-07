using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private Text _timer;
    
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        _timer.text = string.Format("{0:D2}:{1:D2}:{2:D3}", (int) timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }
}
