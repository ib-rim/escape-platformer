using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float movementSpeed;

   [HideInInspector]
   public bool patrolling;
   private bool turning;

   public Rigidbody2D rb1;
   public Transform groundCheckPosition;
   

   public LayerMask groundLayer;

   void start() 
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
       }
   }

   void Patrol()
   {

       if (turning)
       {
           Flip();
       }

       rb1.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb1.velocity.y);
   }

   void Flip()
   {
       patrolling = false;
       transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
       movementSpeed *= -1;
       patrolling = true;
   }
}
