using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private Matt_Controller gameController;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool isPlayerAlive = true;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private bool isOnGround = false;



    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();
        //jumpVelocity = gameController.GetPlayerJumpVelocity();
    }

    // Update is called once per frame
    void LateUpdate()
    {
            //isOnGround = CheckGround();
        jumpVelocity = gameController.GetPlayerJumpVelocity();

        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            playerRigidBody.velocity = Vector2.up * jumpVelocity;
        }
        else if (isPlayerAlive)
        {
            stopPlayer();
        }

    }

    void stopPlayer()
    {
        isPlayerAlive = false;
        jumpVelocity = gameController.GetObstacleMoveVelocity();
        playerRigidBody.velocity = Vector2.up * jumpVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Matt_Obstacle")
        {
            gameController.GameOver();
        }
        else if (collision.gameObject.tag == "Matt_Win")
        {
            Debug.Log("Hit Win");
            gameController.PlayerWins();
        }
    }

    bool CheckGround()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(playerBoxCollider.bounds.center, playerBoxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);

        if (raycast.collider != null)
        {
            return true;
        }

        return false;
    }


}
