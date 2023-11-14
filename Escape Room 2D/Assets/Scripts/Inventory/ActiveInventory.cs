using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexnum = 0;
    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue-1);
    }

    /// <summary>
    /// Set active to false for all hightlight image and set true for select index number
    /// </summary>
    /// <param name="indexNum"></param>
    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexnum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
    }
}
