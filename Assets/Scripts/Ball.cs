using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{ 
    public GameObject explosionPF;
    Rigidbody2D rb;
    private Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 6f);
    }

    void Update() {
    }

    public void shoot(Vector2 velocity) {
        rb.velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EmitterWall") {
            return;
        }
        if(other.gameObject.tag == "BorderWall") {
            GameEventManager.instance.DamageTaken();
        }
        if(other.gameObject.GetComponent<TopDownCharacter>()) {
            GameEventManager.instance.Score();
        }
        Destroy(gameObject);
    }

    void OnDisable()
    {
        GameObject ball = Instantiate(explosionPF, transform.position, Quaternion.identity);
    }
}
