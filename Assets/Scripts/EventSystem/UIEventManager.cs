using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    public static UIEventManager instance;

    void Awake() {
        instance = this;
    }
    
    public event Action onDamageTakenUI;
    public event Action<int> onMaxHealthUpdateUI;
    public event Action onBoostUI;
    public event Action<string> onBombUpdateUI;
    public event Action<string> onScoreUpdateUI;

    public void DamageTakenUI() {
        onDamageTakenUI?.Invoke();
    }
    public void MaxHealthUpdate(int mh) {
        onMaxHealthUpdateUI?.Invoke(mh);
    }

    public void BoostUI() {
        onBoostUI?.Invoke();
    }

    public void BombUpdateUI(string bombString) {
        onBombUpdateUI?.Invoke(bombString);
    }

    public void ScoreUpdateUI(string scoreString) {
        onScoreUpdateUI?.Invoke(scoreString);
    }
}
