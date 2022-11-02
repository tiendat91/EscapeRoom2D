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
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textPress.enabled = false;
        key.SetActive(false);
    }

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
    }

}
