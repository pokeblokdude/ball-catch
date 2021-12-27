using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextFader : MonoBehaviour
{
    Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Color color = text.color;
        color.a = 0f;
        DOTween.To(() => color.a, (x) => color.a = x, 1f, 0.4f);
    }

}
