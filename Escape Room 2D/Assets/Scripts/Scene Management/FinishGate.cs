using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGate : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HeroController>())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
