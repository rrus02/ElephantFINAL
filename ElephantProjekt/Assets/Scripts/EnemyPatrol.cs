using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;

    public Rigidbody2D rb;

    public LayerMask groundLayers;

<<<<<<< HEAD


    public Transform GroundCheck;
=======
    public Transform groundCheck;
>>>>>>> 620dc73d949013a49bd8fb2a32b67214c5dc1ab9

    bool isFacingRight = true;

    RaycastHit2D hit;




    private void Update()
    {

        hit = Physics2D.Raycast(GroundCheck.position, -transform.up, 1f, groundLayers);

    }



    private void FixedUpdate()
    {

        if (hit.collider != false)
        {

            if (isFacingRight)
            {

                rb.velocity = new Vector2(speed, rb.velocity.y);

            }
            else
            {

                rb.velocity = new Vector2(-speed, rb.velocity.y);

            }

        }
        else
        {

            isFacingRight = !isFacingRight;

            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);

        }

    }

}