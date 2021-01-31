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

        //SetEnemyBounds();

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
        enemiesRemaining = enemies.Length;

        if (enemiesRemaining <= 0)
        {
            PlayerWin();
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

    public int GetScore()
    {
        return score;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
