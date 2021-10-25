using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pitfall")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
