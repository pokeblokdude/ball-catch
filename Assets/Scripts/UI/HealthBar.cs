using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start() {
        UIEventManager.instance.onDamageTakenUI += takeDamage;
        UIEventManager.instance.onMaxHealthUpdateUI += setMaxValue;
    }

    void takeDamage() {
        Debug.Log("boop");
        slider.value -= 1;
    }

    public void setMaxValue(int value) {
        Debug.Log("set max value");
        slider.maxValue = value;
        slider.value = value;
    }

    public void setValue(int health) {
        slider.value = health;
    }

    public int getValue() {
        return (int)slider.value;
    }

    void OnDisable() {
        UIEventManager.instance.onDamageTakenUI -= takeDamage;
        UIEventManager.instance.onMaxHealthUpdateUI -= setMaxValue;
    }
}
