using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask platformMask;
    public FixedJoint2D playerFixedJoint;
    GameObject box;

    void Start()
    {
        playerFixedJoint = GetComponent<FixedJoint2D>();
    }
    
    private Vector3 rayDirection()
    {
        return Vector2.right * transform.localScale.x;
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, rayDirection(), 
            distance, platformMask);

        if (hit.collider != null &&
            hit.collider.gameObject.CompareTag("PushPullPlatform") &&
            PlayerController.isPulling)
        {
            box = hit.collider.gameObject;
            playerFixedJoint.enabled = true;
            playerFixedJoint.connectedBody = box.GetComponent<Rigidbody2D>();

        }
        else if (!PlayerController.isPulling)
        {
            playerFixedJoint.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = PlayerController.isPulling ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, 
            transform.position + rayDirection() * distance);
    }
}
