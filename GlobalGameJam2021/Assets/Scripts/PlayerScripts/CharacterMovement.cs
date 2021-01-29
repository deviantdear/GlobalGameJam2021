using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterMovement : MonoBehaviour
    {
        public Vector2 direction;
        public float speed = 5f;
        public Rigidbody2D rb;

        // Update is called once per frame
        void Update()
        {
            //Input in -1 or 1
           direction.x = Input.GetAxisRaw("Horizontal");
           direction.y = Input.GetAxisRaw("Vertical");

        }

        //For physics interactions 
        private void FixedUpdate()
        {
            //Movement
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}