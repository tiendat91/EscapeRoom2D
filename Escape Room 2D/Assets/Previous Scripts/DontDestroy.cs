using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                {
                    Destroy(Object.FindObjectsOfType<DontDestroy>()[i]);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //Destroy the object when reach the final map
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            Destroy(gameObject);
        }
    }
}
