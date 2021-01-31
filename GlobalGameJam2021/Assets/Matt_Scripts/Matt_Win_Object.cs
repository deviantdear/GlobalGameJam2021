using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Win_Object : MonoBehaviour
{

    [SerializeField] private Rigidbody2D winRigidBody;
    [SerializeField] private Matt_Controller gameController;
    private float moveVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();
        moveVelocity = gameController.GetObstacleMoveVelocity();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveVelocity = gameController.GetObstacleMoveVelocity();
        winRigidBody.velocity = Vector2.left * moveVelocity;
    }
}
