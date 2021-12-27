using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPF;
    TopDownCharacter player;
    Rigidbody2D rb;
    GameManager gm;
    public float attackCooldown = 2f;
    public float attackVelocity = 5f;

    private float attackTimer;

    public enum AttackState {
        NO_ATTACK,
        TUTORIAL,
        CLOSE_TO_PLAYER,
        SPIRAL,
        FOUR_WAY
    };
    public AttackState attackState;

    bool spiralFirstShot;
    Vector2 spiralDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<TopDownCharacter>();
        attackTimer = 0;
        spiralFirstShot = true;
        spiralDir = Vector2.zero;

        enabled = false;
        int x = 1;
        DOTween.To(() => x, (y) => x = y, 0, 4f).OnComplete(enable);
    }

    // Update is called once per frame
    void Update()
    {
        attackHandler();
    }

    void enable() {
        enabled = true;
    }

    void attackHandler() {
        Vector2 playerVec = new Vector2(player.rb.position.x - rb.position.x, player.rb.position.y - rb.position.y);
        if(attackTimer >= attackCooldown) {
            switch(attackState) {
                case AttackState.NO_ATTACK:
                
                    break;
                case AttackState.TUTORIAL:
                    attackTutorial();
                    DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, 0.01f, 0.3f).SetEase(Ease.Linear);
                    attackState = AttackState.FOUR_WAY;
                    break;
                case AttackState.CLOSE_TO_PLAYER:
                    attackCloseToPlayer(playerVec);
                    break;
                case AttackState.SPIRAL:
                    attackSpiral(playerVec);
                    break;
                case AttackState.FOUR_WAY:
                    attackFourWay();
                    break;
            }
            attackTimer = 0;
        }
        else {
            attackTimer += Time.deltaTime;
        }
    }

    void attack(Vector2 direction) {
        GameObject ball = Instantiate(ballPF, transform.position, Quaternion.identity);
        Ball newBall = ball.GetComponent<Ball>();
        newBall.shoot(direction * attackVelocity);
    }

    void attackTutorial() {
        attackCooldown = 3f;
        attack(Vector2.left);
    }

    void attackCloseToPlayer(Vector2 playerVec) {
        attackCooldown = 0.6f;

        Vector2 attackDirX = new Vector2(-2f, 2f);
        Vector2 attackDirY = new Vector2(-2f, 2f);
        if(Mathf.Abs(playerVec.x) > Mathf.Abs(playerVec.y)){
            attackDirX = Vector2.zero;
        }
        else if(Mathf.Abs(playerVec.x) < Mathf.Abs(playerVec.y)) {
            attackDirY = Vector2.zero;
        }
        attack(
            new Vector2(
                playerVec.x + Random.Range(attackDirX.x, attackDirX.y),
                playerVec.y + Random.Range(attackDirY.x, attackDirY.y)
            ).normalized
        );
    }

    void attackSpiral(Vector2 playerVec) {
        attackCooldown = 0.2f;
        
        if(spiralFirstShot) {
            spiralDir = playerVec;
            spiralFirstShot = false;
        }
        else {
            Vector3 dir = Quaternion.Euler(0, 0, 5) * spiralDir;
            spiralDir = new Vector2(dir.x, dir.y).normalized;
        }
        attack(spiralDir);
    }

    void attackFourWay() {
        attackCooldown = 1.8f;

        int val = Mathf.FloorToInt(Random.Range(1f, 4.9f));
        Vector2 attackDir = Vector2.zero;
        switch(val) {
            case 1:
                attackDir = Vector2.up;
                break;
            case 2:
                attackDir = Vector2.down;
                break;
            case 3:
                attackDir = Vector2.left;
                break;
            case 4:
                attackDir = Vector2.right;
                break;
        }
        attack(attackDir);
    }
}
