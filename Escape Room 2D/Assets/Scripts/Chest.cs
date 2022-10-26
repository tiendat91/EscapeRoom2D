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
    Canvas textPress;
    [SerializeField]
    Sprite newSprite;
    [SerializeField]
    UnityEvent _onTriggerEvent;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textPress.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.R))
        {
            textPress.enabled = true;
            ChestOpen();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textPress.enabled = false;
    }

    void ChestOpen()
    {
        Debug.Log("Open Chest");
        textPress.enabled = false;
        spriteRenderer.sprite = newSprite;
        Debug.Log("Doi Sprite");

    }
}
