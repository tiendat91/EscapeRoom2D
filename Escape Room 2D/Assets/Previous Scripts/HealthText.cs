using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 1000f;
    public float floatSpeed = 500;
    public Vector3 floatDirection = new Vector3(0, 1, 0);

    public TextMeshProUGUI textMeshPro;
    RectTransform rectTransform;
    float timeElapsed = 0.0f;
    Color startColor;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startColor = textMeshPro.color;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, 1 - (timeElapsed / timeToLive));
        if (timeElapsed > timeToLive)
        {
            //Destroy(gameObject);
        }
    }
}
