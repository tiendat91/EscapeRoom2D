using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoldierGate : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverWin;
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            var countkey = collision.gameObject.GetComponent<SoldierController>();
            if (countkey.countKeyItem == 3)
            {
                gameOverWin.SetActive(true);
            }
        }
    }


}
