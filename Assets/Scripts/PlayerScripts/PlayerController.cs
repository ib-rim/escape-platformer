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

    public Transform ceilingCheck;

    public Animator player_animator;

    void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.gravityScale = defaultGravity;
    }

    private void Update()
    {
        // Conditions for triggering player character's animations

        // Idle
        if (moveValue == new Vector2(0f,0f) && IsGrounded())
        {
            player_animator.SetBool("jump", false);
            player_animator.SetBool("run", false);
        }
        // Jump
        else if (moveValue.y == 1f && IsGrounded() == false)
        {
            player_animator.SetBool("jump", true);
            player_animator.SetBool("run", false);
        }
        // Move
        else if (moveValue.x != 0f && IsGrounded())
        {
            player_animator.SetBool("run", true);
            player_animator.SetBool("jump", false);
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        // When key pressed, set value to 1 or -1 (depending on direction), and 0 when released
        moveValue = context.ReadValue<Vector2>();
        // Flip the player's sprite horizontally when moving left or right
/*        if (moveValue.x > 0)
            playerObject.transform.localScale =
                new Vector3(1, transform.localScale.y, 1);
        if (moveValue.x < 0)
            playerObject.transform.localScale =
                new Vector3(-1, transform.localScale.y, 1);*/
    }

    public void jump(InputAction.CallbackContext context)
    {
        //Allow player to jump when grounded
        if (context.performed && IsGrounded())
        {
            moveValue.y = 1;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        else
        {
            moveValue.y = 0;
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
        }

        if (context.canceled && CanStand())
        {
            playerObject.transform.localScale = new Vector3(playerObject.transform.localScale.x, defaultPlayerHeight, playerObject.transform.localScale.z);
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    private bool CanStand()
    {
        canStand = !Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayer);
        return canStand;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveValue.x * moveSpeed, rb.velocity.y);
    }
}
