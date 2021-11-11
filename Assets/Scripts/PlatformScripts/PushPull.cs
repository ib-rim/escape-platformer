using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    private bool isPlayerPulling;
    private bool isPlayerTouching;
    public Transform objectTransform;
    public PlayerController player;

    private void Start()
    {
        objectTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        isPlayerPulling = PlayerController.isPulling;
        isPlayerTouching = PlayerCollisions.isTouchingPushPull;

        // If the player is pulling, let the object follow the player
        objectTransform.parent = isPlayerPulling && isPlayerTouching ? player.transform : null;
    }
}
