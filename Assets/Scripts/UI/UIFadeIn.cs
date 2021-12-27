using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFadeIn
{
    public void doFade(float value) {
        DOTween.To(() => value, (x) => value = x, 1f, 1f);
    }
}
