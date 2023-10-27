using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject[] menus;

    void Start()
    {
        LoadingMainMenu();
    }

    public void LoadingMainMenu()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
        menus[0].SetActive(true);
    }

    public void QuitGame()
    {
        //Application.Quit();
    }

    public void AccessLinkFace()
    {
        //Application.OpenURL("https://www.facebook.com/datdat910");
    }

    public void AccessLinkGithub()
    {
        //Application.OpenURL("https://github.com/tiendat91/EscapeRoom2DGameProject");
    }
    public void PlayCityMap()
    {
        SceneManager.LoadScene("CityMap");

    }
    public void PlayDesertMap()
    {
        SceneManager.LoadScene("DesertMap");

    }
    public void PlayJungleMap()
    {
        SceneManager.LoadScene("JungleMap");

    }
}
