using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public const float defaultMoveSpeed = 6f;
    public const float defaultJumpSpeed = 10f;
    public const float defaultGravity = 2f;
    public const float defaultPlayerHeight = 1.9f;
    public const float defaultCrouchHeight = 0.95f;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject playerObject;
    public static Vector2 moveValue;
    public static bool isGrounded;
    public float moveSpeed;
    public float jumpSpeed;

    public static bool canStand;
    public static bool crouching;

    public Transform ceilingCheck;

    void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.gravityScale = defaultGravity;
    }

    public void move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context)
    {
        //Allow player to jump when grounded
        moveValue.y = 1;
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //Lower jump height if jump not held down fully
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerObject.transform.localScale = new Vector3(playerObject.transform.localScale.x, defaultCrouchHeight, playerObject.transform.localScale.z);
            crouching = true;
        }

        if (context.canceled)
        {
            crouching = false;
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        return isGrounded;
    }

    private bool CanStand()
    {
        canStand = !Physics2D.OverlapCircle(ceilingCheck.position, 0.5f, groundLayer);
        return canStand;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveValue.x * moveSpeed, rb.velocity.y);
        if(!crouching && CanStand()) {
            playerObject.transform.localScale = new Vector3(playerObject.transform.localScale.x, defaultPlayerHeight, playerObject.transform.localScale.z);
        }
    }

}
