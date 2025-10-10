using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class GameTimerr : MonoBehaviour
{
    private DateTime lastExitTime;
    private const int MaxTime = 5;
    public Text timeText;
    public Button startTimerButton; 
    private bool isTimerRunning = false;
    private float elapsedTime = 0f;
    private int ClickCount = 0;


    void Start()
    {
        string lastExitTimeString = PlayerPrefs.GetString("LastExitTime3", null);
        if (!string.IsNullOrEmpty(lastExitTimeString))
        {
            DateTime lastExit = DateTime.Parse(lastExitTimeString);
            TimeSpan timeSpent = DateTime.Now - lastExit;

            elapsedTime = Mathf.Min((float)timeSpent.TotalSeconds, MaxTime);
            Debug.Log($"Time passed since last exit: {elapsedTime} seconds.");
        }

        ClickCount = PlayerPrefs.GetInt("ClickCount", 0);
        elapsedTime += PlayerPrefs.GetFloat("ElapsedTime", 0f);
        UpdateTimeText();

        startTimerButton.onClick.AddListener(StartTimer);

        if (elapsedTime > 0)
        {
            isTimerRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    void Update()
    {
        if (isTimerRunning && elapsedTime < MaxTime)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) 
        {
            lastExitTime = DateTime.Now;
            PlayerPrefs.SetString("LastExitTime3", lastExitTime.ToString());
            PlayerPrefs.SetFloat("ElapsedTime", elapsedTime); 
            PlayerPrefs.SetInt("ClickCount", ClickCount);
            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
     
        lastExitTime = DateTime.Now;
        PlayerPrefs.SetString("LastExitTime3", lastExitTime.ToString());
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime); 
        PlayerPrefs.SetInt("ClickCount", ClickCount);
        PlayerPrefs.Save();
    }

    void StartTimer()
    {
        ClickCount++; 
        Debug.Log(ClickCount);
        if (ClickCount >= 2)
        {
            startTimerButton.onClick.RemoveListener(StartTimer);
            Debug.Log("���������� ������ �� ����� ����������");
            return; 
        }

        if (!isTimerRunning)
        {
            isTimerRunning = true;
            elapsedTime = 0f; 
            StartCoroutine(TimerCoroutine());
        }
        else
        {
            StopCoroutine(TimerCoroutine()); 
            isTimerRunning = false;
            elapsedTime = 0f; 
            StartCoroutine(TimerCoroutine()); 
        }
    }

    public IEnumerator TimerCoroutine()
    {
        while (elapsedTime < MaxTime)
        {
            yield return new WaitForSeconds(1);
            UpdateTimeText();
            startTimerButton.interactable = elapsedTime > MaxTime;
        }

        isTimerRunning = false;
        timeText.text = "00:00:00";
        Debug.Log("Timer finished!");
    }

    private void UpdateTimeText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
}