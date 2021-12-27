using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCharacter : MonoBehaviour
{
    public InputMaster input;
    //public GameManager gm;

    [Header("Components")]
    public SpriteRenderer sr;
    //public Animator animator;
    public Rigidbody2D rb;
    public BoostBar bb;

    [Header("Movement Settings")]
    public float speed = 1f;
    public float ySpeedDiv = 2f;
    public float turnSpeed = 5f;

    [Header("Boost Settings")]
    public float boostSpeed = 2f;
    public float boostDuration = 1f;
    public float boostCooldown = 2f;

    private Vector2 moveDir;
    
    // ROLL STUFF
    private float doBoost;
    private float boostTimer;
    private bool isBoosting;
    private float dCD;
    private bool canBoost;
    private bool couldBoost;
    

    // POSITION STUFF
    private Vector2 pos;
    private Vector2 lastPos;
    private int lookDir;

    public enum Abilities {
        BOMB,
        NONE
    };

    [Header("Ability Settings")]
    public Abilities selectedAbility = Abilities.BOMB;
    public GameObject abilityPF;

    void Awake()
    {
        // =========== INPUT ===============
        input = new InputMaster();
        input.Enable();
        // -- MOVEMENT
        input.Player.Movement.performed += context => {
            moveDir = context.ReadValue<Vector2>();
            Debug.Log(moveDir);
        };
        input.Player.Movement.canceled += context => {
            moveDir = Vector2.zero;
        };
        // -- BOOST
        input.Player.Boost.performed += context => {
            doBoost = context.ReadValue<float>();
            canBoost = false;
        };
        input.Player.Boost.canceled += context => {
            doBoost = 0;
            canBoost = true;
        };
        // -- ABILITY
        input.Player.Ability.performed += context => {
            useAbility();
        };

        // ================ COMPONENT INSTANTIATION ===================
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bb = FindObjectOfType<BoostBar>();

        // ============== EVENT SUBSCRIPTIONS ===================
        
    }

    // Start is called before the first frame update
    void Start()
    {
        bb.setMaxValue(boostDuration);
        lastPos = transform.position;
        dCD = boostCooldown;
        canBoost = true;
        couldBoost = true;
    }

    void FixedUpdate()
    {
        if(rb.velocity != Vector2.zero) {
            rb.transform.LookAt(transform.position + new Vector3(0, 0, 1), moveDir);
        }
    }
    // Update is called once per frame
    void Update()
    {
        pos = new Vector2(transform.position.x, transform.position.y);

        // Actual movement
        move(moveDir);

        int curDir = xLookDir();
        
        lastPos = pos;
        lookDir = curDir;
        couldBoost = canBoost;

        // Boost Bar Stuff
        if(isBoosting) {
            bb.setValue(boostDuration - boostTimer);
        }
        else {
            bb.setValue(dCD);
        }
    }

    void move(Vector2 dir) {
        rb.velocity = dir * speed;
        boostHandler();
    }

    void boostHandler() {
        // Conditions to stop the boost
        if(boostTimer >= boostDuration) {
            boostTimer = 0;
            isBoosting = false;
            dCD = 0;
            bb.setMaxValue(boostCooldown);
            return;
        }
        // Conditions to continue the boost
        else if(isBoosting) {
            boost();
            boostTimer += Time.deltaTime;
        }
        // Conditions to initiate the boost
        else if(doBoost == 1 && dCD >= boostCooldown && rb.velocity != Vector2.zero && couldBoost) {
            boost();
            isBoosting = true;
            bb.setMaxValue(boostDuration);
        }
        else {
            // Add the frametime to the boost cooldown
            dCD += Time.deltaTime;
        }
    }
    void boost() {
        rb.velocity *= boostSpeed;
    }

    void useAbility() {
        GameEventManager.instance.UseBomb(transform.position);
    }

    private int xLookDir() {
        return (int)Mathf.Sign(pos.x - lastPos.x);
    }

    void OnRestart() {
        Debug.Log("Player Restart Response");
    }

    void OnDisable()
    {
        input.Disable();
        //CustomEventSystem.current.OnRestart -= OnRestart;
    }
}
