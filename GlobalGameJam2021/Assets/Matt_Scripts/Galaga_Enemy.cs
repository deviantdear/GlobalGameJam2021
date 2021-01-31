using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Enemy : MonoBehaviour
{

    [SerializeField] private float enemyMoveSpeed = 5.0f;
    public Vector2 direction;
    public Rigidbody2D rb;

    private float xBoundary = 15;
    public int startingDirection;
    [SerializeField] bool flippedLeft = false;
    [SerializeField] bool flippedRight = false;

    [SerializeField] int scoreValue = 10;
    private Galaga_Controller gameController;
    [SerializeField] GameObject enemyBullet;

    [SerializeField] bool canShoot = false;
    [SerializeField] float shotDelayOffset;
    [SerializeField] float shotDelay;
    [SerializeField] float shotTimer;
     bool isFront;

    float startingX;
    float startingY;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Galaga_Controller").GetComponent<Galaga_Controller>();

        Initialize();      
    }

    public bool IsFrontEnemy()
    {

        //return true;
        ////RaycastHit2D hit = Physics2D.BoxCast(this.GetComponent<BoxCollider2D>().bounds.center, this.GetComponent<BoxCollider2D>().bounds.size, 0f, Vector2.down, 10f);

        //RaycastHit2D hit = Physics2D.BoxCast(this.GetComponent<BoxCollider2D>().bounds.center, this.GetComponent<BoxCollider2D>().bounds.size, 0f, Vector2.down, 10f, default);

        //Vector3 castPosLeft = new Vector3(this.transform.position.x - this.transform.localScale.x/2, this.transform.position.y - this.transform.localScale.y, this.transform.position.z);
        //RaycastHit2D hit1 = Physics2D.Raycast(castPosLeft, Vector2.down,  12f, default);
        //Debug.DrawRay(castPosLeft, Vector2.down * 12);

        Vector3 castPosMid = new Vector3(this.transform.position.x, this.transform.position.y - this.transform.localScale.y, this.transform.position.z);
        //RaycastHit2D hit2 = Physics2D.Raycast(castPosMid, Vector2.down, 12f, default);
        RaycastHit2D hit2 = Physics2D.BoxCast(castPosMid, this.GetComponent<BoxCollider2D>().bounds.size * 1.1f, 0f, Vector2.down, 10f, default);
        //Debug.DrawRay(castPosMid, Vector2.down * 12);

        //Vector3 castPosRight = new Vector3(this.transform.position.x + this.transform.localScale.x / 2, this.transform.position.y - this.transform.localScale.y, this.transform.position.z);
        //RaycastHit2D hit3 = Physics2D.Raycast(castPosRight, Vector2.down, 12f, default);
        //Debug.DrawRay(castPosRight, Vector2.down * 12);


        //if (hit1.collider != null)
        //{
        //    if (hit1.collider.CompareTag("Galaga_Enemy"))
        //    {
        //        // do not shoot, blocked by other enemy
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        if (hit2.collider != null)
        {
            if (hit2.collider.CompareTag("Galaga_Enemy"))
            {
                // do not shoot, blocked by other enemy
                return false;
            }
            else
            {
                return true;
            }
        }
        //else if (hit3.collider != null)
        //{
        //    if (hit3.collider.CompareTag("Galaga_Enemy"))
        //    {
        //        // do not shoot, blocked by other enemy
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        else // no collisions detected
        {
            return true;
        }
    }

    public float GetStartingX()
    {
        return startingX;
    }
    public float GetStartingY()
    {
        return startingY;
    }

    void Initialize()
    {
        enemyMoveSpeed = gameController.GetEnemeySpeed();

        shotDelayOffset = Random.Range(1, 25f);
        shotDelay = Random.Range(3, 7.5f) + shotDelayOffset;
        //shotTimer = shotDelay;
        shotTimer = shotDelayOffset;
        canShoot = false;

        startingX = this.gameObject.transform.position.x;
        startingY = this.gameObject.transform.position.y;

        startingDirection = gameController.GetEnemyStartingDirection();

        if (startingDirection == 0)
        {
            startingDirection = -1;
        }
        else
        {
            startingDirection = 1;
        }

        isFront = IsFrontEnemy();

        float xOffset = Random.Range(0, .00005f);
        int xOffsetSign = Random.Range(0, 2);
        if (xOffsetSign == 0) { xOffset *= -1; }

        transform.position = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z);

        direction.x = startingDirection;
        direction.y = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.x < -xBoundary && !gameController.leftFlipped)
        {
            gameController.FlipEnemies();
            gameController.leftFlipped = true;
            gameController.rightFlipped = false;
        }
        else if (rb.position.x > xBoundary && !gameController.rightFlipped)
        {
            gameController.FlipEnemies();
            gameController.rightFlipped = true;
            gameController.leftFlipped = false;
        }  

        if (canShoot && isFront)
        {
            Shoot();
        }    
        if (!canShoot)
        {
            shotTimer -= Time.deltaTime;
        }
        if (shotTimer <= 0)
        {
            shotTimer = shotDelay;
            canShoot = true;
            //Debug.Log("Can shoot set to true");
        }

    }

    void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            // spawn enemy bullet
            Instantiate(enemyBullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - this.gameObject.transform.localScale.y, 0f), Quaternion.identity);
            Debug.Log("Enemy Fired Bullet");
        }
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }

    public void FlipDirection()
    {
        direction.x *= -1;
    }


    private void FixedUpdate()
    {
        isFront = IsFrontEnemy();

        rb.MovePosition(rb.position + direction * enemyMoveSpeed * Time.fixedDeltaTime);

        //transform.position = new Vector3(transform.position.x + direction.x * enemyMoveSpeed/10, transform.position.y, transform.position.z);
    }

}
