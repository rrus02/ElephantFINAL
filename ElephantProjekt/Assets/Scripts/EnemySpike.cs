using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class EnemySpike : MonoBehaviour
{
    public float speed = 2f;

    public Rigidbody2D rb;

    public LayerMask groundLayers;

    public Transform groundCheck;

    RaycastHit2D hit;

    bool isTurnedOn = true;



    private void Update()
    {

        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);

    }



    private void FixedUpdate()
    {

        if (hit.collider != false)
        {

            if (isTurnedOn)
            {

                rb.velocity = new Vector2(-speed, +rb.velocity.y);

            }
            else
            {

                rb.velocity = new Vector2(-speed, -rb.velocity.y);

            }

        }
        else
        {

            isTurnedOn = !isTurnedOn;

            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);

        }

    }

}