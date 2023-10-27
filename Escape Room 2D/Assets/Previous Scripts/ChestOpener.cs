using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestOpen, ChestClose, Key;
    void Start()
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
        Key.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChestOpen.SetActive(true);
        ChestClose.SetActive(false);
        Key.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestOpen.SetActive(true);
        ChestClose.SetActive(false);
        Key.SetActive(true);
    }
}
