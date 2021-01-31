using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Galaga_Controller : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int enemiesRemaining;
    GameObject[] enemies;

    [SerializeField] Text scoreText;
    [SerializeField] Text gameStatusText;
    [SerializeField] int enemyStartingDirection;

    [SerializeField] float formationWidth;
    [SerializeField] float formationHeight;

    [SerializeField] GameObject enemyFormationBox;

    public bool leftFlipped = false;
    public bool rightFlipped = false;

    float enemySpeed = 6f;
    [SerializeField] int wavesNeededtoWin;
    int currentWave = 1;
    [SerializeField] GameObject[] EnemyWaves;
    bool playerHasWon = false;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        score = 0;
        scoreText.text = "Score: " + score;
        gameStatusText.text = "Wave: " + currentWave;

        wavesNeededtoWin = EnemyWaves.Length;
        //SetEnemyBounds();

        Instantiate(EnemyWaves[currentWave-1], new Vector3(0, 0, 0f), Quaternion.identity);

        enemyStartingDirection = Random.Range(0, 2);

        if (enemyStartingDirection == 0)
        {
            enemyStartingDirection = -1;
        }
        else
        {
            enemyStartingDirection = 1;
        }
    }

    public void DecreaseEnemyCount()
    {
        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        if (enemiesRemaining <= 0)
        {
            if (currentWave < wavesNeededtoWin)
            {
                Destroy(EnemyWaves[currentWave - 1].gameObject);

                currentWave++;
                gameStatusText.text = "Wave: " + currentWave;

                Instantiate(EnemyWaves[currentWave-1], new Vector3(0, 0, 0f), Quaternion.identity);

                enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
                enemiesRemaining = enemies.Length - 1;

                enemySpeed += 2;
            }
            else 
            {
                PlayerWin();
            }
        }
    }

    void PlayerWin()
    {
        gameStatusText.text = "You Win!";
        playerHasWon = true;
    }

    public int GetEnemyStartingDirection()
    {
        return enemyStartingDirection;
    }

    public int GetEnemyCount()
    {
        return enemiesRemaining;
    }

    public void FlipEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Galaga_Enemy>().FlipDirection();
        }

    }

    public void CalculateFormationWidth()
    {

    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public float GetEnemeySpeed()
    {
        return enemySpeed;
    }


    public int GetScore()
    {
        return score;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
