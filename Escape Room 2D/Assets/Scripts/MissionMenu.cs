using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMenu : MonoBehaviour
{
    public bool isPaused;

    void Start()
    {
    }

    void Update()
    {

    }
    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
