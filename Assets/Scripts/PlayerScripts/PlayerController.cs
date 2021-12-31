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

    public BoxCollider2D boxCollider;

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

    public Animator player_animator;

    public PauseMenuController pauseMenu;

    void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.gravityScale = defaultGravity;
        AudioManager.instance.PlaySFX("Footsteps");

        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenuController>();
    }

    private void Update()
    {
        // Conditions for triggering player character's animations

        // Idle
        if (moveValue.x == 0f && IsGrounded())
        {
            resetAnimatorParameters();
            player_animator.SetBool("idle", true);
        }
        // Move
        else if (moveValue.x != 0f && IsGrounded())
        {
            resetAnimatorParameters();
            player_animator.SetBool("move", true);
        }
        // Jump
        else if (rb.velocity.y > 1f && !IsGrounded())
        {
            resetAnimatorParameters();
            player_animator.SetBool("jump", true);
        }
        // Fall
        else if (rb.velocity.y < -1f && !IsGrounded())
        {
            resetAnimatorParameters();
            player_animator.SetBool("fall", true);
        }        

        //Standing
        if(!crouching && CanStand()) {
            player_animator.SetBool("crouch", false);
            player_animator.SetBool("stand", true);
            boxCollider.offset = new Vector2(0f, 0.02f);
            boxCollider.size = new Vector2(0.5f, 0.98f);
        }
        else {
            player_animator.SetBool("stand", false);
        }

    
    }

    public void resetAnimatorParameters() {
        player_animator.SetBool("idle", false);
        player_animator.SetBool("move", false);
        player_animator.SetBool("jump", false);
        player_animator.SetBool("fall", false);
        player_animator.SetBool("death", false);
    }

    public void move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();

        if (moveValue.x > 0)
            playerObject.transform.localScale =
                new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        if (moveValue.x < 0)
            playerObject.transform.localScale =
                new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, 1);
    }

    public void jump(InputAction.CallbackContext context)
    {
        //Allow player to jump when grounded
        moveValue.y = 1;
        if (context.performed && IsGrounded())
        {   
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioManager.instance.PlaySFX("Jump");
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
            boxCollider.offset = new Vector2(0f, -0.23f);
            boxCollider.size = new Vector2(0.5f, 0.48f);
            crouching = true;
            player_animator.SetBool("crouch", true);
            AudioManager.instance.PlaySFX("Crouch");
        }

        if (context.canceled)
        {
            crouching = false;
        }
    }


    //pause the game
    public void pause(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            //pause game and display pause menu
            Debug.Log("Pause Game");
            pauseMenu.pause();
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayer);
        return isGrounded;
    }

    private bool CanStand()
    {
        canStand = !Physics2D.OverlapCircle(ceilingCheck.position, 0.1f, groundLayer);
        return canStand;
    }

    void FixedUpdate()
    {   
        rb.velocity = new Vector2(moveValue.x * moveSpeed, rb.velocity.y);
        if(moveValue.x != 0 && IsGrounded()) {
            AudioManager.instance.PlayFootsteps();
        }
        else {
            AudioManager.instance.StopFootsteps();
        }
    }
}
