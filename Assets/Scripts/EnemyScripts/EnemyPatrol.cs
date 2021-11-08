using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    
    public float movementSpeed;

   [HideInInspector]
   public bool patrolling;
   private bool turning;
   private bool turningWall;

   public Rigidbody2D rb;
   public Transform groundCheckPosition;
   public Transform wallCheckPosition;
   

   public LayerMask groundLayer;
   public LayerMask wallLayer;

   void Start() 
   {
       patrolling = true;
   }

   void Update()
   {
       if (patrolling)
       {
           Patrol();
       }
   }

   private void FixedUpdate()
   {
       if (patrolling)
       {
           turning = !Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f , groundLayer);
           turningWall = Physics2D.OverlapCircle(wallCheckPosition.position, 0.1f, wallLayer);
       }
   }

   void Patrol()
   {

       if (turning)
       {
           Flip();
       }
       
       if (turningWall)
       {
           Flip();
       }

       rb.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
   }

   void Flip()
   {
       patrolling = false;
       transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
       movementSpeed *= -1;
       patrolling = true;
   }
}
