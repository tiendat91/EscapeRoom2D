using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    [SerializeField]
    GameOver gameOver;
    [SerializeField]
    public TextMeshProUGUI timer;
    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTime(TimeLeft);
            }
            else
            {
                TimerOn = false;
                gameOver.PauseGame();
            }
            if (TimeLeft <= 20)
            {
                timer.color = new Color(1, 0, 0, 255);
            }
        }
    }

    void updateTime(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
