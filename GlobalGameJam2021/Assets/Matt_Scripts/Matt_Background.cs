using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Background : MonoBehaviour
{
    private float moveSpeed = .1f;
    private float startingX;

    [SerializeField] private Matt_Controller gameController;

    private float offScreenLimit = -100;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.GetGameActive())
        {
            transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);

            if (transform.position.x < offScreenLimit)
            {
                transform.position = new Vector3(startingX, transform.position.y, transform.position.z);
            }
        }
    }
}
