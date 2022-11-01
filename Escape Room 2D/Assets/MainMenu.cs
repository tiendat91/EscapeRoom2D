using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void AccessLinkFace()
    {
        Application.OpenURL("https://www.facebook.com/datdat910");
    }

    public void AccessLinkGithub()
    {
        Application.OpenURL("https://github.com/tiendat91/EscapeRoom2DGameProject");
    }
    public void PlayCityMap()
    {
        SceneManager.LoadScene("CityMap", LoadSceneMode.Single);

    }
    public void PlayDesertMap()
    {
        SceneManager.LoadScene("DesertMap", LoadSceneMode.Single);

    }
    public void PlayJungleMap()
    {
        SceneManager.LoadScene("JungleMap", LoadSceneMode.Single);

    }
}
