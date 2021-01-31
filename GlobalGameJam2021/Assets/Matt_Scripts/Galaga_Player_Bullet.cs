using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Player_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //Animator bulletAnimator;

    private float bulletMoveSpeed = 40f;
    public Vector2 direction;
    public Rigidbody2D rb;
    private Galaga_Controller gameController;

    private float yBoundary = 45;

    [SerializeField] AudioSource spawnSound;

    void Start()
    {

        spawnSound.Play();
        gameController = GameObject.FindGameObjectWithTag("Galaga_Controller").GetComponent<Galaga_Controller>();
        //bulletAnimator = this.gameObject.GetComponent<Animator>();
        //bulletAnimator.speed /= 8;

        direction.x = 0;
        direction.y = Mathf.Sign(bulletMoveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Galaga_Enemy")
        {
            collision.gameObject.GetComponent<Galaga_Enemy>().PlaySound();
            collision.gameObject.GetComponent<Galaga_Enemy>().Disable();
            //collision.gameObject.SetActive(false);
            gameController.ChangeScore(collision.gameObject.GetComponent<Galaga_Enemy>().GetScoreValue());
            Destroy(collision.gameObject, .2f);
            gameController.DecreaseEnemyCount();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * bulletMoveSpeed * Time.fixedDeltaTime);

        if (this.gameObject.transform.position.y < -yBoundary || this.gameObject.transform.position.y > yBoundary)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (!gameController.GetGameActive()) { Destroy(this.gameObject); }
    }
}
