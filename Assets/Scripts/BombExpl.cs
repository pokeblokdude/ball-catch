using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExpl : MonoBehaviour
{

    CircleCollider2D col;
    float lerpAmt = 0;

    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        col.radius = 1f;
        Destroy(gameObject, 0.26f);
    }
    
    void FixedUpdate()
    {
        lerpAmt += Time.fixedDeltaTime/15;
        col.radius = Mathf.Lerp(1, 2000, lerpAmt);
    }
}
