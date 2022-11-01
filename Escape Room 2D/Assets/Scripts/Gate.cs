using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject;

            var countkey = 0;
            if (player.name == "Soldier")
            {
                countkey =  player.GetComponent<SoldierController>().countKeyItem;
            } else
            {
                countkey = player.GetComponent<CharacterBehaviourScript>().countKeyItem;
            }
            if (countkey == 3)
            {
                Debug.Log("Qua man");
                if (SceneManager.GetActiveScene().name == "JungleMap")
                {
                    SceneManager.LoadScene("DesertMap", LoadSceneMode.Single);
                }
                else if (SceneManager.GetActiveScene().name == "DesertMap")
                {
                    SceneManager.LoadScene("CityMap", LoadSceneMode.Single);
                }
                else if (SceneManager.GetActiveScene().name == "CityMap")
                {
                    SceneManager.LoadScene("JungleMap", LoadSceneMode.Single);

                    Debug.Log("EndGame");
                }

            }
        }
    }


}
