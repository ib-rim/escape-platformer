using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    public FixedJoint2D joint;
    public Rigidbody2D boxRigidBody;
    public static bool isPulling;

    private void Start()
    {
        isPulling = false;
        joint.connectedBody = null;
        joint.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushPullPlatform") &&
            PlayerController.isPressingPullKey &&
            PlayerController.isGrounded)
        {
            boxRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController.jumpEnabled = false;
            isPulling = true;
            joint.enabled = true;
            joint.connectedBody = boxRigidBody;
        }
        else
        {
            isPulling = false;
        }
    }

    private void Update()
    {
        if (PlayerController.isPressingPullKey == false)
        {
            PlayerController.jumpEnabled = true;
            isPulling = false;
        }
        if (isPulling == false)
        {
            joint.connectedBody = null;
            joint.enabled = false;
        }
    }
}
