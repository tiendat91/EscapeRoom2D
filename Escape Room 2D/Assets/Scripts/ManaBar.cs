using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    CharacterBehaviourScript hero;

    float TimeLeft;
    public bool TimerOn = false;

    void Start()
    {
        TimerOn = false;
    }
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                SetManaValue(TimeLeft);
            }
            else
            {
                TimerOn = false;
            }

        }
    }


    public void SetManaValue(float time)
    {
        slider.value = time;
    }

    public void SetTimeMana(float seconds)
    {
        TimeLeft = seconds;
        slider.maxValue = seconds;
        slider.value = seconds;
        slider.wholeNumbers = true;
    }

    public void TurnTimerOn()
    {
        TimerOn = true;
    }
}
