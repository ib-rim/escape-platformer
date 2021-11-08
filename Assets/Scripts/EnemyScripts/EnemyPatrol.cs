using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float movementSpeed;

    [HideInInspector]
    public bool patrolling;

    public Rigidbody2D rb;
    public Transform groundCheck;

    public LayerMask groundLayer;

    void Start() 
    {
        patrolling = true;
    }

    private void FixedUpdate()
    {
        if (patrolling)
        {   
            //Check if at edge of platform and flip enemy if so
            if(!Physics2D.OverlapCircle(groundCheck.position, 0.1f , groundLayer)) {
                Flip();
            }
            
            rb.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }

    }

    //Move enemy in opposite direction
    void Flip()
    {
        patrolling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        movementSpeed *= -1;
        patrolling = true;
    }
}
