using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaga_Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float bulletMoveSpeed = 40f;
    public Vector2 direction;
    public Rigidbody2D rb;
    private Galaga_Controller gameController;

    private float yBoundary = 45;

    // Start is called before the first frame update
    void Start()
    {
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
            // gameController.ChangeScore(collision.gameObject.GetComponent<Galaga_Player>().GetScoreValue());
            //Destroy(collision.gameObject);
            //gameController.DecreaseEnemyCount();
            Destroy(this.gameObject);
            Debug.Log("Enemy Hit Player");
        }
    }

    // Update is called once per frame
    void Update()
    {

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
