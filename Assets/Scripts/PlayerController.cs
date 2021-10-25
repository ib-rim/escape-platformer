using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float moveSpeed = 5f;
    private float jumpSpeed = 5f;

    public Vector2 moveValue;

    public void move(InputAction.CallbackContext context) {
        moveValue = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context) {
        //Grounded jump
        if(context.performed && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //Uncomment for non-grounded jump
        // IsGrounded();
        // if(context.performed && numJumps == 1) {
        //      rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        //      numJumps = 0;
        // }

        if(context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
        }
    }

    //Grounded jump
    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Uncomment for non-grounded jump
    //public float numJumps = 1;
    // private void IsGrounded() {
    //     if(Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)){
    //         numJumps = 1;
    //     }
    // }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Pitfall")
        {
            collision.gameObject.SetActive(false);
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveValue.x*moveSpeed, rb.velocity.y);
    } 

}
