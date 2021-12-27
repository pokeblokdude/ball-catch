using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListener : MonoBehaviour
{
    TextBox t;

    void Awake() {
        t = GetComponent<TextBox>();
    }

    void Start() {
        UIEventManager.instance.onScoreUpdateUI += updateScore;
    }

    void updateScore(string s) {
        t.setText(s);
    }

    void Disable() {
        UIEventManager.instance.onScoreUpdateUI -= updateScore;
    }
}
