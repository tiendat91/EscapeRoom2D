using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    RawImage img;
    [SerializeField]
    float _x, _y;
    float TimeLeft = 5;
    float turn = 1f;

    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
        }
        else
        {
            turn *= -1;
            TimeLeft = 5;
        }
        img.uvRect = new Rect(img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime * turn, img.uvRect.size);
    }
}
