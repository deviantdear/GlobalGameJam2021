using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Galaga_Controller : QuestTrigger
{
    [SerializeField] private int score;
    [SerializeField] private int enemiesRemaining;
    GameObject[] enemies;

    [SerializeField] Text scoreText;
    [SerializeField] Text gameStatusText;
    [SerializeField] int enemyStartingDirection;

    [SerializeField] float formationWidth;
    [SerializeField] float formationHeight;

    [SerializeField] Galaga_Player player;
    [SerializeField] GameObject enemyFormationBox;
    [SerializeField] GameObject playerHearts;

    public bool leftFlipped = false;
    public bool rightFlipped = false;

    int maxPlayerLives;
    int currentPlayerLives;

    float enemySpeed = 6f;
    [SerializeField] int wavesNeededtoWin;
    int currentWave = 1;
    [SerializeField] GameObject[] EnemyWaves;
    public bool playerHasWon = false;
    public bool gameOver = false;
    public bool gameActive = true;


    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource loseSound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] private GameObject turnOffOnWin = null;
    [SerializeField] private GameObject overWorld = null;

    // Start is called before the first frame update
    void Start()
    {      
        Initialize();
    }

    private void Initialize()
    {
        musicPlayer.Play();
        musicPlayer.loop = true;
        playerHasWon = false;
        gameOver = false;
        gameActive = true;     

        leftFlipped = false;
        rightFlipped = false;

        maxPlayerLives = 3;
        currentPlayerLives = maxPlayerLives;

        player = GameObject.FindGameObjectWithTag("Galaga_Player").GetComponent<Galaga_Player>();

        if (player.renderer.enabled == false)
        {
            player.renderer.enabled = true;
        }

        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        score = 0;
        scoreText.text = "Score: " + score;
       

        currentWave = 1;
        wavesNeededtoWin = EnemyWaves.Length;
        gameStatusText.text = "Wave: " + currentWave;
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

        var heartChildren = playerHearts.transform.root.GetComponentsInChildren<Transform>();
        int heartCount = heartChildren.Length - 1;

        for (int i = 1; i <= heartCount; i++)
        {
            heartChildren[i].gameObject.GetComponent<Renderer>().enabled = true;
        }

    }


    public void DecreasePlayerHealth()
    {

        var heartChildren = playerHearts.transform.root.GetComponentsInChildren<Transform>();
        int heartCount = heartChildren.Length - 1;

        heartChildren[currentPlayerLives].gameObject.GetComponent<Renderer>().enabled = false;
        //heartChildren[currentPlayerLives].gameObject.SetActive(false);

        currentPlayerLives--;

        if (currentPlayerLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameStatusText.text = "Game Over - Press R To Resart";
        musicPlayer.Pause();
        deathSound.Play();
        Invoke("PlayLoseSound", .4f);
        player.renderer.enabled = false;
        gameOver = true;
        gameActive = false;
        
        Trigger();
        overWorld?.SetActive(true);
        turnOffOnWin?.SetActive(false);
    }

    void PlayLoseSound()
    {
        loseSound.Play();
    }

    public void DecreaseEnemyCount()
    {
        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        if (enemiesRemaining <= 0)
        {
            if (currentWave < wavesNeededtoWin)
            {
                Destroy(enemies[enemiesRemaining].transform.parent.gameObject, .2f);

                //Destroy(EnemyWaves[currentWave - 1].gameObject);

                currentWave++;
                gameStatusText.text = "Wave: " + currentWave;

                Invoke("SpawnWave", 1f);

                //Instantiate(EnemyWaves[currentWave-1], new Vector3(0, 0, 0f), Quaternion.identity);

                leftFlipped = false;
                rightFlipped = false;

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

    void SpawnWave()
    {
        Instantiate(EnemyWaves[currentWave - 1], new Vector3(0, 0, 0f), Quaternion.identity);
    }

    void PlayerWin()
    {
        musicPlayer.Pause();
        winSound.Play();
        gameStatusText.text = "You Win!";
        playerHasWon = true;
        gameActive = false;
        Trigger();
        overWorld?.SetActive(true);
        turnOffOnWin?.SetActive(false);
    }

    public bool GetGameActive()
    {
        return gameActive;
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

    public void DestroyAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Galaga_Enemy");
        enemiesRemaining = enemies.Length - 1;

        for (int i = 0; i < enemiesRemaining; i++)
        {
            Destroy(enemies[i].gameObject);
        }

        Destroy(enemies[enemiesRemaining].transform.parent.gameObject);
        Destroy(enemies[enemiesRemaining].gameObject);


        //foreach (var enemy in enemies)
        //{
        //    Destroy(enemy.gameObject);
        //    //enemy.GetComponent<Galaga_Enemy>().FlipDirection();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && gameActive == false && !playerHasWon)
        {
            loseSound.Stop();
            player.Initialize();
            DestroyAllEnemies();
            Initialize();
        }
    }
}
