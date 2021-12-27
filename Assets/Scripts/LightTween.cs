using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class LightTween : MonoBehaviour
{
    [SerializeField] float startIntensity = 0.4f;
    [SerializeField] float targetIntensity = 1f;
    [SerializeField] Light2D l;
    [SerializeField] float tweenDuration = 2f;
    [SerializeField] Ease easeType;

    void Awake()
    {
        l = GetComponent<Light2D>();
        l.intensity = startIntensity;
    }
    void Start()
    {
        DOTween.To(() => l.intensity, (x) => l.intensity = x, targetIntensity, tweenDuration);
    }
}
