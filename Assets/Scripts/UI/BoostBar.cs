using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void setMaxValue(float value) {
        slider.maxValue = value;
        slider.value = value;
    }

    public void setValue(float boost) {
        slider.value = boost;
    }

    public float getValue() {
        return slider.value;
    }
}
