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
    public static bool isPressingPullKey;
    public float moveSpeed;
    public float jumpSpeed;

    void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.gravityScale = defaultGravity;
    }

    public void move(InputAction.CallbackContext context) {
        moveValue = context.ReadValue<Vector2>();
    }

    public void pull(InputAction.CallbackContext context) {
        isPressingPullKey = context.performed ? true : false;
    }

    public void jump(InputAction.CallbackContext context) {

        //Allow player to jump when grounded
        moveValue.y = 1;
        if(context.performed && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //Lower jump height if jump not held down fully
        if(context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
        }
    }

    private bool IsGrounded() {
        // Will be called each frame to check if the player is on the ground
        // Used as a condition for jump and push/pull
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveValue.x*moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        IsGrounded();
    }

}
