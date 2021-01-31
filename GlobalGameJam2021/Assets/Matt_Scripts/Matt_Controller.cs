using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matt_Controller : MonoBehaviour
{
    private float basePlayerJumpVelocity;
    [SerializeField] private float playerJumpVelocity;
    [SerializeField] private float obstacleMoveVelcoity;
    [SerializeField] private bool gameActive;
    [SerializeField] private bool gameOver;
    [SerializeField] private int score;
    [SerializeField] private int obstacleScoreValue;
    [SerializeField] private int scoreToWin;

    bool gameRestarted = false;

    [SerializeField] private Text gameStatusText;
    [SerializeField] private Text scoreText;

    [SerializeField] Matt_Spawner spawner;


    void Initialize()
    {
        basePlayerJumpVelocity = 15f;
        playerJumpVelocity = 15f;
        obstacleMoveVelcoity = 8f;
        gameActive = true;
        gameOver = false;
        score = 0;
        obstacleScoreValue = 10;
        scoreToWin = 650;

        scoreText.text = "Score: " + score;
        gameStatusText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        spawner = GameObject.FindGameObjectWithTag("Matt_Spawner").GetComponent<Matt_Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= scoreToWin)
        {
            StopPlayerJump();
        }

        if (gameActive)
        {

        }
        else if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

        }
    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public bool GetGameActive()
    {
        return gameActive;
    }

    public float GetPlayerJumpVelocity()
    {
        return playerJumpVelocity;
    }

    public float GetObstacleMoveVelocity()
    {
        return obstacleMoveVelcoity;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetScoreToWin()
    {
        return scoreToWin;
    }

    public int GetObstacleScoreValue()
    {
        return obstacleScoreValue;
    }

    public void StopPlayerJump()
    {
        playerJumpVelocity = 0f;
    }

    public void PlayerWins()
    {
        if (gameActive)
        {
            gameActive = false;
            gameStatusText.text = "You Win!";
            obstacleMoveVelcoity = 0f;
        }
    }


    public void GameOver()
    {
        if (gameActive && !gameOver)
        {
            //Debug.Log("Game Over");
            gameStatusText.text = "Game Over - Press R to Restart";
            gameActive = false;
            gameOver = true;
            playerJumpVelocity = 0;
            obstacleMoveVelcoity = 0f;
        }
    }


    public void DeleteAllObstacles()
    {
        GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("Matt_Obstacle");

        foreach(var obstacle in Obstacles)
        {
            Destroy(obstacle.gameObject);
        }        
    }

    public void DeleteWin()
    {
        GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("Matt_Win");

        foreach (var obstacle in Obstacles)
        {
            Destroy(obstacle.gameObject);
        }
    }

    public void RestartGame()
    {
        gameRestarted = true;
        DeleteAllObstacles();
        DeleteWin();
        spawner.Initialize();
        Initialize();     
    }

}
