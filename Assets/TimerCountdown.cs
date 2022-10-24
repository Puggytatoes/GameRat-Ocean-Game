using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    private float timeValue = 120; 
	public TextMeshProUGUI timerText;

    private void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            SceneManager.LoadScene("Game Over");
        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddTime(float timeToAdd)
    {
        timeValue += timeToAdd;
    }

    public float GetTime()
    {
        return timeValue; 
    }
}
