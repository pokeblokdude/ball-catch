using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem current;

    void Awake() {
        current = this;
    }

    public delegate void Restart();
    public event Restart OnRestart;

    public void doRestart() {
        if(OnRestart != null) {
            OnRestart();
        }
    }
}
