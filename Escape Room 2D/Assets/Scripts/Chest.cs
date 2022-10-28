using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    public Canvas textPress;
    [SerializeField]
    Sprite newSprite;
    [SerializeField]
    GameObject key;


    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textPress.enabled = false;
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChestOpen()
    {
        spriteRenderer.sprite = newSprite;
        if (key != null)
        {
            key.SetActive(true);
        }
        Debug.Log("Mo Ruong");
    }

}
