using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombListener : MonoBehaviour
{
    TextBox t;

    void Awake() {
        t = GetComponent<TextBox>();
    }

    void Start() {
        UIEventManager.instance.onBombUpdateUI += updateBomb;
    }

    void updateBomb(string s) {
        Debug.Log("bomb");
        t.setText(s);
    }

    void Disable() {
        UIEventManager.instance.onBombUpdateUI -= updateBomb;
    }
}
