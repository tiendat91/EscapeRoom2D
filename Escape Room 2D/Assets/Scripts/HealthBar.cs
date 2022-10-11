using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetHealth(float health)
    {
        slider.value = health;
        slider.wholeNumbers = true;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        slider.wholeNumbers = true;
    }
}
