using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageGate : MonoBehaviour
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
            var countkey = collision.gameObject.GetComponent<MageController>();
            if (countkey.countKeyItem == 3)
            {
                gameOverWin.SetActive(true);
            }
        }
    }
}
