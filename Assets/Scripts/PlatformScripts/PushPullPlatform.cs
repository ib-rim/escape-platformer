using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullPlatform : MonoBehaviour
{
    public Rigidbody2D boxRigidbody;
    private bool boxIsGrounded;

    private void Start()
    {
        boxRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // If player is dragging box along the ground, box should move
        if (PlayerPushPull.isPulling && boxIsGrounded)
        {
            print("Player pushing && box grounded");
            boxRigidbody.constraints = RigidbodyConstraints2D.None;
            //boxRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else 
        {
            // If box is in the ground, and player is not pulling, then box should be immovable
            if (boxIsGrounded)
            {
                print("box grounded");
                boxRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
                
            // If player is dragging the box through the air while falling, box should move
            if (PlayerPushPull.isPulling)
            {
                print("player pushing");
                boxRigidbody.constraints = RigidbodyConstraints2D.None;
                boxRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            }       
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StaticPlatform"))
        {
            boxIsGrounded = true;
            //print(boxIsGrounded);
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StaticPlatform"))
        {
            boxIsGrounded = false;
            //print(boxIsGrounded);
        }
    }
}
