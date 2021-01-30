using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matt_Controller : MonoBehaviour
{
    private float basePlayerJumpVelocity = 12f;
    [SerializeField] private float playerJumpVelocity = 12f;
    [SerializeField] private float obstacleMoveVelcoity = 8f;
    [SerializeField] private bool gameActive = true;
    [SerializeField] private int score = 0;
    [SerializeField] private int obstacleScoreValue = 10;
    [SerializeField] private int scoreToWin = 500;

    [SerializeField] private Text gameStatusText;
    [SerializeField] private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
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
        else
        {

        }
    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
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
        if (gameActive)
        {
            //Debug.Log("Game Over");
            gameStatusText.text = "Game Over";
            gameActive = false;
            playerJumpVelocity = 0;
            obstacleMoveVelcoity = 0f;
        }
    }


}
