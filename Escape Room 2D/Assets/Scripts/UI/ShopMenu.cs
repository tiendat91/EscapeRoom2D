using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour
{
    public bool isPaused;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
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
