using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float bulletMoveSpeed = 20f;
    public Vector2 direction;
    public Rigidbody2D rb;
    private Galaga_Controller gameController;

    [SerializeField] AudioSource spawnSound;

    private float yBoundary = 45;

    // Start is called before the first frame update
    void Start()
    {
        spawnSound.Play();
        gameController = GameObject.FindGameObjectWithTag("Galaga_Controller").GetComponent<Galaga_Controller>();
        //bulletAnimator = this.gameObject.GetComponent<Animator>();
        //bulletAnimator.speed /= 8;

        direction.x = 0;
        direction.y = -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Galaga_Player")
        {
            gameController.DecreasePlayerHealth();
            Destroy(this.gameObject);
            //Debug.Log("Enemy Hit Player");
        }

        if (collision.gameObject.tag == "Galaga_Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>());
        }

        if (collision.gameObject.tag == "Galaga_Player_Bullet")
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetGameActive()) { Destroy(this.gameObject); }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * bulletMoveSpeed * Time.fixedDeltaTime);

        if (this.gameObject.transform.position.y < -yBoundary || this.gameObject.transform.position.y > yBoundary)
        {
            Destroy(this.gameObject);
        }
    }
}
