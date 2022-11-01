using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject gameOverWin;
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
            var countkey = collision.gameObject.GetComponent<CharacterBehaviourScript>();
            if (countkey.countKeyItem == 3)
            {
                gameOverWin.SetActive(true);
            }
        }
    }


}
