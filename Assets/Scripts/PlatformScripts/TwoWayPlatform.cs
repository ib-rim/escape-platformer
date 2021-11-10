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
        
        if (playerMoveValue == -1 && playerIsOnPlatform)
        {
            effector.rotationalOffset = 180f;
        }

        if (playerMoveValue == 1)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
