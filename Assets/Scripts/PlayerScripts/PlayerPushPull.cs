using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    public FixedJoint2D joint;
    private bool isPulling;

    private void Start()
    {
        isPulling = false;
        joint.connectedBody = null;
        joint.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushPullPlatform") &&
            PlayerController.isPressingPullKey)
        {
            isPulling = true;
            joint.enabled = true;
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
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
            isPulling = false;
        }
        if (isPulling == false)
        {
            joint.connectedBody = null;
            joint.enabled = false;
        }
    }
}
