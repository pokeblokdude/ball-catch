using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    void Awake() {
        instance = this;
    }
    
    public event Action onDamageTaken;
    public event Action onScore;
    public event Action<Vector2> onUseBomb;
    public event Action<BallSpawner.AttackState> onModeChange;

    public void DamageTaken() {
        onDamageTaken?.Invoke();
    }

    public void Score() {
        onScore?.Invoke();
    }

    public void UseBomb(Vector2 location) {
        onUseBomb?.Invoke(location);
    }

    public void ModeChange(BallSpawner.AttackState mode) {
        onModeChange?.Invoke(mode);
    }

}

