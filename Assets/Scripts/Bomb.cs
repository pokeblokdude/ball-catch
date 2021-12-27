using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameManager gm;
    
    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<TopDownCharacter>()) {
            gm.addBomb();
            Destroy(gameObject);
        }
    }
}
