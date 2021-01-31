using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Enemy_Formation : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 10.0f;
    public Vector2 direction;
    public Rigidbody2D rb;

    private float xBoundary = 15;
    public int startingDirection;
    private Galaga_Controller gameController;

    [SerializeField] bool flippedLeft;
    [SerializeField] bool flippedRight;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Galaga_Controller").GetComponent<Galaga_Controller>();

        startingDirection = gameController.GetEnemyStartingDirection();

        xBoundary = xBoundary - this.gameObject.transform.localScale.x / 2;

        if (startingDirection == 0)
        {
            startingDirection = -1;
            flippedRight = true;
            flippedLeft = false;
        }
        else
        {
            startingDirection = 1;
            flippedLeft = true;
            flippedRight = false;
        }

        direction.x = startingDirection;
        direction.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.x < -xBoundary && !flippedLeft)
        {
            gameController.FlipEnemies();
            direction.x *= -1;
            flippedLeft = true;
            flippedRight = false;
            //Debug.Log("Flip Left");
        }
        else if (rb.position.x > xBoundary && !flippedRight)
        {
            gameController.FlipEnemies();
            direction.x *= -1;
            flippedRight = true;
            flippedLeft = false;
            //Debug.Log("Flip Right");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * enemyMoveSpeed * Time.fixedDeltaTime);
    }

}
