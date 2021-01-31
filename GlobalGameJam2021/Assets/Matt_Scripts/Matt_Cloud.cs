using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Cloud : MonoBehaviour
{
    [SerializeField] private Rigidbody2D cloudRigidBody;
    [SerializeField] private Matt_Controller gameController;


    private float moveVelocity = 0f;
    private float offScreenLimit = -25;
    private bool gameOver = false;
    float velocityOffset;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();

        velocityOffset = Random.Range(0f, 2f);
        moveVelocity = gameController.GetObstacleMoveVelocity() / 3 + velocityOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameController.GetGameActive())
        {
            moveVelocity = gameController.GetObstacleMoveVelocity() / 3 + velocityOffset;
            cloudRigidBody.velocity = Vector2.left * moveVelocity;
            CheckIfOffSceen();
        }
        else if (!gameOver)
        {
            stopObstacle();
        }
    }


    void CheckIfOffSceen()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Matt_Player");

        if (this.gameObject.transform.position.x < player.transform.position.x + offScreenLimit)
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    void stopObstacle()
    {
        gameOver = true;
        moveVelocity = gameController.GetObstacleMoveVelocity();
        cloudRigidBody.velocity = Vector2.left * moveVelocity;
    }

}
