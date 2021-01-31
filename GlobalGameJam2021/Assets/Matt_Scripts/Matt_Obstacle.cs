using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Obstacle: MonoBehaviour
{ 
    [SerializeField] private Rigidbody2D obstacleRigidBody;
    [SerializeField] private Matt_Controller gameController;

    private int scoreValue;
    private bool scoreCounted = false;
    private float moveVelocity = 0f;
    private float offScreenLimit = -25;
    private bool gameOver = false;



    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();
        moveVelocity = gameController.GetObstacleMoveVelocity();
        scoreValue = gameController.GetObstacleScoreValue();
        //offScreenLimit = Camera.current.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameController.GetGameActive())
        {
            moveVelocity = gameController.GetObstacleMoveVelocity();
            obstacleRigidBody.velocity = Vector2.left * moveVelocity;
            CheckIfBehindPlayer();
            CheckIfOffSceen();
        }
        else if (!gameOver)
        {
            stopObstacle();
        }
    }

    void CheckIfBehindPlayer()
    {
        if (!scoreCounted)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Matt_Player");

            if (this.gameObject.transform.position.x < player.transform.position.x - player.transform.localScale.x) // if behind player (player has cleared obstacle)
            {
                gameController.ChangeScore(scoreValue);
                scoreCounted = true;
            }
        }
    }

    void CheckIfOffSceen()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Matt_Player");

        if (this.gameObject.transform.position.x < player.transform.position.x + offScreenLimit )
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    void stopObstacle()
    {
        gameOver = true;
        moveVelocity = gameController.GetObstacleMoveVelocity();
        obstacleRigidBody.velocity = Vector2.left * moveVelocity;
    }

}
