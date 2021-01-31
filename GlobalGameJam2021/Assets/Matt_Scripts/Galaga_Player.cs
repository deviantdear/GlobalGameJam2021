using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float playerMoveSpeed = 5.0f;
    public Vector2 direction;
    public Rigidbody2D rb;
    private Galaga_Controller gameController;

    private float xBoundary = 15;
    [SerializeField] bool canShoot = true;
    [SerializeField] GameObject playerBullet;

    float shotDelay = .75f;
    float shotTimer = 0f;

    [SerializeField] AudioSource hitSound;

    Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
        gameController = GameObject.FindGameObjectWithTag("Galaga_Controller").GetComponent<Galaga_Controller>();
        Initialize();
    }

    public void Initialize()
    {
        transform.position = startingPosition;
        canShoot = true;
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    { 
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = 0;

        if (Input.GetKey(KeyCode.Space))
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
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Galaga_Enemy_Bullet")
        {
            hitSound.Play();
        }
    }

    private void FixedUpdate()
    {
        if (gameController.GetGameActive())
        {
            //Movement
            if ((direction.x > 0 && rb.position.x < xBoundary) || (direction.x < 0 && rb.position.x > -xBoundary))
            {
                rb.MovePosition(rb.position + direction * playerMoveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            Instantiate(playerBullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + this.gameObject.transform.localScale.y, 0f), Quaternion.identity);
        }
    }


}
