using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public const float defaultMoveSpeed = 6f;
    public const float defaultJumpSpeed = 10f;
    public const float defaultGravity = 2f;
    
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject playerObject;
    public static Vector2 moveValue;
    public static bool isGrounded;
    public float moveSpeed;
    public float jumpSpeed;

    //Wall slide variables
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    
    //wall jump variables
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.gravityScale = defaultGravity;
    }

    public void move(InputAction.CallbackContext context) {
        moveValue = context.ReadValue<Vector2>();

        if (moveValue.x != 0)
            playerObject.transform.localScale = 
            new Vector3 (moveValue.x, transform.localScale.y, 1);
    }

    public void jump(InputAction.CallbackContext context) {
        //Grounded jump
        moveValue.y = 1;
        if(context.performed && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if(context.performed && wallSliding==true)
        {
            wallJumping = true;
            Invoke("setWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping ==true)
        {
            rb.velocity = new Vector2(xWallForce * moveSpeed, yWallForce);
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    private bool IsTouchingFront() {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.2f, groundLayer);
        return isTouchingFront;
    }

    //Uncomment for non-grounded jump
    //public float numJumps = 1;
    // private void IsGrounded() {
    //     if(Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)){
    //         numJumps = 1;
    //     }
    // }

    private void Update()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.2f, groundLayer);

        if(IsTouchingFront()==true && IsGrounded()==false) {
            wallSliding = true;
        }
        else {
            wallSliding = false;
        }
        if (wallSliding) 
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveValue.x*moveSpeed, rb.velocity.y);

        
    } 

    void setWallJumpingToFalse()
    {
        wallJumping = false;
    }



    

}
