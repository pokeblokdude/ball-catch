using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    InputMaster input;

    [Header("Game Objects")]
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] GameObject bombPF;
    [SerializeField] GameObject abilityPF;

    [Header("Mechanics")]
    [SerializeField] int maxHP;
    [SerializeField] int numBombs = 1;
    [SerializeField] int multiplierInterval = 3;

    int curHP;
    int points = 0;
    int modeCounter = 0;
    int multiplier = 1;
    int multTracker = 0;
    bool paused = false;

    void Awake() {
        Time.timeScale = 1;

        // ============== INPUT SETUP =========================
        input = new InputMaster();
        input.Enable();
        input.UI.Pause.performed += context => {
            pause();
        };

        // =================== EVENT SUBSCRIPTIONS ==============================
        GameEventManager.instance.onDamageTaken += takeDamage;
        GameEventManager.instance.onScore += score;
        GameEventManager.instance.onUseBomb += useBomb;
    }

    void Start() {
        curHP = maxHP;
        UIEventManager.instance.MaxHealthUpdate(maxHP);
        UIEventManager.instance.ScoreUpdateUI("SCORE: " + points + " (x" + multiplier + ")");

        int a = 1;
        DOTween.To(() => a, (y) => a = y, 0, 4.5f).OnComplete(activateTutorialDialogue);
    }

    void Update() {
        if(modeCounter >= 100) {
            float num = Random.Range(3, 5);
            GameEventManager.instance.ModeChange((BallSpawner.AttackState)((num >= 3.5f) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num)));
            modeCounter = 0;
        }

        if(curHP <= 0) {
            gameOver();
        }
    }

    // =========================== GAMEPLAY ==================================
    public void activateTutorialDialogue() {
        tutorialUI.SetActive(true);
    }

    void spawnBomb() {
        float randomX = Random.Range(-16.3f, 16.3f);
        float randomY = Random.Range(-7.38f, 7.38f);
        if(randomY < 1.3f && randomY > -1.3f) {
            int sign = (int)Mathf.Sign(randomY);
            randomY += sign * 1.3f;
        }
        if(randomX < 1.3f && randomX > -1.3f) {
            int sign = (int)Mathf.Sign(randomY);
            randomX += sign * 1.3f;
        }
        GameObject bomb = Instantiate(bombPF, new Vector2(randomX, randomY), Quaternion.identity);
    }

    public void score() {
        multTracker++;
        points += multiplier;
        modeCounter += multiplier;
        if(multTracker == multiplierInterval) {
            multiplier++;
            multTracker = 0;
            if(multiplier % 10 == 0) {
                spawnBomb();
            }
        }
        UIEventManager.instance.ScoreUpdateUI("SCORE: " + points + " (x" + multiplier + ")");
    }

    public void addBomb() {
        numBombs++;
        UIEventManager.instance.BombUpdateUI(numBombs.ToString());
    }
    public void useBomb(Vector2 location) {
        if(numBombs > 0) {
            GameObject ability = Instantiate(abilityPF, location, Quaternion.identity);
            numBombs--;
            UIEventManager.instance.BombUpdateUI(numBombs.ToString());
        }
    }
    public int bombs() {
        return numBombs;
    }

    public void takeDamage() {
        UIEventManager.instance.DamageTakenUI();
        curHP--;
        Debug.Log(curHP);
        multiplier = 1;
        multTracker = 0;
        UIEventManager.instance.ScoreUpdateUI("SCORE: " + points + " (x" + multiplier + ")");
        if(curHP <= 0) {
            gameOver();
        }
    }
    // ===================================================================

    // ============================ SYSTEM ================================
    void gameOver() {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void pause() {
        paused = !paused;
        Time.timeScale = (paused ? 0 : 1);
        pauseUI.SetActive(paused);
    }

    public void restart() {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // =====================================================================

    void OnDisable()
    {
        // Input Shutdown
        input.Disable();

        // Events Shutdown
        GameEventManager.instance.onDamageTaken -= takeDamage;
        GameEventManager.instance.onScore -= score;
        GameEventManager.instance.onUseBomb -= useBomb;
    }
}
