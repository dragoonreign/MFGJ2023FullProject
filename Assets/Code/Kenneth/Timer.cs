using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaylightTimerScript : MonoBehaviour
{
   

    [SerializeField]
    private float _timeRemaining = 10;

    private bool _timerIsRunning;


    [SerializeField]
    public TMP_Text timeText;

    [SerializeField]
    private GameObject _directionalLight;

    
    public UIManagerScript _uiManager;

    void Start()
    {
        _timerIsRunning = true;

        //_player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        RunningTimer();
        //LightingCountdown();
    }

    //References the Display of Minutes and Seconds
    private void DisplayTime(float timeToDisplay)
    {

        timeToDisplay += 1;

        float _minutes = Mathf.FloorToInt(timeToDisplay / 60);

        float _seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    //Makes the Timer decrease every seconds
    private void RunningTimer()
    {
        

        if (_timerIsRunning == true)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;

                DisplayTime(_timeRemaining);
            }
            else
            {
                PlayerHealth player = GetComponent<PlayerHealth>();



                Debug.Log("Out of Time Sucka!");

                _timeRemaining = 0;

                _timerIsRunning = false;

                if (_timeRemaining == 0)
                {
                    _uiManager.GameOverSequence();
                }
            }
        }
    }

    ////Reduces the Temperature depending on the Timer
    //private void LightingCountdown()
    //{
    //    _directionalLight.GetComponent<Light>().colorTemperature = _temperature;

    //    if (_timerIsRunning == true)
    //    {
    //        _temperature -= 2000f / _timeRemaining * Time.deltaTime;
    //    }
    //    else
    //    {
    //        Debug.Log("It is dark");
    //    }
    //}
}
