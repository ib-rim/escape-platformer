using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float playerMoveValue;
    private bool playerIsOnPlatform;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        effector.rotationalOffset = 0f;
    }

    void Update()
    {
        playerMoveValue = PlayerController.moveValue.y;
        playerIsOnPlatform = PlayerController.isGrounded;
        
        // When the down key is pressed, and when the player is grounded,
        // Allow player to drop through the platform from above
        if (playerMoveValue == -1 && playerIsOnPlatform)
        {
            effector.rotationalOffset = 180f;
        }

        // When the player is jumping, 
        // Allow player to pass throught the platform from below
        if (playerMoveValue == 1)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
